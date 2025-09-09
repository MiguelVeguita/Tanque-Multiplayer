using UnityEngine;
using TMPro; // �Importante! A�ade esta l�nea para poder usar TextMeshPro

public class ContadorFPS : MonoBehaviour
{
    [Header("Referencias de UI")]
    [Tooltip("Arrastra aqu� el objeto de texto (TextMeshPro) desde tu Canvas.")]
    public TextMeshProUGUI textoFPS;

    [Header("Configuraci�n")]
    [Tooltip("Con qu� frecuencia (en segundos) se actualizar� el texto. Un valor m�s bajo es m�s preciso pero consume m�s recursos.")]
    public float tiempoDeActualizacion = 0.5f;

    private float temporizador;

    // Eliminamos el m�todo OnGUI() por completo.

    void Update()
    {
        // Restamos el tiempo que ha pasado desde el �ltimo frame.
        temporizador -= Time.unscaledDeltaTime;

        // Si el temporizador llega a cero o menos, es hora de actualizar el texto.
        if (temporizador <= 0f)
        {
            // Calculamos los FPS a partir del tiempo del �ltimo frame.
            float fps = 1f / Time.unscaledDeltaTime;

            // Verificamos si la referencia al texto est� asignada antes de usarla.
            if (textoFPS != null)
            {
                // Actualizamos el contenido del texto.
                // Usamos {0:0} para mostrar el n�mero como un entero, sin decimales.
                textoFPS.text = string.Format("{0:0} FPS", fps);
            }

            // Reiniciamos el temporizador para la siguiente actualizaci�n.
            temporizador = tiempoDeActualizacion;
        }
    }
}

