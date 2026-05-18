using UnityEngine;

public class EnemigoTanque : Enemigo
{
    protected override void Start()
    {
        vidaMaxima = 80;
        velocidadMovimiento = 2.5f;
        danioAlJugador = 20;
        puntaje = 250;
        base.Start();
    }
}