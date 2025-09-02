using UnityEngine;
using System.Collections; // Necesario para las Coroutines

public class GeneradorDeEnemigos : MonoBehaviour
{
    [Header("Configuraci�n")]
    public GameObject enemigoPrefab;
    public Transform[] puntosDeSpawneo;
    public float tiempoEntreSpawns = 2f;
    public int maximoEnemigosEnEscena = 10;

    private int enemigosActuales = 0;

    void Start()
    {
        // Iniciamos la coroutine que se encargar� de generar enemigos.
        StartCoroutine(SpawneoCoroutine());
        // Nos suscribimos al evento de muerte para saber cu�ndo podemos spawnear m�s.
        Musulman.OnMusulmDeath += HandleEnemigoMuerte;
    }

    // Este m�todo se asegura de que no haya fugas de memoria si el generador se destruye.
    private void OnDestroy()
    {
        Musulman.OnMusulmDeath -= HandleEnemigoMuerte;
    }

    IEnumerator SpawneoCoroutine()
    {
        // Bucle infinito que se ejecuta en segundo plano.
        while (true)
        {
            // Solo generamos un enemigo si no hemos alcanzado el l�mite.
            if (enemigosActuales < maximoEnemigosEnEscena)
            {
                SpawnearEnemigo();
            }
            // Esperamos el tiempo definido antes de intentar de nuevo.
            yield return new WaitForSeconds(tiempoEntreSpawns);
        }
    }

    void SpawnearEnemigo()
    {
        // Elegimos un punto de spawneo al azar.
        int indiceRandom = Random.Range(0, puntosDeSpawneo.Length);
        Transform puntoDeSpawneo = puntosDeSpawneo[indiceRandom];

        // Creamos una instancia del prefab del enemigo en ese punto.
        Instantiate(enemigoPrefab, puntoDeSpawneo.position, puntoDeSpawneo.rotation);
        enemigosActuales++;
    }

    // Este m�todo se llama autom�ticamente cada vez que un enemigo muere.
    void HandleEnemigoMuerte()
    {
        enemigosActuales--;
    }
}