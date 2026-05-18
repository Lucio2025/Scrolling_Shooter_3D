using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Stats")]
    public int vidaMaxima = 30;
    public float velocidadMovimiento = 4f;
    public int danioAlJugador = 10;
    public int puntaje = 100;

    [Header("Movimiento errático")]
    public float tiempoEntreMovimientos = 0.4f;
    public float distanciaMovimiento = 3f;

    [Header("Comportamiento agresivo")]
    public float tiempoHastaAtacar = 5f;  // Segundos hasta que ataca

    [Header("Límites de la pantalla")]
    public float limitX = 12f;
    public float limitY = 6f;

    // Privados
    private int vidaActual;
    private Transform jugador;
    private Rigidbody rb;
    private Vector3 destino;
    private float timerMovimiento;
    private float timerAtaque;
    private bool estaAtacando = false;

    protected virtual void Start()
    {
        vidaActual = vidaMaxima;
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        timerMovimiento = tiempoEntreMovimientos;
        timerAtaque = tiempoHastaAtacar;
        ElegirNuevoDestino();
    }

    void Update()
    {
        timerAtaque -= Time.deltaTime;

        if (timerAtaque <= 0f)
            estaAtacando = true;

        if (estaAtacando)
            ComportamientoAgresivo();
        else
            ComportamientoErratico();
    }

    void FixedUpdate()
    {
        // Mover hacia el destino actual
        Vector3 direccion = (destino - rb.position).normalized;
        float distancia = Vector3.Distance(rb.position, destino);

        if (distancia > 0.3f)
            rb.MovePosition(rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
        else if (!estaAtacando)
            ElegirNuevoDestino(); // Llegó al destino → elegir otro
    }

    // ── Movimiento aleatorio dentro de la pantalla ──
    void ComportamientoErratico()
    {
        timerMovimiento -= Time.deltaTime;
        if (timerMovimiento <= 0f)
        {
            ElegirNuevoDestino();
            timerMovimiento = tiempoEntreMovimientos + Random.Range(-0.1f, 0.2f);
        }
    }

    void ElegirNuevoDestino()
    {
        destino = new Vector3(
            Random.Range(-limitX, limitX),
            Random.Range(-limitY, limitY),
            transform.position.z + Random.Range(-1.5f, 1.5f) // pequeña variación en Z
        );
    }

    // ── Comportamiento agresivo: va directo al jugador ──
    void ComportamientoAgresivo()
    {
        if (jugador != null)
            destino = jugador.position;
    }

    // ── Recibir daño ──
    public void RecibirDanio(int danio)
    {
        vidaActual -= danio;

        if (vidaActual <= 0)
            Morir();
    }

    protected virtual void Morir()
    {
        // Próximamente: sumar puntaje, partículas, etc.
        Destroy(gameObject);
    }

    // ── Colisión con el jugador ──
    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            //otro.GetComponent<VidaJugador>()?.RecibirDanio(danioAlJugador);
            Morir();
        }
    }
}