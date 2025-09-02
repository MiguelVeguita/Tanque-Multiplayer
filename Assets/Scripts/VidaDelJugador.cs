using UnityEngine;
using UnityEngine.UI; // Para controlar el Slider
using UnityEngine.SceneManagement; // Para reiniciar el nivel

public class VidaDelJugador : MonoBehaviour
{
    [Header("Estadísticas de Vida")]
    public int vidaMaxima = 10;
    private int vidaActual;

    [Header("Referencias de UI")]
    public Slider barraDeVidaUI;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarraDeVida();
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void ActualizarBarraDeVida()
    {
        if (barraDeVidaUI != null)
        {
            // Normalizamos la vida para el slider (valor de 0 a 1).
            barraDeVidaUI.value = (float)vidaActual / vidaMaxima;
        }
    }

    void Morir()
    {
        // Aquí puedes poner lógica de "Game Over", como reiniciar la escena.
        Debug.Log("¡El jugador ha muerto!");
        // Reinicia la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
