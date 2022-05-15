using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SaveEquipment
{ 
    public int IDItem;
    public bool isEquiped;
    public SaveEquipment(int IDItem, bool isEquiped)
    {
        this.IDItem = IDItem;
        this.isEquiped = isEquiped;
    }
}

[System.Serializable]
public class SaveSkill
{
    public int idSkill;
    public SkillState stateSkill;
    public SaveSkill(int ID, SkillState state)
    {
        idSkill = ID;
        stateSkill = state;
    }
}

[System.Serializable]
public class SaveAbilitiy
{
    public int IDabiliti;
    public int IDabilitiHandler;

    public SaveAbilitiy(int IDab, int IDah)
    {
        IDabiliti = IDab;
        IDabilitiHandler = IDah;
    }
}

[System.Serializable]
public class SaveData
{
    public int level;
    public int exp;
    public List<SaveEquipment> ListEquipment = new List<SaveEquipment>();
    public List<SaveAbilitiy> ListAblili = new List<SaveAbilitiy>();
    public List<SaveSkill> ListSkillState = new List<SaveSkill>();
    public int SkillPoints;
    public int SpendPoints;

    public SaveData()
    {
        level = 1;
        exp = 0;
        SpendPoints = 0;
        SkillPoints = 0;
    }
    public SaveData(PlayerStats stats)
    {
        ListEquipment.AddRange(stats.SendItems());
        ListSkillState.AddRange(stats.sk.SaveSendSkills());
        ListAblili.AddRange(stats.SendAbilitis());
        exp = stats.expierience;
        level = stats.Level;
        SpendPoints = stats.sk.SpendPoints;
        SkillPoints = stats.SkillPoints;
    }

}
