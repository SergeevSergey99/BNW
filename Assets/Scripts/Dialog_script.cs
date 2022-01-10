using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog_script : MonoBehaviour
{
    private float tmp;
    public List<GameObject> dialogs;
    private int currI = 0;
    public void Pause()
    {
        Time.timeScale = tmp - Time.timeScale;
    }

    // Use this for initialization
    void Start ()
    {

        tmp = Time.timeScale;
        Time.timeScale = tmp - Time.timeScale;
        int i = 0;
        foreach (var dialog in dialogs)
        {
            
            if (i != 0)
            {
                gameObject.transform.GetChild(i).gameObject.SetActive(false);
            }

            i++;
        }

    }

    public void next()
    {
        dialogs[currI].SetActive(false);
        currI++;
        if (currI >= dialogs.Count)
        {
            Time.timeScale = tmp - Time.timeScale;
            
            Destroy(this.transform.parent.gameObject);
            return;
        }
        dialogs[currI].SetActive(true);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
