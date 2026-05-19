using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestorJuego : MonoBehaviour
{
    public static GestorJuego instancia;

    [Header("Puntaje para ganar")]
    public int puntajeVictoria = 1000;

    [Header("Paneles")]
    public GameObject panelGameOver;
    public GameObject panelVictoria;

    [Header("Texto puntaje final")]
    public TextMeshProUGUI textoPuntajeFinalGameOver;
    public TextMeshProUGUI textoPuntajeFinalVictoria;

    private bool juegoTerminado = false;

    void Awake()
    {
        // Singleton — solo existe un GestorJuego
        if (instancia == null)
            instancia = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        panelGameOver.SetActive(false);
        panelVictoria.SetActive(false);
    }

    public void VerificarVictoria(int puntajeActual)
    {
        if (juegoTerminado) return;

        if (puntajeActual >= puntajeVictoria)
            MostrarVictoria(puntajeActual);
    }

    public void MostrarGameOver(int puntajeFinal)
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        Time.timeScale = 0f; // Congela el juego

        if (textoPuntajeFinalGameOver != null)
            textoPuntajeFinalGameOver.text = "Puntaje: " + puntajeFinal;

        panelGameOver.SetActive(true);
    }

    public void MostrarVictoria(int puntajeFinal)
    {
        if (juegoTerminado) return;
        juegoTerminado = true;

        Time.timeScale = 0f; // Congela el juego

        if (textoPuntajeFinalVictoria != null)
            textoPuntajeFinalVictoria.text = "PUNTAJE FINAL: " + puntajeFinal;

        panelVictoria.SetActive(true);
    }

    // Llamado por el botón de reinicio
    public void Reiniciar()
    {
        Time.timeScale = 1f; // Descongela antes de recargar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}