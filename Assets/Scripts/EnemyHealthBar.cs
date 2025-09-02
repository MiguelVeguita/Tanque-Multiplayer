using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaEnemigo : MonoBehaviour
{
    public Slider slider;
    private Transform camara;

    void Start()
    {
        // Encontramos la c�mara principal al inicio.
        camara = Camera.main.transform;
    }

    // Usamos LateUpdate para asegurarnos de que se ejecuta despu�s de que la c�mara se haya movido.
    void LateUpdate()
    {
        // Hacemos que la barra de vida siempre mire hacia la c�mara (efecto "billboard").
        transform.LookAt(transform.position + camara.forward);
    }

    // M�todo para actualizar el valor del slider.
    public void ActualizarBarraDeVida(int vidaActual, int vidaMaxima)
    {
        // El valor del slider va de 0 a 1, as� que normalizamos la vida.
        slider.value = (float)vidaActual / vidaMaxima;
    }
}
