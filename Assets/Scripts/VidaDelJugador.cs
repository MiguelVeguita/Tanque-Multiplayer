using UnityEngine;
using UnityEngine.UI; // Para controlar el Slider
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // Para reiniciar el nivel

public class VidaDelJugador : MonoBehaviour
{
    [Header("Estadísticas de Vida")]
    public int vidaMaxima = 10;
    public int vidaActual;

    [Header("Referencias de UI")]
    public Slider barraDeVidaUI;

    public int id;

    public GameObject cam,panel,panel2;
    public ControladorMovimiento script;

    void Start()
    {
       script.enabled = true; ;
        cam.gameObject.SetActive(false);
        vidaActual = vidaMaxima;
        ActualizarBarraDeVida();
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        ActualizarBarraDeVida();

        if (vidaActual <= 0)
        {

            Morir(id);
        }
    }
 
    void ActualizarBarraDeVida()
    {
        if (barraDeVidaUI != null)
        {
            barraDeVidaUI.value = (float)vidaActual / vidaMaxima;
        }
    }

    void Morir(int id)
    {
        script.enabled = false; ;
        if (id == 1)
        {
            cam.gameObject.SetActive(true);
            panel.SetActive(true);
        }
        else
        {
            cam.gameObject.SetActive(true);

            panel2.SetActive(true);
        }
        Debug.Log("¡El jugador ha muerto!");
       
    }

    public void OnTriggerEnter(Collider other)
    {
        // Si una Bala1 nos golpea...
        if (other.gameObject.CompareTag("Bala1"))
        {

            if (id == 2)
            {
                RecibirDano(1);
                Debug.Log("¡El jugador " + id + " recibió daño de una Bala1!");
                Destroy(other.gameObject); 
            }
        }

        if (other.gameObject.CompareTag("Bala2"))
        {
           
            if (id == 1)
            {
                RecibirDano(1);
                Debug.Log("¡El jugador " + id + " recibió daño de una Bala2!");
                Destroy(other.gameObject);
            }
        }
    }
}
