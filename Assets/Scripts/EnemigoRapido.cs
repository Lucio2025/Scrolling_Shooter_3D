using UnityEngine;

public class EnemigoRapido : Enemigo
{
    protected override void Start()
    {
        vidaMaxima = 15;
        velocidadMovimiento = 9f;
        danioAlJugador = 1;
        tiempoHastaAtacar = 3f;
        puntaje = 15;
        base.Start();
    }
}