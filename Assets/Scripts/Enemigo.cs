using UnityEngine;

public class Enemigo : MonoBehaviour
{
    [Header("Stats")]
    public int vidaMaxima = 30;
    public float velocidadMovimiento = 4f;
    public int danioAlJugador = 1;
    public int puntaje = 10;

    [Header("Movimiento errático")]
    public float tiempoEntreMovimientos = 0.4f;
    public float distanciaMovimiento = 3f;

    [Header("Comportamiento agresivo")]
    public float tiempoHastaAtacar = 5f;

    [Header("Límites de la pantalla")]
    public float limitX = 12f;
    public float limitY = 6f;

    [Header("Efectos visuales")]
    public GameObject prefabExplosion;         // Arrastrá tu prefab de partículas
    public float duracionParpadeo = 0.12f;
    public Color colorParpadeo = new Color(1f, 0.3f, 0.3f); // Rojo claro

    // Privados
    protected int vidaActual;
    protected Transform jugador;
    private Rigidbody rb;
    private Vector3 destino;
    private float timerMovimiento;
    private float timerAtaque;
    private bool estaAtacando = false;

    // Para el parpadeo
    private Renderer rendererEnemigo;
    private Color colorOriginal;
    private bool parpadeando = false;

    protected virtual void Start()
    {
        vidaActual = vidaMaxima;
        rb = GetComponent<Rigidbody>();
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
        timerMovimiento = tiempoEntreMovimientos;
        timerAtaque = tiempoHastaAtacar;

        rendererEnemigo = GetComponent<Renderer>();
        if (rendererEnemigo != null)
            colorOriginal = rendererEnemigo.material.color;

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
        Vector3 direccion = (destino - rb.position).normalized;
        float distancia = Vector3.Distance(rb.position, destino);

        if (distancia > 0.3f)
            rb.MovePosition(rb.position + direccion * velocidadMovimiento * Time.fixedDeltaTime);
        else if (!estaAtacando)
            ElegirNuevoDestino();
    }

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
            transform.position.z + Random.Range(-1.5f, 1.5f)
        );
    }

    void ComportamientoAgresivo()
    {
        if (jugador != null)
            destino = jugador.position;
    }

    public void RecibirDanio(int danio)
    {
        vidaActual -= danio;

        if (!parpadeando)
            StartCoroutine(Parpadear());

        if (vidaActual <= 0)
            Morir();
    }

    // Parpadeo rojo al recibir daño
    System.Collections.IEnumerator Parpadear()
    {
        parpadeando = true;

        if (rendererEnemigo != null)
            rendererEnemigo.material.color = colorParpadeo;

        yield return new WaitForSeconds(duracionParpadeo);

        if (rendererEnemigo != null)
            rendererEnemigo.material.color = colorOriginal;

        parpadeando = false;
    }

    protected virtual void Morir()
    {
        Debug.Log("Enemigo murió en: " + transform.position);

        if (prefabExplosion != null)
        {
            Debug.Log("Instanciando explosión...");
            GameObject explosion = Instantiate(prefabExplosion, transform.position, Quaternion.identity);
            Debug.Log("Explosión creada: " + explosion.name);
        }
        else
        {
            Debug.LogWarning("prefabExplosion es NULL en " + gameObject.name);
        }

        VidaJugador vidaJugador = GameObject.FindGameObjectWithTag("Player")
                                            .GetComponent<VidaJugador>();
        if (vidaJugador != null)
            vidaJugador.SumarPuntaje(puntaje);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            otro.GetComponent<VidaJugador>()?.RecibirDanio(danioAlJugador);
            Morir();
        }
    }
}