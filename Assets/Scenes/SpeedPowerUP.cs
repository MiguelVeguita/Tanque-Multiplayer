using System;
using UnityEngine;

public class SpeedPowerUP : MonoBehaviour
{
    public static event Action<int, float> OnSpeedUpdate;
    int speedUpdate = 10;
    float speedDuration = 6f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("e");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("collision");
            OnSpeedUpdate?.Invoke(speedUpdate, speedDuration);
        }
        if (other.gameObject.tag == "Player2")
        {
            Debug.Log("collision");
            OnSpeedUpdate?.Invoke(speedUpdate, speedDuration);
        }
    }/*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("collision");
            OnSpeedUpdate?.Invoke(speedUpdate, speedDuration);
        }
        if (collision.gameObject.tag == "Player2" )
        {
            Debug.Log("collision");
            OnSpeedUpdate?.Invoke(speedUpdate, speedDuration);
        }
    }*/

}
