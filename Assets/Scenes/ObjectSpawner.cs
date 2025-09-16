using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs a Generar")]
    [Tooltip("Arrastra aquí todos los prefabs que quieres que este spawner pueda generar.")]
    public List<GameObject> prefabsToSpawn;

    [Header("Puntos de Aparición")]
    [Tooltip("Crea GameObjects vacíos en tu escena y arrástralos aquí para definir las posiciones de spawn.")]
    public List<Transform> spawnPoints;

    [Header("Configuración de Tiempo")]
    [Tooltip("El tiempo en segundos que espera el spawner antes de intentar generar un nuevo objeto.")]
    public float spawnInterval = 5f;

    // Array interno para rastrear qué objeto ocupa cada punto de aparición.
    private GameObject[] spawnedObjects;

    void Start()
    {
        // Validamos que todo esté configurado correctamente para evitar errores.
        if (prefabsToSpawn.Count == 0)
        {
            Debug.LogError("La lista 'prefabsToSpawn' está vacía. No se puede generar nada.", this);
            return;
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("La lista 'spawnPoints' está vacía. No hay dónde generar objetos.", this);
            return;
        }

        // Inicializamos nuestro array de seguimiento con el mismo tamaño que la lista de puntos.
        spawnedObjects = new GameObject[spawnPoints.Count];

        // Iniciamos la rutina de generación que se ejecutará en segundo plano.
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // Este bucle se ejecuta infinitamente mientras el objeto Spawner esté activo.
        while (true)
        {
            // Espera el intervalo de tiempo definido antes de continuar.
            yield return new WaitForSeconds(spawnInterval);

            // Llama a la función que intenta generar un objeto.
            TrySpawnObject();
        }
    }

    private void TrySpawnObject()
    {
        // 1. Encontrar todos los puntos de aparición que están libres.
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            // La "magia" está aquí: si el objeto en nuestro array es 'null',
            // significa que el punto está libre (o el objeto fue destruido).
            if (spawnedObjects[i] == null)
            {
                availableIndices.Add(i);
            }
        }

        // 2. Si encontramos al menos un punto libre, procedemos a generar.
        if (availableIndices.Count > 0)
        {
            // Elegimos un índice al azar de la lista de puntos disponibles.
            int randomIndex = availableIndices[Random.Range(0, availableIndices.Count)];
            Transform spawnPoint = spawnPoints[randomIndex];

            // Elegimos un prefab al azar de la lista de prefabs.
            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];

            // Creamos la nueva instancia del prefab en la posición y rotación del punto.
            GameObject newObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Generando '{newObject.name}' en el punto '{spawnPoint.name}'.");

            // 3. MUY IMPORTANTE: Registramos el objeto recién creado en nuestro array de seguimiento.
            spawnedObjects[randomIndex] = newObject;
        }
        else
        {
            Debug.Log("Todos los puntos de aparición están ocupados. Esperando...");
        }
    }

    // --- Ayuda Visual en el Editor ---
    // Dibuja esferas en la vista de escena para que puedas ver fácilmente dónde están tus spawn points.
    void OnDrawGizmos()
    {
        if (spawnPoints == null) return;

        Gizmos.color = new Color(0.2f, 1f, 0.3f, 0.5f); // Verde semitransparente
        foreach (Transform point in spawnPoints)
        {
            if (point != null)
            {
                Gizmos.DrawSphere(point.position, 0.5f);
            }
        }
    }
}