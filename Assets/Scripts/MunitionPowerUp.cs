using System;
using Unity.VisualScripting;
using UnityEngine;

public class MunitionPowerUp : MonoBehaviour
{
    int munitionQuantity = 10;

    public static event Action<int> OnMunitionUpdate;
    void Start()
    {
        
    }

    
    private void OnDisable()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnMunitionUpdate?.Invoke(munitionQuantity);
           Destroy(this.gameObject);
        }
        if (other.gameObject.tag == "Player2")
        {
            OnMunitionUpdate?.Invoke(munitionQuantity);
            Destroy(this.gameObject);

        }
    }
   
}
