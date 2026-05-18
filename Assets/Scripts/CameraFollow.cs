using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Jugador")]
    public Transform target;

    [Header("Posición base de la cámara")]
    public Vector3 offsetBase = new Vector3(0f, 4f, -14f);

    [Header("Límite en el que la cámara empieza a moverse")]
    [Tooltip("El jugador puede ir hasta 14, la cámara se mueve cuando pasa de 10")]
    public float umbralX = 10f;
    public float umbralY = 4f;

    [Header("Cuánto se desplaza la cámara máximo")]
    public float maxDesplazamientoX = 2f;
    public float maxDesplazamientoY = 1.5f;

    [Header("Suavidad")]
    public float smoothSpeed = 3f;

    void LateUpdate()
    {
        if (target == null) return;

        float desplX = 0f;
        float desplY = 0f;

        // Si el jugador pasa el umbral X, la cámara se corre levemente
        if (Mathf.Abs(target.position.x) > umbralX)
        {
            float exceso = target.position.x - (umbralX * Mathf.Sign(target.position.x));
            desplX = Mathf.Clamp(exceso, -maxDesplazamientoX, maxDesplazamientoX);
        }

        // Si el jugador pasa el umbral Y, la cámara se mueve levemente
        if (Mathf.Abs(target.position.y) > umbralY)
        {
            float exceso = target.position.y - (umbralY * Mathf.Sign(target.position.y));
            desplY = Mathf.Clamp(exceso, -maxDesplazamientoY, maxDesplazamientoY);
        }

        Vector3 posicionDeseada = new Vector3(
            offsetBase.x + desplX,
            offsetBase.y + desplY,
            offsetBase.z
        );

        transform.position = Vector3.Lerp(transform.position, posicionDeseada, smoothSpeed * Time.deltaTime);
    }
}