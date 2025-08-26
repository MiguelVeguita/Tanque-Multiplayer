using UnityEngine;
using UnityEngine.InputSystem;

public class ControladorTorreta : MonoBehaviour
{
    public Transform torreta;
    public Transform puntoDeDisparo;
    public GameObject proyectilPrefab;

    public float velocidadRotacionTorreta = 50.0f;
    public float fuerzaDisparo = 1500f;

    private Vector2 direccionApuntado;


    public void RotarTorreta(InputAction.CallbackContext context)
    {
        direccionApuntado = context.ReadValue<Vector2>();
        print(direccionApuntado);
    }

    public void Disparar(InputAction.CallbackContext context)
    {
        print("aa");
        if (context.performed)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDeDisparo.position, puntoDeDisparo.rotation);
            Rigidbody rbProyectil = proyectil.GetComponent<Rigidbody>();
            rbProyectil.AddForce(puntoDeDisparo.forward * fuerzaDisparo);
            Destroy(proyectil, 2.0f);
        }

        
    }

    void Update()
    {
        float rotacionHorizontal = direccionApuntado.x * velocidadRotacionTorreta * Time.deltaTime;
        torreta.Rotate(0, rotacionHorizontal, 0, Space.Self);
    }
}