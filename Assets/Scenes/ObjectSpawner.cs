using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Prefabs a Generar")]
    [Tooltip("Arrastra aqu� todos los prefabs que quieres que este spawner pueda generar.")]
    public List<GameObject> prefabsToSpawn;

    [Header("Puntos de Aparici�n")]
    [Tooltip("Crea GameObjects vac�os en tu escena y arr�stralos aqu� para definir las posiciones de spawn.")]
    public List<Transform> spawnPoints;

    [Header("Configuraci�n de Tiempo")]
    [Tooltip("El tiempo en segundos que espera el spawner antes de intentar generar un nuevo objeto.")]
    public float spawnInterval = 5f;

    // Array interno para rastrear qu� objeto ocupa cada punto de aparici�n.
    private GameObject[] spawnedObjects;

    void Start()
    {
        // Validamos que todo est� configurado correctamente para evitar errores.
        if (prefabsToSpawn.Count == 0)
        {
            Debug.LogError("La lista 'prefabsToSpawn' est� vac�a. No se puede generar nada.", this);
            return;
        }

        if (spawnPoints.Count == 0)
        {
            Debug.LogError("La lista 'spawnPoints' est� vac�a. No hay d�nde generar objetos.", this);
            return;
        }

        // Inicializamos nuestro array de seguimiento con el mismo tama�o que la lista de puntos.
        spawnedObjects = new GameObject[spawnPoints.Count];

        // Iniciamos la rutina de generaci�n que se ejecutar� en segundo plano.
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        // Este bucle se ejecuta infinitamente mientras el objeto Spawner est� activo.
        while (true)
        {
            // Espera el intervalo de tiempo definido antes de continuar.
            yield return new WaitForSeconds(spawnInterval);

            // Llama a la funci�n que intenta generar un objeto.
            TrySpawnObject();
        }
    }

    private void TrySpawnObject()
    {
        // 1. Encontrar todos los puntos de aparici�n que est�n libres.
        List<int> availableIndices = new List<int>();
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            // La "magia" est� aqu�: si el objeto en nuestro array es 'null',
            // significa que el punto est� libre (o el objeto fue destruido).
            if (spawnedObjects[i] == null)
            {
                availableIndices.Add(i);
            }
        }

        // 2. Si encontramos al menos un punto libre, procedemos a generar.
        if (availableIndices.Count > 0)
        {
            // Elegimos un �ndice al azar de la lista de puntos disponibles.
            int randomIndex = availableIndices[Random.Range(0, availableIndices.Count)];
            Transform spawnPoint = spawnPoints[randomIndex];

            // Elegimos un prefab al azar de la lista de prefabs.
            GameObject prefab = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];

            // Creamos la nueva instancia del prefab en la posici�n y rotaci�n del punto.
            GameObject newObject = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            Debug.Log($"Generando '{newObject.name}' en el punto '{spawnPoint.name}'.");

            // 3. MUY IMPORTANTE: Registramos el objeto reci�n creado en nuestro array de seguimiento.
            spawnedObjects[randomIndex] = newObject;
        }
        else
        {
            Debug.Log("Todos los puntos de aparici�n est�n ocupados. Esperando...");
        }
    }

    // --- Ayuda Visual en el Editor ---
    // Dibuja esferas en la vista de escena para que puedas ver f�cilmente d�nde est�n tus spawn points.
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