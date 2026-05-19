using UnityEngine;

public class EnemigoFrancotirador : Enemigo
{
    [Header("Disparo")]
    public GameObject prefabProyectil;
    public float velocidadProyectil = 7f;
    public float cooldownDisparo = 2.5f;

    private float timerDisparo;

    protected override void Start()
    {
        // Stats del francotirador
        vidaMaxima = 25;
        velocidadMovimiento = 2f;
        danioAlJugador = 1;
        tiempoHastaAtacar = 999f; // Nunca se tira hacia el jugador
        puntaje = 15;

        base.Start();

        timerDisparo = cooldownDisparo;
    }

    void Update()
    {
        timerDisparo -= Time.deltaTime;

        if (timerDisparo <= 0f)
        {
            Disparar();
            timerDisparo = cooldownDisparo;
        }
    }

    void Disparar()
    {
        if (prefabProyectil == null || jugador == null) return;

        Vector3 direccion = (jugador.position - transform.position).normalized;

        GameObject proyectil = Instantiate(
            prefabProyectil,
            transform.position,
            Quaternion.LookRotation(direccion)
        );

        Rigidbody rb = proyectil.GetComponent<Rigidbody>();
        if (rb != null)
            rb.linearVelocity = direccion * velocidadProyectil;
    }
}