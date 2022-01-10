using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = System.Random;

public class Drager : MonoBehaviour,  IDragHandler, IEndDragHandler
{
    private static GameObject itemBeingDragged;
    public GameObject resourses;
    public Vector3 zeroPoint;
    public int cost = 0;
    public int reloadTime = 0;
    public int reloadTime_now = 0;
    public GameObject unit;
    private Random rand = new Random();

    public bool inArea = false;
    // Use this for initialization
    private void Start()
    {
        
        itemBeingDragged = gameObject;
        zeroPoint = gameObject.GetComponent<RectTransform>().anchoredPosition;
        if (!unit.CompareTag("Actor"))
        {
            cost = 10;
            gameObject.transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
            reloadTime = 5000;
            return;
        }

        reloadTime = unit.GetComponent<Unit1>().reloadTime;
        cost = unit.GetComponent<Unit1>().cost;
        gameObject.transform.GetChild(0).GetComponent<Text>().text = cost.ToString();
    }


    public static GameObject GetDraggedItem()
    {
        return itemBeingDragged;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Drager.itemBeingDragged = gameObject;
//        float k = Screen.currentResolution.height / 733.0f;
        float k = 1.0f;
        //наводим курором на место где должен появиться юнит
        gameObject.GetComponent<RectTransform>().anchoredPosition =
            new Vector3((Input.mousePosition.x), (Input.mousePosition.y) * k, 0);
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = zeroPoint;
        
        /*
        int res = Int32.Parse(resourses.GetComponent<Text>().text);
        if (res >= cost && Time.timeScale > 0&& reloadTime_now >= reloadTime)
        {
            reloadTime_now = 0;
            //создаём юнит в координатах где отпустили мышь
            if (!gameObject.name.Equals("AvaMe"))
            {
                Instantiate(unit, new Vector3(9, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, -0.5f),
                    Quaternion.Euler(0, 0, 0));
            }
            else
            {
                for (int i = 0; i < 100; i++)
                {
                    
                    Instantiate(unit, 
                        new Vector3( 
                            rand.Next(-500, 500)/100.0f, 
                            Camera.main.ScreenToWorldPoint(Input.mousePosition).y + 35 + rand.Next(-500, 500)/100.0f, 
                            -0.5f),
                        Quaternion.Euler(0, 0, -90)).GetComponent<Rigidbody2D>().velocity = Vector2.down * 15;

                }
            }

            res -= cost;
            resourses.GetComponent<Text>().text = res.ToString();
        }

        gameObject.GetComponent<RectTransform>().anchoredPosition = zeroPoint;*/
    }
    
    
    void FixedUpdate()
    {

        if (reloadTime >= reloadTime_now)
        {
            reloadTime_now++;
            gameObject.GetComponent<Image>().fillAmount = reloadTime_now / (float)reloadTime;
        }
    }
}