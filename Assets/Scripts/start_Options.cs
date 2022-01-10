using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct Updates
{
    public int Add_Hp;
    public int damage;
    public float speed;
    public int reloadTime;
    public float maxDistance;
}
public class start_Options : MonoBehaviour
{
    public GameObject General;
    public SpriteRenderer GeneralSprite;

    public List<GameObject> Army;

    public void Set()
    {
        if (!GameManagerScript.isSet)
        {
            GameManagerScript.General = General;
            //    GameManagerScript.GeneralSprite = GeneralSprite;
            GameManagerScript.Army = Army;
            GameManagerScript.isSet = true;
            
            List<int> l = new List<int>();
            l.Add(0);
            GameManagerScript.UnlockedLevels = l;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Set();
    }
}