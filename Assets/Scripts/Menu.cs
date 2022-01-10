
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private float tmp;
    public void Pause()
    {
        Time.timeScale = tmp - Time.timeScale;
    }

    // Use this for initialization
    void Awake ()
    {

        tmp = Time.timeScale;
    }
    public void newGame()
    {
        SceneManager.LoadScene("Game3d");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    
    public void loadScene(String scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void cameraToRight(float x)
    {
        
        if(Camera.main.GetComponent<Transform>().position.x < 
           Math.Max(GameObject.Find("Unit_Main").GetComponent<Transform>().position.x, 
               GameObject.Find("eUnit_Main").GetComponent<Transform>().position.x) - 10)
            Camera.main.GetComponent<Transform>().Translate(x,0,0);
    }
    public void cameraToLeft(float x)
    {
        Debug.Log(GameObject.Find("Unit_Main").GetComponent<Transform>().position.x);
        Debug.Log(GameObject.Find("eUnit_Main").GetComponent<Transform>().position.x);
        if(Camera.main.GetComponent<Transform>().position.x > 
           Math.Min(GameObject.Find("Unit_Main").GetComponent<Transform>().position.x, 
               GameObject.Find("eUnit_Main").GetComponent<Transform>().position.x) + 10)
            Camera.main.GetComponent<Transform>().Translate(-x,0,0);
    }
}