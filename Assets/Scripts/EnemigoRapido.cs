using UnityEngine;

public class EnemigoRapido : Enemigo
{
    protected override void Start()
    {
        vidaMaxima = 15;
        velocidadMovimiento = 9f;
        danioAlJugador = 5;
        tiempoHastaAtacar = 3f;
        puntaje = 150;
        base.Start();
    }
}