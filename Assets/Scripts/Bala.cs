using UnityEngine;

public class Bala : MonoBehaviour
{
    [Header("Daño")]
    public int danio = 10;

    [Header("Tiempo de vida")]
    public float tiempoVida = 4f;

    void Start()
    {
        Destroy(gameObject, tiempoVida);
    }

    void OnTriggerEnter(Collider otro)
    {
        if (otro.CompareTag("Enemigo"))
        {
            otro.GetComponent<Enemigo>()?.RecibirDanio(danio);
            Destroy(gameObject);
        }
    }
}