using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
    // Este m�todo lo llamar�s desde el bot�n "2 Jugadores".
    public void SetDosJugadores(string nombreDeLaEscena)
    {
        if (ConfiguracionJuego.Instancia != null)
        {
            ConfiguracionJuego.Instancia.numeroDeJugadores = 2;
            Debug.Log("Modo de juego configurado para 2 jugadores.");
            SceneManager.LoadScene(nombreDeLaEscena);
        }
    }

    // Este m�todo lo llamar�s desde el bot�n "4 Jugadores".
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