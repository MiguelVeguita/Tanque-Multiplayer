using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaEnemigo : MonoBehaviour
{
    public Slider slider;
    private Transform camara;

    void Start()
    {
        // Encontramos la cámara principal al inicio.
        camara = Camera.main.transform;
    }

    // Usamos LateUpdate para asegurarnos de que se ejecuta después de que la cámara se haya movido.
    void LateUpdate()
    {
        // Hacemos que la barra de vida siempre mire hacia la cámara (efecto "billboard").
        transform.LookAt(transform.position + camara.forward);
    }

    // Método para actualizar el valor del slider.
    public void ActualizarBarraDeVida(int vidaActual, int vidaMaxima)
    {
        // El valor del slider va de 0 a 1, así que normalizamos la vida.
        slider.value = (float)vidaActual / vidaMaxima;
    }
}
