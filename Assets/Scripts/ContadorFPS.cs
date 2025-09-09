using UnityEngine;
using TMPro; // ¡Importante! Añade esta línea para poder usar TextMeshPro

public class ContadorFPS : MonoBehaviour
{
    [Header("Referencias de UI")]
    [Tooltip("Arrastra aquí el objeto de texto (TextMeshPro) desde tu Canvas.")]
    public TextMeshProUGUI textoFPS;

    [Header("Configuración")]
    [Tooltip("Con qué frecuencia (en segundos) se actualizará el texto. Un valor más bajo es más preciso pero consume más recursos.")]
    public float tiempoDeActualizacion = 0.5f;

    private float temporizador;

    // Eliminamos el método OnGUI() por completo.

    void Update()
    {
        // Restamos el tiempo que ha pasado desde el último frame.
        temporizador -= Time.unscaledDeltaTime;

        // Si el temporizador llega a cero o menos, es hora de actualizar el texto.
        if (temporizador <= 0f)
        {
            // Calculamos los FPS a partir del tiempo del último frame.
            float fps = 1f / Time.unscaledDeltaTime;

            // Verificamos si la referencia al texto está asignada antes de usarla.
            if (textoFPS != null)
            {
                // Actualizamos el contenido del texto.
                // Usamos {0:0} para mostrar el número como un entero, sin decimales.
                textoFPS.text = string.Format("{0:0} FPS", fps);
            }

            // Reiniciamos el temporizador para la siguiente actualización.
            temporizador = tiempoDeActualizacion;
        }
    }
}

