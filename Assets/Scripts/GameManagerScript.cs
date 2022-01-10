using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

public class GameManagerScript 
{
    public static GameManagerScript instance;

    //public static int points = 100;
    public static bool isSet = false;
    public static int Money = 0;
    public static GameObject General;
    public static List<GameObject> Army;
    public static List<int> UnlockedLevels;
/*
    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }*/
    
    public void SetGeneralAndArmy(GameObject G, List<GameObject> A)
    {
        General = G;
        Army = A;
    }
}