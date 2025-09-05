using UnityEngine;

public class ConfiguracionJuego : MonoBehaviour
{
    // Usamos un "Singleton" para que sea f�cil de acceder desde cualquier script.
    public static ConfiguracionJuego Instancia;

    // Esta es la variable que guardar� nuestra elecci�n.
    // La inicializamos en 4 por si acaso.
    public int numeroDeJugadores = 4;

    private void Awake()
    {
        // L�gica del Singleton para que no se duplique.
        if (Instancia == null)
        {
            Instancia = this;
            // �La magia! Esto evita que el objeto se destruya al cargar una nueva escena.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Si ya existe una instancia, destruimos esta para no tener duplicados.
            Destroy(gameObject);
        }
    }
}