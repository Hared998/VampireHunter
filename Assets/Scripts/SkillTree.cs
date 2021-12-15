using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTree : MonoBehaviour
{
    public static SkillTree skillTree;

    public List<Skill> ListofSkills;
    public GameObject SkillHandler;

    public Text skillPointsText;

    public GameObject SkillMenu;
    public int SkillPoints;
    public int SpendPoints;
    public int Level;

    public Text requiredLevelText;
    public Text SkillPointsText;

    public GameObject Player;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
            SkillMenu.active =  true;
        }
    }
    public void RenewGame()
    {
        Time.timeScale = 1;
    }
    void Start()
    {
        SkillMenu.active = false;
        foreach (var skill in SkillHandler.GetComponentsInChildren<Skill>())
            ListofSkills.Add(skill);
        

    }

    public void UpdateAllSkills()
    {
        SkillPoints = Player.GetComponent<PlayerStats>().SkillPoints - SpendPoints;
        Level = Player.GetComponent<PlayerStats>().Level;

        skillPointsText.text = "Skill Points: " + SkillPoints;
        requiredLevelText.text = "Level: " + Level;

        foreach (var skill in ListofSkills)
            skill.UpdateST();
    }

}
