using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 8f;

    [Header("Límites del área de juego")]
    public float limitX = 4f;
    public float limitZMin = -9f;
    public float limitZMax = 2f;

    private Rigidbody rb;
    private Vector2 inputMovimiento;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // El nuevo Input System llama a este método automáticamente
    // cuando detecta el Action "Move" (viene incluido por defecto)
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
        Vector3 direccion = new Vector3(inputMovimiento.x, 0f, inputMovimiento.y).normalized;

        Vector3 nuevaPosicion = rb.position + direccion * moveSpeed * Time.fixedDeltaTime;

        nuevaPosicion.x = Mathf.Clamp(nuevaPosicion.x, -limitX, limitX);
        nuevaPosicion.z = Mathf.Clamp(nuevaPosicion.z, limitZMin, limitZMax);
        nuevaPosicion.y = 1f;

        rb.MovePosition(nuevaPosicion);
    }
}