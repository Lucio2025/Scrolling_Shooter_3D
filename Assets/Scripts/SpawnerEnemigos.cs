using UnityEngine;

public class SpawnerEnemigos : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject preEnemigo;
    public GameObject preTanque;
    public GameObject preRapido;
    public GameObject preFrancotirador;

    [Header("Configuración")]
    public float tiempoEntreSpawns = 2f;
    public float limitX = 14f;
    public float limitY = 6f;
    public float posicionZ = 15f;

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
        float x = Random.value > 0.5f ? limitX : -limitX;
        float y = Random.Range(-limitY, limitY);
        Vector3 posicion = new Vector3(x, y, posicionZ);

        float roll = Random.value;
        GameObject prefab;

        if (roll < 0.5f) prefab = preEnemigo;         // 50% básico
        else if (roll < 0.7f) prefab = preRapido;          // 20% rápido
        else if (roll < 0.85f) prefab = preFrancotirador;   // 15% francotirador
        else prefab = preTanque;           // 15% tanque

        Instantiate(prefab, posicion, Quaternion.identity);
    }
}