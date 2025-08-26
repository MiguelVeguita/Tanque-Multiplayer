using UnityEngine;
using System;

public class Musulman : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static event Action OnMusulmDeath;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bala")
        {
            OnMusulmDeath?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
