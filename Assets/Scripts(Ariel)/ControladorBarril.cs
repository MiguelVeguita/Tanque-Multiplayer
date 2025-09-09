using UnityEngine;
using SBS.ME; // No olvides incluir el namespace de MeshExploder

public class ControladorBarril : MonoBehaviour
{
    public GameObject prefabEfectoExplosion;

    [Header("Configuración de Daño")]
    [Tooltip("Marca esta casilla si quieres que el barril haga daño al explotar.")]
    public bool generarDaño = true; // La casilla para activar/desactivar el daño

    [Tooltip("El radio en metros dentro del cual la explosión causará daño.")]
    public float radioDeDaño = 5f;

    [Tooltip("La cantidad de vida que restará la explosión.")]
    public int cantidadDeDaño = 2;

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
  


    // Esta función ahora se encarga de TODO lo que pasa al explotar.
    private void AlIniciarExplosion(GameObject objetoQueExplota, Vector3 posicion)
    {
        // 1. Instanciar el efecto visual
        if (prefabEfectoExplosion != null)
        {
            Instantiate(prefabEfectoExplosion, posicion, Quaternion.identity);
        }

        // 2. Aplicar el daño si la opción está activada
        if (generarDaño)
        {
            AplicarDañoEnArea(posicion);
        }
    }

    private void AplicarDañoEnArea(Vector3 centroDeExplosion)
    {
        // Creamos una esfera invisible y obtenemos todos los colliders dentro de ella.
        Collider[] collidersAfectados = Physics.OverlapSphere(centroDeExplosion, radioDeDaño);

        // Recorremos cada objeto que fue golpeado por la explosión.
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
                    vidaJugador.vidaActual -= cantidadDeDaño;
                    Debug.Log("¡El jugador ha recibido " + cantidadDeDaño + " de daño! Vida restante: " + vidaJugador.vidaActual);

                    // Salimos del bucle si ya hemos dañado al jugador una vez.
                    // Esto es útil si el jugador tiene múltiples colliders.
                    break;
                }
            }
        }
    }

    // --- Ayuda Visual para el Editor de Unity ---
    // Esto dibujará una esfera roja en la vista de escena para que puedas
    // ver el radio de daño sin necesidad de darle a Play.
    private void OnDrawGizmosSelected()
    {
        if (generarDaño)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, radioDeDaño);
        }
    }
}