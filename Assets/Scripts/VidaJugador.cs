using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VidaJugador : MonoBehaviour
{
    [Header("Vida")]
    public int vidaMaxima = 3;
    private int vidaActual;

    [Header("Inmunidad temporal")]
    public float tiempoInmunidad = 1.5f;
    private float timerInmunidad = 0f;
    private bool esInmune = false;

    [Header("Corazones UI")]
    public RawImage corazon1;
    public RawImage corazon2;
    public RawImage corazon3;
    public Texture2D texturaCorazonLleno;
    public Texture2D texturaCorazonVacio;

    [Header("Puntaje")]
    public TextMeshProUGUI textoPuntaje;
    private int puntaje = 0;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarCorazones();
    }

    void Update()
    {
        // Contador de inmunidad
        if (esInmune)
        {
            timerInmunidad -= Time.deltaTime;
            if (timerInmunidad <= 0f)
                esInmune = false;
        }
    }

    public void RecibirDanio(int danio)
    {
        if (esInmune) return;  // Si es inmune, ignorar el daño

        vidaActual -= danio;
        vidaActual = Mathf.Clamp(vidaActual, 0, vidaMaxima);
        ActualizarCorazones();

        // Activar inmunidad temporal
        esInmune = true;
        timerInmunidad = tiempoInmunidad;

        Debug.Log("¡Jugador recibió daño! Vida actual: " + vidaActual);

        if (vidaActual <= 0)
            Morir();
    }

    public void SumarPuntaje(int puntos)
    {
        puntaje += puntos;
        if (textoPuntaje != null)
            textoPuntaje.text = "PUNTAJE: " + puntaje;
    }

    void ActualizarCorazones()
    {
        corazon1.texture = vidaActual >= 1 ? texturaCorazonLleno : texturaCorazonVacio;
        corazon2.texture = vidaActual >= 2 ? texturaCorazonLleno : texturaCorazonVacio;
        corazon3.texture = vidaActual >= 3 ? texturaCorazonLleno : texturaCorazonVacio;
    }

    void Morir()
    {
        Debug.Log("¡Game Over!");
        // Próximamente: pantalla de Game Over
    }
}