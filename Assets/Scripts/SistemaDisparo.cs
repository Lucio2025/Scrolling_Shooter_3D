using UnityEngine;
using UnityEngine.InputSystem;

public class SistemaDisparo : MonoBehaviour
{
    [Header("Referencia a la mira")]
    public MiraCursor mira;

    [Header("Proyectil normal (fusil)")]
    public GameObject prefabBalaNormal;
    public float velocidadBalaNormal = 30f;
    public float cadenciaFusil = 0.08f;

    [Header("Proyectil especial (torpedo)")]
    public GameObject prefabBalaEspecial;
    public float velocidadBalaEspecial = 10f;
    public float cadenciaTorpedo = 1f;

    [Header("Punto de salida de las balas")]
    public Transform puntoDisparo;

    private float timerFusil = 0f;
    private float timerTorpedo = 0f;

    // Guardamos si el botón está ACTUALMENTE presionado
    private bool fusilPresionado = false;
    private bool torpedoPresionado = false;

    void Update()
    {
        // Leemos el mouse directamente — más confiable para botones
        fusilPresionado = Mouse.current.leftButton.isPressed;
        torpedoPresionado = Mouse.current.rightButton.isPressed;

        timerFusil -= Time.deltaTime;
        timerTorpedo -= Time.deltaTime;

        if (fusilPresionado && timerFusil <= 0f)
        {
            Disparar(prefabBalaNormal, velocidadBalaNormal);
            timerFusil = cadenciaFusil;
        }

        if (torpedoPresionado && timerTorpedo <= 0f)
        {
            Disparar(prefabBalaEspecial, velocidadBalaEspecial);
            timerTorpedo = cadenciaTorpedo;
        }
    }

    void Disparar(GameObject prefabBala, float velocidad)
    {
        if (prefabBala == null || mira == null) return;

        Vector3 puntoObjetivo = mira.ObtenerPuntoObjetivo();
        Vector3 direccion = (puntoObjetivo - puntoDisparo.position).normalized;

        GameObject bala = Instantiate(
            prefabBala,
            puntoDisparo.position,
            Quaternion.LookRotation(direccion)
        );

        Rigidbody rb = bala.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = direccion * velocidad;
    }
}