using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class DropHandler :  MonoBehaviour, IDropHandler
{
   
   

    public void OnDrop(PointerEventData eventData)
    {
        GameObject unit = Drager.GetDraggedItem();
        
        
        
            int res = Int32.Parse(unit.GetComponent<Drager>().resourses.GetComponent<Text>().text);
            if (res >= unit.GetComponent<Drager>().cost && Time.timeScale > 0 && unit.GetComponent<Drager>().reloadTime_now >= unit.GetComponent<Drager>().reloadTime)
            {
                unit.GetComponent<Drager>().reloadTime_now = 0;
                //создаём юнит в координатах где отпустили мышь
                if (!unit.GetComponent<Drager>().name.Equals("AvaMe"))
                {
                    Instantiate(unit.GetComponent<Drager>().unit, new Vector3(9, gameObject.transform.position.y, -0.5f),
                        Quaternion.Euler(0, 0, 0), gameObject.transform);
                }
                /*
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
                }*/

                res -= unit.GetComponent<Drager>().cost;
                unit.GetComponent<Drager>().resourses.GetComponent<Text>().text = res.ToString();
            }

            unit.GetComponent<Drager>().GetComponent<RectTransform>().anchoredPosition = unit.GetComponent<Drager>().zeroPoint;
        
    }
}
