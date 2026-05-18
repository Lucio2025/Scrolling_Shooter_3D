using UnityEngine;
using UnityEngine.InputSystem;

public class MiraCursor : MonoBehaviour
{
    [Header("Texturas de la mira")]
    public Texture2D miraNormal;
    public Texture2D miraEnemigo;

    [Header("Capa de los enemigos")]
    public LayerMask capaEnemigos;

    [Header("Cámara")]
    public Camera camPrincipal;

    void Start()
    {
        // Ocultamos el cursor del sistema y lo reemplazamos con nuestra mira
        Cursor.visible = false;
        ActualizarCursor(miraNormal);
    }

    void Update()
    {
        DetectarEnemigo();
    }

    void DetectarEnemigo()
    {
        // Lanzamos un raycast desde la cámara hacia donde apunta el mouse
        Ray rayo = camPrincipal.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit impacto;

        if (Physics.Raycast(rayo, out impacto, 200f, capaEnemigos))
        {
            // El rayo tocó un enemigo → mira roja
            ActualizarCursor(miraEnemigo);
        }
        else
        {
            // No hay enemigo → mira normal
            ActualizarCursor(miraNormal);
        }
    }

    void ActualizarCursor(Texture2D textura)
    {
        // El hotspot es el centro de la imagen (donde "apunta" realmente)
        Vector2 hotspot = new Vector2(textura.width / 2f, textura.height / 2f);
        Cursor.SetCursor(textura, hotspot, CursorMode.Auto);
        Cursor.visible = true;
    }

    // Devuelve el punto 3D donde está apuntando el mouse (lo usará el disparo)
    public Vector3 ObtenerPuntoObjetivo()
    {
        Ray rayo = camPrincipal.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit impacto;

        if (Physics.Raycast(rayo, out impacto, 200f))
        {
            return impacto.point;
        }

        // Si no toca nada, devuelve un punto lejano en esa dirección
        return rayo.GetPoint(50f);
    }
}