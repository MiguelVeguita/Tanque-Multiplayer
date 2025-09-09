using UnityEngine;

public class EfectoDeExplosion : MonoBehaviour
{
    public GameObject prefabEfecto; // Aquí arrastrarás tu prefab de explosión

    // Esta es la función que llamaremos desde el evento de MeshExploder
    public void InstanciarEfecto(GameObject objetoQueExplota, Vector3 posicion)
    {
        if (prefabEfecto != null)
        {
            // Creamos el efecto de partículas en la posición de la explosión
            Instantiate(prefabEfecto, posicion, Quaternion.identity);
        }
    }
}