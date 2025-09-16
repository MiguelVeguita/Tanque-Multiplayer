using UnityEngine;

public class ItemFlotante : MonoBehaviour
{
    [Header("Configuración de Rotación")]
    [Tooltip("La velocidad a la que el objeto rotará en cada eje (X, Y, Z).")]
    public Vector3 velocidadDeRotacion = new Vector3(0f, 45f, 0f);

    [Header("Configuración de Flotación")]
    [Tooltip("La altura en metros que el objeto subirá y bajará desde su punto de origen.")]
    public float amplitudDeFlotacion = 0.5f;

    [Tooltip("La rapidez con la que el objeto sube y baja. Un valor más alto significa un movimiento más rápido.")]
    public float frecuenciaDeFlotacion = 1f;

    // Guardaremos la posición inicial para calcular el movimiento a partir de ella.
    private Vector3 posicionInicial;

    // Se ejecuta una sola vez cuando el objeto es activado.
    void Start()
    {
        // Almacenamos la posición original del objeto.
        posicionInicial = transform.position;
    }

    // Se ejecuta en cada frame.
    void Update()
    {
        // --- 1. Rotación del objeto ---
        // Rotamos el objeto de forma constante. Se multiplica por Time.deltaTime
        // para que la velocidad de rotación sea independiente de los FPS del juego.
        transform.Rotate(velocidadDeRotacion * Time.deltaTime);

        // --- 2. Flotación del objeto (usando una onda senoidal) ---
        // Calculamos el desplazamiento vertical usando la función Seno para un movimiento suave.
        float desplazamientoVertical = Mathf.Sin(Time.time * frecuenciaDeFlotacion) * amplitudDeFlotacion;

        // Creamos la nueva posición combinando la posición inicial con el desplazamiento.
        transform.position = posicionInicial + new Vector3(0, desplazamientoVertical, 0);
    }
}