using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Collections;

public class ControladorMovimiento : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [Tooltip("La velocidad de avance y retroceso del tanque.")]
    public float velocidad = 10.0f;
    [SerializeField] float currentSpeed = 0;

    [Header("Configuración de Rotación")]
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

        
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    public void Mover(InputAction.CallbackContext context)
    {
        direccionMovimiento = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        
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


      


        
        float giro = direccionMovimiento.x * velocidadRotacion * Time.fixedDeltaTime;

       
        Quaternion rotacion = Quaternion.Euler(0f, giro, 0f);

      
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