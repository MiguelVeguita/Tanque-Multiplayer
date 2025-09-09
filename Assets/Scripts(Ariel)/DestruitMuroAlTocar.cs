using SBS.ME;
using UnityEngine;

public class DestruirMuroAlTocar : MonoBehaviour
{
    private ReactiveMeshExploder reactiveExploder;

    void Start()
    {
        // Guardamos la referencia a nuestro exploder reactivo.
        reactiveExploder = GetComponent<SBS.ME.ReactiveMeshExploder>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Solo reaccionamos al primer choque con el Player.
        if (reactiveExploder != null && collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hehehe");
            // Obtenemos el punto exacto del impacto.
            Vector3 puntoDeImpacto = collision.GetContact(0).point;

            // Llamamos a nuestra función especializada en el exploder reactivo.
            reactiveExploder.EXPLODE_AtPoint(puntoDeImpacto);

            // Desactivamos este script para que no se ejecute de nuevo.
            this.enabled = false;
        }
    }
}