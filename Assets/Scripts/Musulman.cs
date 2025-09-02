using System;
using UnityEngine;
using UnityEngine.AI;

public class Musulman : MonoBehaviour
{
    [Header("Estad�sticas de Vida")]
    public int vidaMaxima = 3;
    private int vidaActual;

    [Header("Configuraci�n de IA")]
    [Tooltip("El radio en el que el enemigo detectar� a los jugadores.")]
    public float rangoDeDeteccion = 20f;
    [Tooltip("El da�o que hace el enemigo al chocar con el jugador.")]
    public int danoAlJugador = 1;

    [Header("Referencias")]
    [Tooltip("Referencia al objeto de la barra de vida que est� sobre la cabeza.")]
    public BarraDeVidaEnemigo barraDeVida; // Script que crearemos en la Parte 2

    // Evento est�tico que se dispara cuando un enemigo muere.
    public static event Action OnMusulmDeath;

    private NavMeshAgent agent;
    private Transform objetivo;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // Buscamos al jugador por su tag al iniciar.
        objetivo = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        // Al iniciar, la vida est� al m�ximo.
        vidaActual = vidaMaxima;
        // Actualizamos la barra de vida para que muestre la vida completa.
        barraDeVida.ActualizarBarraDeVida(vidaActual, vidaMaxima);
    }

    void Update()
    {
        // Si no tenemos objetivo o est� muy lejos, no hacemos nada.
        if (objetivo == null) return;

        float distanciaAlObjetivo = Vector3.Distance(transform.position, objetivo.position);

        // Si el jugador est� dentro del rango de detecci�n, lo perseguimos.
        if (distanciaAlObjetivo <= rangoDeDeteccion)
        {
            agent.SetDestination(objetivo.position);
        }
        else
        {
            // Si est� muy lejos, nos detenemos.
            agent.SetDestination(transform.position);
        }
    }

    public void RecibirDano(int cantidad)
    {
        vidaActual -= cantidad;
        barraDeVida.ActualizarBarraDeVida(vidaActual, vidaMaxima);

        if (vidaActual <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        // Disparamos el evento para que otros scripts (como un contador de puntos) se enteren.
        OnMusulmDeath?.Invoke();
        // Destruimos el objeto del enemigo.
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider collision)
    {
        // Si chocamos con una bala, recibimos da�o.
        if (collision.gameObject.CompareTag("Bala"))
        {
            RecibirDano(1); // Asumimos que cada bala hace 1 de da�o.
            Destroy(collision.gameObject); // Destruimos la bala.
        }

        // Si chocamos con el jugador...
        if (collision.gameObject.CompareTag("Player"))
        {
            // ...le hacemos da�o al jugador.
            VidaDelJugador vidaJugador = collision.gameObject.GetComponent<VidaDelJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDano(danoAlJugador);
            }

            // Y nos autodestruimos.
            Morir();
        }
    }
    
}
