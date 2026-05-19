using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    public int danio = 1;
    public float tiempoVida = 6f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Player"))
        {
            otro.GetComponent<VidaJugador>()?.RecibirDanio(danio);
            Destroy(gameObject);
        }

        // Se destruye si choca con una bala del jugador
        if (otro.CompareTag("Bala"))
            Destroy(gameObject);
    }
}