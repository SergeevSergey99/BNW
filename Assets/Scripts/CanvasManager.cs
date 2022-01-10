using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

   // public String ArmyDescriptionText = "";
    [SerializeField] private GameObject CommanderProfilePanel;
    [SerializeField] private GameObject HeadquartersPanel;
    [SerializeField] private GameObject SkillDescription0;
    [SerializeField] private GameObject SkillDescription1;
    [SerializeField] private GameObject SkillDescription2;
    [SerializeField] private GameObject SkillDescription3;
    [SerializeField] private GameObject SkillImage0;
    [SerializeField] private GameObject SkillImage1;
    [SerializeField] private GameObject SkillImage2;
    [SerializeField] private GameObject SkillImage3;
    [SerializeField] private GameObject CommanderAvatar;
    [SerializeField] private GameObject ArmySelectionPanel;
    
    // Start is called before the first frame update
    void Start()
    {
//        GameObject.Find("CommanderAvatar(Major)").GetComponent<Image>().sprite =
  //          GameManagerScript.GeneralSprite.GetComponent<Image>().sprite; 

        BackToCommanderProvifle();
    }
    
    public void SetActiveHeadquartersPanel()
    {
        CommanderProfilePanel.SetActive(false);
        HeadquartersPanel.SetActive(true);
        ArmySelectionPanel.SetActive(true);
        
        SkillImage0.SetActive(true);
        SkillImage1.SetActive(true);
        SkillImage2.SetActive(true);
        SkillImage3.SetActive(true);
        
        //SkillImage0.GetComponent<Image>().sprite = 
    }

    public void BackToCommanderProvifle()
    {
        ArmySelectionPanel.SetActive(false);
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        SkillDescription3.SetActive(false);
        
        
        SkillImage0.SetActive(true);
        SkillImage0.GetComponent<Image>().sprite = null; 
        SkillImage0.GetComponent<Image>().color = Color.black;
        SkillImage1.SetActive(true);
        SkillImage1.GetComponent<Image>().sprite = null;
        SkillImage1.GetComponent<Image>().color = Color.black;
        SkillImage2.SetActive(true);
        SkillImage2.GetComponent<Image>().sprite = null;
        SkillImage2.GetComponent<Image>().color = Color.black;
        SkillImage3.SetActive(true);
        SkillImage3.GetComponent<Image>().sprite = null;
        SkillImage3.GetComponent<Image>().color = Color.black;

        List<GameObject> armyList = GameManagerScript.Army;
        
        int i = 0;
        foreach (var unit in armyList)
        {
            switch (i)
            {
                case 0: SkillImage0.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite; 
                    SkillImage0.GetComponent<Image>().color = Color.white; break;
                case 1: SkillImage1.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite; 
                    SkillImage1.GetComponent<Image>().color = Color.white; break;
                case 2: SkillImage2.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite;  
                    SkillImage2.GetComponent<Image>().color = Color.white; break;
                case 3: SkillImage3.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite;  
                    SkillImage3.GetComponent<Image>().color = Color.white; break;
            }
            i++;
        }
        
        
        HeadquartersPanel.SetActive(false);
        CommanderProfilePanel.SetActive(true); 
        CommanderAvatar.GetComponent<Image>().sprite = GameManagerScript.General.GetComponent<SpriteRenderer>().sprite;
        CommanderProfilePanel.transform.Find("CommanderImage").GetComponent<Image>().sprite = GameManagerScript.General.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetActivArmySelection(GameObject gameObject)
    {
        
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        SkillDescription3.SetActive(false);
        

        ArmySelectionPanel.SetActive(true);
        
        
        SkillImage0.SetActive(true);
        SkillImage0.GetComponent<Image>().sprite = null; 
        SkillImage0.GetComponent<Image>().color = Color.black;
        SkillImage1.SetActive(true);
        SkillImage1.GetComponent<Image>().sprite = null;
        SkillImage1.GetComponent<Image>().color = Color.black;
        SkillImage2.SetActive(true);
        SkillImage2.GetComponent<Image>().sprite = null;
        SkillImage2.GetComponent<Image>().color = Color.black;
        SkillImage3.SetActive(true);
        SkillImage3.GetComponent<Image>().sprite = null;
        SkillImage3.GetComponent<Image>().color = Color.black;

        List<GameObject> armyList = gameObject.GetComponent<ArmyBox>().GetArmy();
        
        int i = 0;
        foreach (var unit in armyList)
        {
            switch (i)
            {
                case 0: SkillImage0.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite; 
                    SkillImage0.GetComponent<Image>().color = Color.white; break;
                case 1: SkillImage1.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite; 
                    SkillImage1.GetComponent<Image>().color = Color.white; break;
                case 2: SkillImage2.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite; 
                    SkillImage2.GetComponent<Image>().color = Color.white; break;
                case 3: SkillImage3.GetComponent<Image>().sprite = unit.GetComponent<SpriteRenderer>().sprite; 
                    SkillImage3.GetComponent<Image>().color = Color.white; break;
            }
            i++;
        }
        
        ArmySelectionPanel.transform.GetChild(0).gameObject.GetComponent<Text>().text = gameObject.transform.Find("Army").gameObject.GetComponent<Text>().text;
        GameManagerScript.Army = armyList;
        GameManagerScript.General = gameObject.GetComponent<ArmyBox>().GetGeneral();

    }
    
    public void SetActiveSkillDescription0()
    {
        ArmySelectionPanel.SetActive(false);
        
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        SkillDescription3.SetActive(false);
        SkillDescription0.SetActive(true);
        
        SkillImage0.SetActive(false);
        SkillImage1.SetActive(false);
        SkillImage2.SetActive(false);
        SkillImage3.SetActive(false);
    }
    
    public void SetActiveSkillDescription1()
         {
             ArmySelectionPanel.SetActive(false);
             
             SkillDescription0.SetActive(false);
             SkillDescription2.SetActive(false);
             SkillDescription3.SetActive(false);
             
             SkillDescription1.SetActive(true);
         }
    
    public void SetActiveSkillDescription2()
    {
        ArmySelectionPanel.SetActive(false);
        
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription3.SetActive(false);
        
        SkillDescription2.SetActive(true);
    }
    
    public void SetActiveSkillDescription3()
    {
        ArmySelectionPanel.SetActive(false);
        
        SkillDescription0.SetActive(false);
        SkillDescription1.SetActive(false);
        SkillDescription2.SetActive(false);
        
        SkillDescription3.SetActive(true);
    }
}
