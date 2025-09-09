using UnityEngine;
using System;
using System.Collections;
public class TempPlayer : MonoBehaviour
{

    [SerializeField] int munition = 5;
   [SerializeField] int currentSpeed = 0;
    [SerializeField] int speedBase = 5;

    void Start()
    {
        Debug.Log("a");
    }

    void Update()
    {
        if(munition <= 0)
        {
            //no Shoot
        }
    }
    private void OnEnable()
    {
        MunitionPowerUp.OnMunitionUpdate += IncreaseLife;
        SpeedPowerUP.OnSpeedUpdate += IncreaseSpeed;
        Debug.Log("enable");
    }
    private void OnDisable()
    {
        MunitionPowerUp.OnMunitionUpdate -= IncreaseLife;
        SpeedPowerUP.OnSpeedUpdate -= IncreaseSpeed;
        Debug.Log("disable");
    }
    void IncreaseLife(int munitionQuantity)
    {
        munition += munitionQuantity;
    }
    void IncreaseSpeed(int updateSpeed, float duration)
    {
       StartCoroutine(SpeedActivation(updateSpeed, duration));
    }

    IEnumerator SpeedActivation(int updateSpeed, float duration)
    {
        currentSpeed = speedBase + updateSpeed;
        Debug.Log(currentSpeed);
        yield return new WaitForSeconds(duration);

        currentSpeed = speedBase;
        Debug.Log(currentSpeed);
    }

}
