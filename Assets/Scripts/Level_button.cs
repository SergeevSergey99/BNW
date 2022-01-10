using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level_button : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isButton = true;
    void Start()
    {
        if(isButton)
            if (!GameManagerScript.UnlockedLevels.Contains(requirements))
            {
                Color c = gameObject.GetComponent<Image>().color;
                c.a = 0.3f;
                gameObject.GetComponent<Image>().color = c;
            }  
    }

    public int requirements = 0;

    public void AddRequirementsToList()
    {
        if(!GameManagerScript.UnlockedLevels.Contains(requirements))
            GameManagerScript.UnlockedLevels.Add(requirements);
    }
    public void loadScene(String scene)
    {
        Debug.Log("requirements for this level = " + requirements);
        if(GameManagerScript.UnlockedLevels.Contains(requirements))
            SceneManager.LoadScene(scene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
