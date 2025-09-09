using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // ¡Importante! Añade esto para la UI de la munición

public class ControladorTorreta : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    [Tooltip("El objeto de la torreta que rota horizontalmente.")]
    public Transform torreta;
    [Tooltip("El objeto del cañón que se inclina verticalmente.")]
    public Transform cañon;
    public Transform puntoDeDisparo;
    public GameObject proyectilPrefab;

    [Header("Configuración de Rotación")]
    public float velocidadRotacionHorizontal = 50.0f;
    public float velocidadRotacionVertical = 25.0f;

    [Header("Límites de Inclinación")]
    [Tooltip("El ángulo máximo en grados que el cañón puede subir.")]
    public float anguloMaximoSubida = 45.0f;
    [Tooltip("El ángulo máximo en grados que el cañón puede bajar (usar un valor negativo).")]
    public float anguloMinimoBajada = -10.0f;

    [Header("Configuración de Disparo")]
    public float fuerzaDisparo = 1500f;

    [Header("Munición")]
    public int municionMaxima = 20;
    private int municionActual;
    public TextMeshProUGUI textoMunicion; // Arrastra aquí el texto de la munición

    // ... (otras variables privadas)
    private Vector2 direccionApuntado;
    private float rotacionVerticalActual = 0f;
    public ProyectorDeTrayectoria proyector;
    public Rigidbody proyectilRbPrefab;

    private void OnEnable()
    {
        MunitionPowerUp.OnMunitionUpdate += IncreaseMuni;
        Debug.Log("enable");
    }
    private void OnDisable()
    {
        MunitionPowerUp.OnMunitionUpdate -= IncreaseMuni;
        Debug.Log("disable");
    }
    void Start()
    {
        // Al empezar, la munición está al máximo
        municionActual = municionMaxima;
        ActualizarTextoMunicion();
    }
    void IncreaseMuni(int munitionQuantity)
    {
        municionActual += munitionQuantity;
        ActualizarTextoMunicion();

    }
    public void Disparar(InputAction.CallbackContext context)
    {
        // --- CONDICIONAL NUEVA ---
        // Solo disparamos si se presionó el botón Y si nos queda munición.
        if (!context.performed || municionActual <= 0)
        {
            // Opcional: puedes poner un sonido de "sin balas" aquí.
            return;
        }

        // Restamos una bala
        municionActual--;
        ActualizarTextoMunicion(); // Actualizamos la UI

        GameObject proyectil = Instantiate(proyectilPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
        Rigidbody rbProyectil = proyectil.GetComponent<Rigidbody>();
        rbProyectil.AddForce(puntoDeDisparo.forward * fuerzaDisparo, ForceMode.Impulse);
        Destroy(proyectil, 5.0f);
    }

    public void RotarTorreta(InputAction.CallbackContext context)
    {
        direccionApuntado = context.ReadValue<Vector2>();
    }

    void Update()
    {
        // Rotación horizontal (torreta)
        float rotacionHorizontal = direccionApuntado.x * velocidadRotacionHorizontal * Time.deltaTime;
        torreta.Rotate(0, rotacionHorizontal, 0, Space.Self);

        // Rotación vertical (cañón)
        float cambioDeAngulo = -direccionApuntado.y * velocidadRotacionVertical * Time.deltaTime;
        rotacionVerticalActual += cambioDeAngulo;
        rotacionVerticalActual = Mathf.Clamp(rotacionVerticalActual, anguloMinimoBajada, anguloMaximoSubida);
        cañon.localRotation = Quaternion.Euler(rotacionVerticalActual, 0, 0);

        ActualizarProyeccion();
    }

    void ActualizarProyeccion()
    {
        if (proyector != null && proyectilRbPrefab != null)
        {
            Vector3 velocidadInicial = puntoDeDisparo.forward * (fuerzaDisparo / proyectilRbPrefab.mass);
            proyector.DibujarTrayectoria(puntoDeDisparo.position, velocidadInicial);
        }
    }

    // Método para actualizar el texto de la munición en la pantalla
    void ActualizarTextoMunicion()
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = "Munición: " + municionActual;
        }
    }

    // Método público para que los power-ups puedan darnos munición
    public void AnadirMunicion(int cantidad)
    {
        municionActual += cantidad;
        // Nos aseguramos de no pasarnos del máximo
        municionActual = Mathf.Min(municionActual, municionMaxima);
        ActualizarTextoMunicion();
    }
}

