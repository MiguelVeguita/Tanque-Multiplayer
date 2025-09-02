using System;
using TMPro;
using UnityEngine;

using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    public int quantity=4;
    [SerializeField] GameObject OnWin, OnLose;
    [SerializeField] TextMeshProUGUI timer,Team1,Team2;
    public float time = 60;
    public int team1=0;
    public int team2=0;
    void Start()
    {
        OnWin.SetActive(false);
        OnLose.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Team1.text = team1.ToString();
        Team2.text = team2.ToString();
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
        Musulman.Puntaje += score;
    }
    private void OnDisable()
    {
        Musulman.OnMusulmDeath -= contador;
        Musulman.Puntaje -= score;
    }

    public void contador()
    {
        quantity--;
    }
    public void score(int i)
    {
        if (i == 1)
        {
            team1 ++;
        }
        else if (i == 2)
        {
            team2 ++;
        }
    }
    
    public void ResetScene()

    {

        SceneManager.LoadScene("SampleScene");

    }
}
