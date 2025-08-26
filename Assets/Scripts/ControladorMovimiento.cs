using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorMovimiento : MonoBehaviour
{
    public float velocidad = 7.0f;

    private Rigidbody rb;
    private Vector2 direccionMovimiento;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

      
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void Mover(InputAction.CallbackContext context)
    {
        direccionMovimiento = context.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        Vector3 movimiento = new Vector3(direccionMovimiento.x, 0f, direccionMovimiento.y);

        rb.linearVelocity = new Vector3(movimiento.x * velocidad, rb.linearVelocity.y, movimiento.z * velocidad);

    }
}