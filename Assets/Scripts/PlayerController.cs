using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 8f;

    [Header("Límites del área de juego (el jugador puede ir hasta acá)")]
    public float limitX = 14f;
    public float limitY = 7f;

    private Rigidbody rb;
    private Vector2 inputMovimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMove(InputValue value)
    {
        inputMovimiento = value.Get<Vector2>();
    }

    void FixedUpdate()
    {
        MoverJugador();
    }

    void MoverJugador()
    {
        // W/S = arriba/abajo (eje Y), A/D = izquierda/derecha (eje X)
        Vector3 direccion = new Vector3(inputMovimiento.x, inputMovimiento.y, 0f).normalized;

        Vector3 nuevaPosicion = rb.position + direccion * moveSpeed * Time.fixedDeltaTime;

        // Limitar dentro del área de juego
        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, -limitX, limitX);
        nuevaPosicion.y = Mathf.Clamp(nuevaPosicion.y, -limitY, limitY);
        nuevaPosicion.z = rb.position.z; // Z fijo siempre

        rb.MovePosition(nuevaPosicion);
    }
}