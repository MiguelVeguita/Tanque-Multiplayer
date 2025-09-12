using UnityEngine;
using UnityEngine.SceneManagement;

public class managerbotones : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reniciar(string name)
    {
        SceneManager.LoadScene(name);
    }
    public void salir(string name)
    {
        SceneManager.LoadScene(name);

    }
}
