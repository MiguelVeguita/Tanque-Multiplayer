using UnityEngine;
using UnityEngine.InputSystem;
using TMPro; // �Importante! A�ade esto para la UI de la munici�n

public class ControladorTorreta : MonoBehaviour
{
    [Header("Referencias de Objetos")]
    [Tooltip("El objeto de la torreta que rota horizontalmente.")]
    public Transform torreta;
    [Tooltip("El objeto del ca��n que se inclina verticalmente.")]
    public Transform ca�on;
    public Transform puntoDeDisparo;
    public GameObject proyectilPrefab;

    [Header("Configuraci�n de Rotaci�n")]
    public float velocidadRotacionHorizontal = 50.0f;
    public float velocidadRotacionVertical = 25.0f;

    [Header("L�mites de Inclinaci�n")]
    [Tooltip("El �ngulo m�ximo en grados que el ca��n puede subir.")]
    public float anguloMaximoSubida = 45.0f;
    [Tooltip("El �ngulo m�ximo en grados que el ca��n puede bajar (usar un valor negativo).")]
    public float anguloMinimoBajada = -10.0f;

    [Header("Configuraci�n de Disparo")]
    public float fuerzaDisparo = 1500f;

    [Header("Munici�n")]
    public int municionMaxima = 20;
    private int municionActual;
    public TextMeshProUGUI textoMunicion; // Arrastra aqu� el texto de la munici�n

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
        // Al empezar, la munici�n est� al m�ximo
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
        // Solo disparamos si se presion� el bot�n Y si nos queda munici�n.
        if (!context.performed || municionActual <= 0)
        {
            // Opcional: puedes poner un sonido de "sin balas" aqu�.
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
        // Rotaci�n horizontal (torreta)
        float rotacionHorizontal = direccionApuntado.x * velocidadRotacionHorizontal * Time.deltaTime;
        torreta.Rotate(0, rotacionHorizontal, 0, Space.Self);

        // Rotaci�n vertical (ca��n)
        float cambioDeAngulo = -direccionApuntado.y * velocidadRotacionVertical * Time.deltaTime;
        rotacionVerticalActual += cambioDeAngulo;
        rotacionVerticalActual = Mathf.Clamp(rotacionVerticalActual, anguloMinimoBajada, anguloMaximoSubida);
        ca�on.localRotation = Quaternion.Euler(rotacionVerticalActual, 0, 0);

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

    // M�todo para actualizar el texto de la munici�n en la pantalla
    void ActualizarTextoMunicion()
    {
        if (textoMunicion != null)
        {
            textoMunicion.text = "Munici�n: " + municionActual;
        }
    }

    // M�todo p�blico para que los power-ups puedan darnos munici�n
    public void AnadirMunicion(int cantidad)
    {
        municionActual += cantidad;
        // Nos aseguramos de no pasarnos del m�ximo
        municionActual = Mathf.Min(municionActual, municionMaxima);
        ActualizarTextoMunicion();
    }
}

