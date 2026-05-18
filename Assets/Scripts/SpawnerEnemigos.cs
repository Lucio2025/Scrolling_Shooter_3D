using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject preEnemigo;
    public GameObject preTanque;
    public GameObject preRapido;

    [Header("Configuración de spawn")]
    public float tiempoEntreSpawns = 2f;
    public float limitX = 14f;
    public float limitY = 6f;
    public float posicionZ = 15f;  // Desde dónde aparecen (adelante)

    private float timer;

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            SpawnearEnemigo();
            timer = tiempoEntreSpawns;
        }
    }

    void SpawnearEnemigo()
    {
        // Posición aleatoria en los bordes laterales
        float x = Random.value > 0.5f ? limitX : -limitX;
        float y = Random.Range(-limitY, limitY);
        Vector3 posicion = new Vector3(x, y, posicionZ);

        // Elegir tipo con probabilidad
        float roll = Random.value;
        GameObject prefab;

        if (roll < 0.6f) prefab = preEnemigo;   // 60% básico
        else if (roll < 0.85f) prefab = preRapido;    // 25% rápido
        else prefab = preTanque;    // 15% tanque

        Instantiate(prefab, posicion, Quaternion.identity);
    }
}