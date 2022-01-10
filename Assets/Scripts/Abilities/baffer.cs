using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baffer : MonoBehaviour
{
    public Updates updates;

    public bool ourTeam = false;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform VARIABLE in GameObject.Find("Canvas back").transform.Find("Areas"))
        {
            foreach (Transform VARIABLE1 in VARIABLE)
            {

                if (VARIABLE1.gameObject.GetComponent<Unit1>())
                {
                    if (VARIABLE1.gameObject.GetComponent<Unit1>().isOurTeam == ourTeam && !VARIABLE1.gameObject.GetComponent<Unit1>().isMain)
                    {
                        VARIABLE1.gameObject.GetComponent<Unit1>().health += updates.Add_Hp;
                        VARIABLE1.gameObject.GetComponent<Unit1>().damage += updates.damage;
                        VARIABLE1.gameObject.GetComponent<Unit1>().speed += updates.speed;
                        VARIABLE1.gameObject.GetComponent<Unit1>().maxDistance += updates.maxDistance;
                        //VARIABLE1.gameObject.GetComponent<Unit1>().reloadTime += updates.reloadTime;

                    }

                }
            }
        }
    }

    public int lifetime = 100;

    void FixedUpdate()
    {
        lifetime--;
        if (lifetime <= 0)
        {
            
            foreach (Transform VARIABLE in GameObject.Find("Canvas back").transform.Find("Areas"))
            {
                foreach (Transform VARIABLE1 in VARIABLE)
                {

                    if (VARIABLE1.gameObject.GetComponent<Unit1>())
                    {
                        if (VARIABLE1.gameObject.GetComponent<Unit1>().isOurTeam == ourTeam && !VARIABLE1.gameObject.GetComponent<Unit1>().isMain)
                        {
                            VARIABLE1.gameObject.GetComponent<Unit1>().health -= updates.Add_Hp;
                            VARIABLE1.gameObject.GetComponent<Unit1>().damage -= updates.damage;
                            VARIABLE1.gameObject.GetComponent<Unit1>().speed -= updates.speed;
                            VARIABLE1.gameObject.GetComponent<Unit1>().maxDistance -= updates.maxDistance;
                            //VARIABLE1.gameObject.GetComponent<Unit1>().reloadTime -= updates.reloadTime;

                        }

                    }
                }
            
            }
            
            Destroy(gameObject);
        }
    }
}
