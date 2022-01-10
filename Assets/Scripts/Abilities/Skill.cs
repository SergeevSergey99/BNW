using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public GameObject Resourses;
    public int cost = 0;
    public int reloadTime = 0;
    private int reloadTime_now = 0;

    public GameObject skill;

    private void Start()
    {
        gameObject.transform.GetChild(0).GetComponent<Text>().text = cost.ToString();

    }
    void FixedUpdate()
    {

        if (reloadTime >= reloadTime_now)
        {
            reloadTime_now++;
            gameObject.GetComponent<Image>().fillAmount = reloadTime_now / (float)reloadTime;
        }
    }
    public void Active()
    {            
        int res = Int32.Parse(Resourses.GetComponent<Text>().text);
        if (res >= cost && Time.timeScale > 0 && reloadTime_now >= reloadTime)
        {
            reloadTime_now = 0;
            Instantiate(skill, GameObject.Find("Main Camera").transform);
            res -= cost;
            Resourses.GetComponent<Text>().text = res.ToString();
        }
    }
}
