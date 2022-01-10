using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class SpawnMashine : MonoBehaviour
{
    private int resourse = 10;

//	private float maxDistance = 8;
    private Vector3 moveVector;

    /*
    public GameObject unitB;
    public GameObject unitZ;
    public GameObject unitK;
    public GameObject unitL;*/
    public List<GameObject> unitList;
    int[] unitReloadlist = new int[100];
    int[] unitReloadlist_now = new int[100];

    // Use this for initialization
    private Random rnd = new Random();
    public int reloadMax = 60;
    private int reload = 60;
    private int resReload = 100;
    private int maxValue;

    void Start()
    {
        moveVector = gameObject.GetComponent<Unit1>().GetMVector();
        int i = 0;
        foreach (var unit in unitList)
        {
            maxValue = Mathf.Max(maxValue, unit.GetComponent<Unit1>().cost);
            unitReloadlist[i] = unit.GetComponent<Unit1>().reloadTime;
            unitReloadlist_now[i] = 0;
            i++;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            if (unitReloadlist_now[i] < unitReloadlist[i])
                unitReloadlist_now[i]++;
        }

        resReload--;
        if (resReload <= 0)
        {
            resourse++;
            resReload = 100;
        }

        if (reload <= 0)
        {
            if (resourse >= maxValue)
            {
                Collider2D[] clds = Physics2D.OverlapCircleAll(gameObject.transform.position + moveVector * 4.0f, 3.0f);
                int count = 0;
                foreach (var colider in clds)
                {
                    if (colider.gameObject.CompareTag("Actor")
                        && colider.gameObject.GetComponent<Unit1>().isOurTeam
                        ^ gameObject.GetComponent<Unit1>().isOurTeam)
                    {
                        count++;
                    }
                }

                int spawnY;
                int unitType;


                spawnY = GameObject.Find("Canvas back").transform.Find("Areas").transform.childCount;
                if (count <= 0)
                {
                    unitType = rnd.Next(0, unitList.Count);
                    spawnY = rnd.Next(0, spawnY);
                    reloadMax = 60;
                }
                else
                {
                    reloadMax = 10;
                    resourse = maxValue;
                    unitType = rnd.Next(0, unitList.Count);
                    spawnY = 1;
                }

                if (unitReloadlist_now[unitType] >= unitReloadlist[unitType])
                {
                    unitReloadlist_now[unitType] = 0;
                    GameObject unit = Instantiate(unitList[unitType],
                        new Vector3(transform.position.x, GameObject.Find("Canvas back").transform.Find("Areas").transform.Find("Area" +( spawnY +1)).position.y, 0), // + 0.01f+(Time.time%30/10000f)),
                        Quaternion.Euler(0, 0, 0),
                        GameObject.Find("Canvas back").transform.Find("Areas").transform.Find("Area" +( spawnY +1))
                        );
                    unit.GetComponent<Unit1>().moveVector = moveVector;
                    unit.GetComponent<Unit1>().isOurTeam = gameObject.GetComponent<Unit1>().isOurTeam;
                    reload = reloadMax;
                    resourse -= unitList[unitType].GetComponent<Unit1>().cost;
                }
                /*
                switch (unitType)
                {
                    case 1:
                        {
                            Instantiate(unitK, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
                            reload = reloadMax;
                            resourse -= unitK.GetComponent<Unit1>().cost;
                        }
    
                        break;
                    case 2:
                        {
                            Instantiate(unitB, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
                            reload = reloadMax;
                            resourse -= unitB.GetComponent<Unit1>().cost;
                        }
    
                        break;
                    case 3:
                        {
                            Instantiate(unitZ, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
                            reload = reloadMax;
                            resourse -= unitZ.GetComponent<Unit1>().cost;
                        }
    
                        break;
                    case 4:
                        {
                            Instantiate(unitL, new Vector3(transform.position.x, spawnY, 0), Quaternion.Euler(0, 0, 0));
                            reload = reloadMax;
                            resourse -= unitL.GetComponent<Unit1>().cost;
                        }
    
                        break;
                }*/
            }
        }
        else
        {
            reload--;
        }
    }
}