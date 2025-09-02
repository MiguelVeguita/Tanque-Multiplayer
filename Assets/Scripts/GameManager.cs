using System;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int quantity=4;
    [SerializeField] GameObject OnWin, OnLose;
    [SerializeField] TextMeshProUGUI timer;
    public float time = 60;
    void Start()
    {
        OnWin.SetActive(false);
        OnLose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quantity == 0)
        {
            OnWin.SetActive(true);
        }
        else if (time <= 0)
        {
            OnLose.SetActive(true);
        }
        timer.text = time.ToString("F0");
        ReduceTime();
    }
    void ReduceTime()
    {
        time -= Time.deltaTime;
    }
    private void OnEnable()
    {
        Musulman.OnMusulmDeath += contador;
    }
    private void OnDisable()
    {
        Musulman.OnMusulmDeath -= contador;

    }

    public void contador()
    {
        quantity--;
    }
    public void ResetScene()

    {

        SceneManager.LoadScene("SampleScene");

    }
}
