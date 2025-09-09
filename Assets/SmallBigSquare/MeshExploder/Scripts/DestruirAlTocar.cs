using SBS.ME;
using UnityEngine;

public class DestruirAlTocar : MonoBehaviour
{
    private bool haExplotado = false;
    private MeshExploder exploder;

    void Start()
    {
        // Obtenemos la referencia al componente MeshExploder al inicio
        exploder = GetComponent<SBS.ME.MeshExploder>();
    }

    // Esta función se llama automáticamente cuando otro collider entra en contacto con este
    private void OnCollisionEnter(Collision collision)
    {
        // Verificamos si no ha explotado ya y si el objeto con el que chocamos tiene el tag "Player"
        if (!haExplotado && collision.gameObject.CompareTag("Player"))
        {
            // Marcamos que ya ha explotado para evitar que se llame múltiples veces
            haExplotado = true;

            // ¡Llamamos a la función pública del otro script para iniciar la explosión!
            exploder.EXPLODE();
        }
    }
}