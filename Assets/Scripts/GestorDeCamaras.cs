using UnityEngine;
using System.Collections.Generic; // Para usar Listas

public class GestorDeCamaras : MonoBehaviour
{
    // Usaremos una clase anidada para organizar mejor las referencias en el Inspector.
    [System.Serializable]
    public class SetupJugador
    {
        public GameObject objetoJugador;
        public Camera camaraJugador;
    }

    // Creamos una lista pública para arrastrar nuestros jugadores y sus cámaras.
    public List<SetupJugador> jugadores = new List<SetupJugador>();

    void Start()
    {
        // Verificamos si nuestra configuración existe.
        if (ConfiguracionJuego.Instancia != null)
        {
            // Leemos el número de jugadores y llamamos al método correspondiente.
            if (ConfiguracionJuego.Instancia.numeroDeJugadores == 2)
            {
                ConfigurarParaDosJugadores();
            }
            else // Por defecto o si es 4
            {
                ConfigurarParaCuatroJugadores();
            }
        }
        else
        {
            // Si ejecutamos la escena directamente sin pasar por el menú, usamos 4 jugadores.
            Debug.LogWarning("No se encontró ConfiguracionJuego. Se usará el layout por defecto de 4 jugadores.");
            ConfigurarParaCuatroJugadores();
        }
    }

    void ConfigurarParaDosJugadores()
    {
        Debug.Log("Configurando cámaras para 2 jugadores (vertical).");

        // --- JUGADOR 1 (Izquierda) ---
        jugadores[0].objetoJugador.SetActive(true);
        // Rect(X, Y, Ancho, Alto) -> (0, 0, 0.5, 1) significa que empieza en la esquina
        // inferior izquierda y ocupa la mitad del ancho y toda la altura.
        jugadores[0].camaraJugador.rect = new Rect(0f, 0f, 0.5f, 1f);

        // --- JUGADOR 2 (Derecha) ---
        jugadores[1].objetoJugador.SetActive(true);
        // (0.5, 0, 0.5, 1) significa que empieza a la mitad de la pantalla
        // y ocupa la otra mitad del ancho y toda la altura.
        jugadores[1].camaraJugador.rect = new Rect(0.5f, 0f, 0.5f, 1f);

        // --- DESACTIVAMOS JUGADORES 3 Y 4 ---
        jugadores[2].objetoJugador.SetActive(false);
        jugadores[3].objetoJugador.SetActive(false);
    }

    void ConfigurarParaCuatroJugadores()
    {
        Debug.Log("Configurando cámaras para 4 jugadores.");

        // --- JUGADOR 1 (Arriba-Izquierda) ---
        jugadores[0].objetoJugador.SetActive(true);
        jugadores[0].camaraJugador.rect = new Rect(0f, 0.5f, 0.5f, 0.5f);

        // --- JUGADOR 2 (Arriba-Derecha) ---
        jugadores[1].objetoJugador.SetActive(true);
        jugadores[1].camaraJugador.rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);

        // --- JUGADOR 3 (Abajo-Izquierda) ---
        jugadores[2].objetoJugador.SetActive(true);
        jugadores[2].camaraJugador.rect = new Rect(0f, 0f, 0.5f, 0.5f);

        // --- JUGADOR 4 (Abajo-Derecha) ---
        jugadores[3].objetoJugador.SetActive(true);
        jugadores[3].camaraJugador.rect = new Rect(0.5f, 0f, 0.5f, 0.5f);
    }
}