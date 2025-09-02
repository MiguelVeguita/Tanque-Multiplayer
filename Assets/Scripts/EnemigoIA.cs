using UnityEngine;
using UnityEngine.AI;

public class EnemigoIA : MonoBehaviour
{
    [Header("Configuración de IA")]
    [Tooltip("El radio en el que el enemigo detectará a los jugadores.")]
    public float rangoDeDeteccion = 15f;
    [Tooltip("Con qué frecuencia (en segundos) el enemigo buscará al jugador más cercano.")]
    public float intervaloDeBusqueda = 1f;

    // Ahora el objetivo es privado, ya que cambiará dinámicamente.
    private Transform objetivoActual;
    private NavMeshAgent agent;
    private float proximaBusqueda = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // En lugar de buscar siempre, lo hacemos cada cierto intervalo de tiempo para ser más eficientes.
        if (Time.time >= proximaBusqueda)
        {
            BuscarObjetivoMasCercano();
            proximaBusqueda = Time.time + intervaloDeBusqueda;
        }

        // Si tenemos un objetivo válido...
        if (objetivoActual != null)
        {
            // ...le decimos al NavMeshAgent que su destino es la posición del objetivo.
            agent.SetDestination(objetivoActual.position);

            // Si estamos dentro del rango de parada, miramos al objetivo.
            float distanciaAlObjetivo = Vector3.Distance(transform.position, objetivoActual.position);
            if (distanciaAlObjetivo <= agent.stoppingDistance)
            {
                MirarAlObjetivo();
            }
        }
        else
        {
            // Si no hay ningún objetivo, nos detenemos.
            agent.SetDestination(transform.position);
        }
    }

    void BuscarObjetivoMasCercano()
    {
        // 1. Encontrar TODOS los objetos con el tag "Player".
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        Transform mejorObjetivo = null;
        float distanciaMasCercana = Mathf.Infinity; // Empezamos con una distancia infinita.

        // 2. Iterar sobre cada jugador encontrado.
        foreach (GameObject playerGO in players)
        {
            // 3. Calcular la distancia a este jugador.
            float distanciaAlPlayer = Vector3.Distance(transform.position, playerGO.transform.position);

            // 4. Si este jugador está más cerca que el último que revisamos Y está dentro del rango de detección...
            if (distanciaAlPlayer < distanciaMasCercana && distanciaAlPlayer <= rangoDeDeteccion)
            {
                // ...lo guardamos como el mejor objetivo hasta ahora.
                distanciaMasCercana = distanciaAlPlayer;
                mejorObjetivo = playerGO.transform;
            }
        }

        // 5. Al final del bucle, asignamos el mejor objetivo encontrado como nuestro objetivo actual.
        // Si no se encontró a nadie en el rango, "mejorObjetivo" será null.
        objetivoActual = mejorObjetivo;
    }

    void MirarAlObjetivo()
    {
        Vector3 direccion = (objetivoActual.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // Dibuja la esfera de detección en el editor.
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangoDeDeteccion);
    }
}