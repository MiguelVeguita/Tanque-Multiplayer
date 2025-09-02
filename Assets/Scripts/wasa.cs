using UnityEngine;
using UnityEngine.SceneManagement;

public class wasa : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void game2player()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void game4player()
    {
        SceneManager.LoadScene("game4player");
    }
}
