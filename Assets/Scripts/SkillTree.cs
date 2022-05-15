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
            if (SkillMenu.active)
                Time.timeScale = 1;
            else
                Time.timeScale = 0;
            
            SkillMenu.active = !SkillMenu.active;
            
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
        if (ListofSkills.Count == 0)
        {
            foreach (var skill in SkillHandler.GetComponentsInChildren<Skill>())
            {
                skill.SetSkillTree(this);
                skill.UpdateST();
            }
        }
        foreach (var skill in ListofSkills)
            skill.UpdateST();
    }
    public List<SaveSkill> SaveSendSkills()
    {
        List<SaveSkill> tmp = new List<SaveSkill>();
        foreach (var skill in ListofSkills)
        {
            tmp.Add(new SaveSkill(skill.ability.ID, skill.state));
        }
        return tmp;

    }

}
