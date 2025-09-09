using UnityEngine;
using SBS.ME; // No olvides incluir el namespace de MeshExploder

public class ControladorBarril : MonoBehaviour
{
    public GameObject prefabEfectoExplosion;

    [Header("Configuraci�n de Da�o")]
    [Tooltip("Marca esta casilla si quieres que el barril haga da�o al explotar.")]
    public bool generarDa�o = true; // La casilla para activar/desactivar el da�o

    [Tooltip("El radio en metros dentro del cual la explosi�n causar� da�o.")]
    public float radioDeDa�o = 5f;

    [Tooltip("La cantidad de vida que restar� la explosi�n.")]
    public int cantidadDeDa�o = 2;

    private MeshExploder exploder;
    private bool haExplotado = false;

    void Awake()
    {
        exploder = GetComponent<MeshExploder>();
    }

    void OnEnable()
    {
        // Suscribirse a los eventos
        exploder.onExplosionStarted.AddListener(AlIniciarExplosion);
    }

    void OnDisable()
    {
        // Desuscribirse de los eventos
        exploder.onExplosionStarted.RemoveListener(AlIniciarExplosion);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!haExplotado && other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Bala1") || !haExplotado && other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Bala2"))
        {
            haExplotado = true;
            exploder.EXPLODE();

        }
    }
  


    // Esta funci�n ahora se encarga de TODO lo que pasa al explotar.
    private void AlIniciarExplosion(GameObject objetoQueExplota, Vector3 posicion)
    {
        // 1. Instanciar el efecto visual
        if (prefabEfectoExplosion != null)
        {
            Instantiate(prefabEfectoExplosion, posicion, Quaternion.identity);
        }

        // 2. Aplicar el da�o si la opci�n est� activada
        if (generarDa�o)
        {
            AplicarDa�oEnArea(posicion);
        }
    }

    private void AplicarDa�oEnArea(Vector3 centroDeExplosion)
    {
        // Creamos una esfera invisible y obtenemos todos los colliders dentro de ella.
        Collider[] collidersAfectados = Physics.OverlapSphere(centroDeExplosion, radioDeDa�o);

        // Recorremos cada objeto que fue golpeado por la explosi�n.
        foreach (Collider hitCollider in collidersAfectados)
        {
            // Comprobamos si el objeto afectado es el jugador.
            if (hitCollider.CompareTag("Player"))
            {
                // Intentamos obtener el script de vida del jugador.
                VidaDelJugador vidaJugador = hitCollider.GetComponent<VidaDelJugador>();
                if (vidaJugador != null)
                {
                    // Si encontramos el script, le restamos la vida.
                    vidaJugador.vidaActual -= cantidadDeDa�o;
                    Debug.Log("�El jugador ha recibido " + cantidadDeDa�o + " de da�o! Vida restante: " + vidaJugador.vidaActual);

                    // Salimos del bucle si ya hemos da�ado al jugador una vez.
                    // Esto es �til si el jugador tiene m�ltiples colliders.
                    break;
                }
            }
        }
    }

    // --- Ayuda Visual para el Editor de Unity ---
    // Esto dibujar� una esfera roja en la vista de escena para que puedas
    // ver el radio de da�o sin necesidad de darle a Play.
    private void OnDrawGizmosSelected()
    {
        if (generarDa�o)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDa�o);
        }
    }
}