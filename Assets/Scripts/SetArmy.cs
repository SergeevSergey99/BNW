using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetArmy : MonoBehaviour
{
    [SerializeField]private GameObject UI_Unit;
    // Start is called before the first frame update
    void Awake()
    {
        if (GameObject.Find("StartOptions") != null)
        {
            GameObject.Find("StartOptions").gameObject.GetComponent<start_Options>().Set();
        }
            
        GameObject.Find("Unit_Main").GetComponent<SpriteRenderer>().sprite = GameManagerScript.General.GetComponent<SpriteRenderer>().sprite;
        GameObject.Find("Unit_Main").GetComponent<SpriteRenderer>().flipX = !GameManagerScript.General.GetComponent<SpriteRenderer>().flipX;
        if(GameObject.Find("Unit_Main").GetComponent<Animator>())
            GameObject.Find("Unit_Main").GetComponent<Animator>().runtimeAnimatorController = GameManagerScript.General.GetComponent<Animator>().runtimeAnimatorController;
        GameObject.Find("Unit_Main").GetComponent<Unit1>().health = GameManagerScript.General.GetComponent<Unit1>().health;
        GameObject.Find("Unit_Main").GetComponent<Unit1>().damage = GameManagerScript.General.GetComponent<Unit1>().damage;
        GameObject.Find("Unit_Main").GetComponent<Unit1>().reloadTime = GameManagerScript.General.GetComponent<Unit1>().reloadTime;
        GameObject.Find("Unit_Main").GetComponent<AudioSource>().clip = GameManagerScript.General.GetComponent<AudioSource>().clip;
        GameObject.Find("Unit_Main").GetComponent<Unit1>().maxDistance = GameManagerScript.General.GetComponent<Unit1>().maxDistance;
        GameObject.Find("Unit_Main").GetComponent<Unit1>().bulletPrefab = GameManagerScript.General.GetComponent<Unit1>().bulletPrefab;
        GameObject.Find("Unit_Main").GetComponent<Unit1>().isMeele = GameManagerScript.General.GetComponent<Unit1>().isMeele;
        GameObject.Find("Unit_Main").GetComponent<SpawnMashine>().unitList = GameManagerScript.Army;

        for (int i = 0; i < GameManagerScript.Army.Count; i++)
        {
            // генерация иконок
            GameObject icon = Instantiate(UI_Unit, GameObject.Find("Canvas UI").transform);
            icon.name = "Unit" + i;
            icon.GetComponent<Image>().sprite = GameManagerScript.Army[i].GetComponent<SpriteRenderer>().sprite;
            icon.transform.GetChild(0).GetComponent<Drager>().resourses =
                GameObject.Find("Canvas UI").transform.Find("Resources").GetChild(0).gameObject;
            icon.transform.GetChild(0).GetComponent<Image>().sprite = GameManagerScript.Army[i].GetComponent<SpriteRenderer>().sprite;
            icon.transform.GetChild(0).GetComponent<Drager>().unit = GameManagerScript.Army[i];
            
            icon.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(-170 * i - 10,0,0);

        }
    }

}
