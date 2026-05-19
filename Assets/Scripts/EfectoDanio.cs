using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EfectoDanio : MonoBehaviour
{
    [Header("Flash de pantalla roja")]
    public Image imagenFlash;           // Una Image roja que cubre toda la pantalla
    public float duracionFlash = 0.3f;

    [Header("Parpadeo del jugador")]
    public Renderer rendererJugador;
    public float duracionInmunidad = 1.5f;
    public float velocidadParpadeo = 0.1f;

    public void ActivarEfectoDanio()
    {
        StartCoroutine(FlashPantalla());
        StartCoroutine(ParpadeoJugador());
    }

    IEnumerator FlashPantalla()
    {
        if (imagenFlash == null) yield break;

        // Aparece rojo
        imagenFlash.color = new Color(1f, 0f, 0f, 0.4f);
        yield return new WaitForSeconds(duracionFlash);

        // Desaparece gradualmente
        float timer = 0f;
        while (timer < duracionFlash)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Lerp(0.4f, 0f, timer / duracionFlash);
            imagenFlash.color = new Color(1f, 0f, 0f, alpha);
            yield return null;
        }

        imagenFlash.color = new Color(1f, 0f, 0f, 0f);
    }

    IEnumerator ParpadeoJugador()
    {
        if (rendererJugador == null) yield break;

        float timer = 0f;
        while (timer < duracionInmunidad)
        {
            // Alterna entre visible e invisible
            rendererJugador.enabled = !rendererJugador.enabled;
            timer += velocidadParpadeo;
            yield return new WaitForSeconds(velocidadParpadeo);
        }

        // Asegurarse que quede visible al final
        rendererJugador.enabled = true;
    }
}