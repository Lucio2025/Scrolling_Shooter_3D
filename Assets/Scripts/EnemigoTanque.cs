using UnityEngine;

public class EnemigoTanque : Enemigo
{
    protected override void Start()
    {
        vidaMaxima = 80;
        velocidadMovimiento = 2.5f;
        danioAlJugador = 1;
        puntaje = 15;
        base.Start();
    }
}