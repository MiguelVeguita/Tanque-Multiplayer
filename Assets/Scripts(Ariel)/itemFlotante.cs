using UnityEngine;

public class ItemFlotante : MonoBehaviour
{
    [Header("Configuraci�n de Rotaci�n")]
    [Tooltip("La velocidad a la que el objeto rotar� en cada eje (X, Y, Z).")]
    public Vector3 velocidadDeRotacion = new Vector3(0f, 45f, 0f);

    [Header("Configuraci�n de Flotaci�n")]
    [Tooltip("La altura en metros que el objeto subir� y bajar� desde su punto de origen.")]
    public float amplitudDeFlotacion = 0.5f;

    [Tooltip("La rapidez con la que el objeto sube y baja. Un valor m�s alto significa un movimiento m�s r�pido.")]
    public float frecuenciaDeFlotacion = 1f;

    // Guardaremos la posici�n inicial para calcular el movimiento a partir de ella.
    private Vector3 posicionInicial;

    // Se ejecuta una sola vez cuando el objeto es activado.
    void Start()
    {
        // Almacenamos la posici�n original del objeto.
        posicionInicial = transform.position;
    }

    // Se ejecuta en cada frame.
    void Update()
    {
        // --- 1. Rotaci�n del objeto ---
        // Rotamos el objeto de forma constante. Se multiplica por Time.deltaTime
        // para que la velocidad de rotaci�n sea independiente de los FPS del juego.
        transform.Rotate(velocidadDeRotacion * Time.deltaTime);

        // --- 2. Flotaci�n del objeto (usando una onda senoidal) ---
        // Calculamos el desplazamiento vertical usando la funci�n Seno para un movimiento suave.
        float desplazamientoVertical = Mathf.Sin(Time.time * frecuenciaDeFlotacion) * amplitudDeFlotacion;

        // Creamos la nueva posici�n combinando la posici�n inicial con el desplazamiento.
        transform.position = posicionInicial + new Vector3(0, desplazamientoVertical, 0);
    }
}