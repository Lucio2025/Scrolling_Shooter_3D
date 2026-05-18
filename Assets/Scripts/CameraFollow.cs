using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Objetivo a seguir")]
    public Transform target;

    [Header("Offset respecto al jugador")]
    public Vector3 offset = new Vector3(0f, 12f, -10f);

    [Header("Suavidad del seguimiento")]
    public float smoothSpeed = 5f;

    void LateUpdate()
    {
        if (target == null) return;

        // Solo seguimos en X y Z (el jugador no sube/baja)
        Vector3 desiredPosition = new Vector3(
            target.position.x,
            transform.position.y,       // La cámara no sube ni baja
            target.position.z + offset.z
        );

        transform.position = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}