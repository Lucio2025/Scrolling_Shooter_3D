using UnityEngine;

public class SpawnerDecoracion : MonoBehaviour
{
    [Header("Objetos decorativos")]
    public GameObject[] prefabsDecoracion;

    [Header("Configuración")]
    public float velocidadScroll = 7f;
    public float tiempoEntreSpawns = 4f;
    public float limiteX = 70f;
    public float spawnZ = 200f;
    public float destruccionZ = -50f;

    [Header("Raycast al piso")]
    [Tooltip("Desde qué altura se lanza el rayo hacia abajo")]
    public float alturaRaycast = 20f;
    [Tooltip("Cuánto se eleva el objeto sobre el piso (para que no flote ni se entierre)")]
    public float offsetY = 0f;
    public LayerMask capaPiso;  // Asigná el layer del Ground acá

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnearDecoracion();
            timer = tiempoEntreSpawns + Random.Range(-0.3f, 0.3f);
        }

        // Mover toda la decoración hacia atrás
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Transform hijo = transform.GetChild(i);
            hijo.position += Vector3.back * velocidadScroll * Time.deltaTime;

            if (hijo.position.z < destruccionZ)
                Destroy(hijo.gameObject);
        }
    }

    void SpawnearDecoracion()
    {
        if (prefabsDecoracion.Length == 0) return;

        float x = Random.Range(-limiteX, limiteX);

        // Lanzar raycast desde arriba hacia abajo para encontrar el piso
        Vector3 origenRayo = new Vector3(x, alturaRaycast, spawnZ);
        RaycastHit impacto;

        if (Physics.Raycast(origenRayo, Vector3.down, out impacto, alturaRaycast * 2f, capaPiso))
        {
            // Encontró el piso — spawneamos justo ahí
            Vector3 posicion = impacto.point + Vector3.up * offsetY;

            GameObject prefab = prefabsDecoracion[Random.Range(0, prefabsDecoracion.Length)];

            Quaternion rotacion = Quaternion.Euler(
                0f,
                Random.Range(0f, 360f),
                0f  // Sin inclinación — siempre derecho sobre el piso
            );

            GameObject obj = Instantiate(prefab, posicion, rotacion);
            obj.transform.SetParent(transform);
        }
        // Si no encuentra piso, simplemente no spawna nada
    }

    // Dibuja el área de spawn en el editor para facilitar ajustes
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(
            new Vector3(0, 0, spawnZ),
            new Vector3(limiteX * 2, 1, 1)
        );
    }
}