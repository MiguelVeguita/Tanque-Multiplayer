using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class ControladorMovimiento : MonoBehaviour
{
    [Header("Configuraci�n de Movimiento")]
    [Tooltip("La velocidad de avance y retroceso del tanque.")]
    public float velocidad = 10.0f;
    [SerializeField] float currentSpeed = 0;

    [Header("Configuraci�n de Rotaci�n")]
    [Tooltip("La velocidad con la que el chasis del tanque gira sobre su eje.")]
    public float velocidadRotacion = 100.0f;

    private Rigidbody rb;
    private Vector2 direccionMovimiento;
    private void OnEnable()
    {
        SpeedPowerUP.OnSpeedUpdate += IncreaseSpeed;
        Debug.Log("enable");
    }
    private void OnDisable()
    {
        SpeedPowerUP.OnSpeedUpdate -= IncreaseSpeed;
        Debug.Log("disable");
    }
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Congelamos la rotaci�n en X y Z para evitar que el tanque se vuelque,
        // pero dejamos el eje Y libre para poder rotar.
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Mover(InputAction.CallbackContext context)
    {
        direccionMovimiento = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        // --- L�GICA DE MOVIMIENTO (AVANCE/RETROCESO) ---

        // Usamos el input VERTICAL (W/S o stick arriba/abajo) para el movimiento.
        // El movimiento siempre es en la direcci�n "hacia adelante" del tanque (transform.forward).
        // Time.fixedDeltaTime es importante para que el movimiento sea consistente sin importar el framerate.
        if (currentSpeed > velocidad)
        {
            Vector3 movimiento = transform.forward * direccionMovimiento.y * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movimiento);
        }
        else
        {
            Vector3 movimiento = transform.forward * direccionMovimiento.y * velocidad * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movimiento);
        }


        // Usamos MovePosition para aplicar el movimiento respetando las f�sicas.
       // rb.MovePosition(rb.position + movimiento);


        // --- L�GICA DE ROTACI�N (GIRO) ---

        // Usamos el input HORIZONTAL (A/D o stick izq/der) para la rotaci�n.
        float giro = direccionMovimiento.x * velocidadRotacion * Time.fixedDeltaTime;

        // Creamos una rotaci�n alrededor del eje Y (el eje vertical).
        Quaternion rotacion = Quaternion.Euler(0f, giro, 0f);

        // Usamos MoveRotation para aplicar la rotaci�n al Rigidbody.
        // Multiplicar quaternions combina sus rotaciones.
        rb.MoveRotation(rb.rotation * rotacion);
    }
    void IncreaseSpeed(int updateSpeed, float duration)
    {
        StartCoroutine(SpeedActivation(updateSpeed, duration));
    }

    IEnumerator SpeedActivation(int updateSpeed, float duration)
    {
        currentSpeed = velocidad + updateSpeed;
        Debug.Log(currentSpeed);
        yield return new WaitForSeconds(duration);

        currentSpeed = velocidad;
        Debug.Log(currentSpeed);
    }


}