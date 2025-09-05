using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Este método lo llamarás desde el botón "2 Jugadores".
    public void SetDosJugadores(string nombreDeLaEscena)
    {
        if (ConfiguracionJuego.Instancia != null)
        {
            ConfiguracionJuego.Instancia.numeroDeJugadores = 2;
            Debug.Log("Modo de juego configurado para 2 jugadores.");
            SceneManager.LoadScene(nombreDeLaEscena);
        }
    }

    // Este método lo llamarás desde el botón "4 Jugadores".
    public void SetCuatroJugadores(string nombreDeLaEscena)
    {
        if (ConfiguracionJuego.Instancia != null)
        {
            ConfiguracionJuego.Instancia.numeroDeJugadores = 4;
            Debug.Log("Modo de juego configurado para 4 jugadores.");
            SceneManager.LoadScene(nombreDeLaEscena);
        }
    }

  
}