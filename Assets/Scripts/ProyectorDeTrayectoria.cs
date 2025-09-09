using UnityEngine;
using System.Collections.Generic; // Para usar Listas

[RequireComponent(typeof(LineRenderer))]
public class ProyectorDeTrayectoria : MonoBehaviour
{
    private LineRenderer lineRenderer;

    [Header("Configuración de la Curva")]
    [Tooltip("Número de puntos que formarán la curva.")]
    [Range(10, 100)]
    public int numeroDePuntos = 50;

    [Tooltip("El intervalo de tiempo entre cada punto de la simulación.")]
    [Range(0.01f, 0.25f)]
    public float intervaloDeTiempo = 0.1f;

    [Tooltip("Las capas con las que la trayectoria puede colisionar (paredes, enemigos, etc.).")]
    public LayerMask capasDeColision;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Este será nuestro método principal, lo llamaremos desde el script del tanque.
    public void DibujarTrayectoria(Vector3 puntoDeInicio, Vector3 velocidadInicial)
    {
        // Creamos una lista para guardar todos los puntos de la trayectoria.
        List<Vector3> puntos = new List<Vector3>();
        Vector3 posicionActual = puntoDeInicio;
        Vector3 velocidadActual = velocidadInicial;

        // Añadimos el primer punto, que es donde estamos.
        puntos.Add(posicionActual);

        // Hacemos un bucle para calcular los siguientes puntos.
        for (int i = 0; i < numeroDePuntos; i++)
        {
            // Movemos la posición según la velocidad actual.
            Vector3 siguientePosicion = posicionActual + velocidadActual * intervaloDeTiempo;

            // Simulamos el efecto de la gravedad en la velocidad.
            velocidadActual += Physics.gravity * intervaloDeTiempo;

            // --- Detección de Colisión (la parte más importante) ---
            // Lanzamos un rayo desde el punto anterior al nuevo para ver si chocamos con algo.
            if (Physics.Raycast(posicionActual, (siguientePosicion - posicionActual).normalized, out RaycastHit hit, Vector3.Distance(posicionActual, siguientePosicion), capasDeColision))
            {
                // Si chocamos, el último punto de la línea es el punto de impacto.
                puntos.Add(hit.point);
                // Salimos del bucle porque la trayectoria se detiene aquí.
                break;
            }

            // Si no chocamos, actualizamos la posición y añadimos el nuevo punto.
            posicionActual = siguientePosicion;
            puntos.Add(posicionActual);
        }

        // Finalmente, le pasamos la lista de puntos al Line Renderer para que los dibuje.
        lineRenderer.positionCount = puntos.Count;
        lineRenderer.SetPositions(puntos.ToArray());
    }

    // Método para ocultar la línea si es necesario.
    public void OcultarLinea()
    {
        lineRenderer.positionCount = 0;
    }
}