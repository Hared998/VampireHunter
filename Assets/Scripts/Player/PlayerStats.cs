using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    private Character character;
    public float Health = 100;
    public float Armor = 10;

    public int BaseDamage = 5;
    public int Power = 50;

    public int SkillPoints = 0;
    public int baseexpNeeded = 1000;
    public int expNeeded;
    public int Level = 1;
    public int expierience = 0;

    public Slider ExpBar;

    public SkillTree sk;

    public Slider HealthBar;

    public List<Ability> ListAbiliti;

    List<SaveEquipment> se;
    private void Awake()
    {
        se = new List<SaveEquipment>();
        character = gameObject.GetComponent<Character>();
    }
    void Start()
    {
   
        character.LoadInventoryItems(se);
        gameconsole.SendInfo("Press B to open equipment");
    }
    public void TakeDamage(float Damage)
    {
        character.GetDamage((double)Damage);
    }
    public float ExpPercentage()
    {
        return  (float)expierience / (float)ExpNeedForLevel(Level) * 1.0f;

    }

    // Update is called once per frame
    void Update()
    {
        foreach(var stat in character.ReturnStatiticList())
        {
            if (stat.statisticId == StatName.Helth)
                Health = (float)stat.actualPoint;
        }
        ExpBar.value = ExpPercentage();
        HealthBar.value = Health;
        if (Health <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
    public int ExpNeedForLevel(int level)
    {
        return ((baseexpNeeded * level) + (baseexpNeeded * level / baseexpNeeded)*100);
    }
    public void UpdateLevel(int Giveexp)
    {
        expierience += Giveexp;
        if (expierience >= expNeeded)
        {
           
            Level++;
            gameconsole.SendInfo("Level UP! [" + Level + " LEVEL ] Press P to add abilitis! ");
            expNeeded = ExpNeedForLevel(Level);
            expierience = 0;
            SkillPoints++;
            sk.UpdateAllSkills();
        }
    }
    public void SaveData()
    {
        Saving.SaveGameData(this);
    }

    public void LoadData(List<ConnectToAbillity> CTA)
    {

        SaveData data = Saving.LoadGameData();
        this.SkillPoints = data.SkillPoints;
        this.sk.SpendPoints = data.SpendPoints;
        this.expierience = data.exp;
        this.Level = data.level;
        expNeeded = ExpNeedForLevel(Level);
        if (data.ListAblili.Count != 0)
            SetAbilitis(data.ListAblili,CTA);
        if(data.ListSkillState.Count != 0)
        {
            foreach(var i in sk.SkillHandler.GetComponentsInChildren<Skill>())
            {
                foreach(var j in data.ListSkillState)
                {
                    if (i.ability.ID == j.idSkill)
                        i.state = j.stateSkill;
                }
            }
        }
        se.AddRange(data.ListEquipment);
    }

    public List<SaveAbilitiy> SendAbilitis()
    {
        AbilityHolder[] tmpabli = gameObject.GetComponentsInChildren<AbilityHolder>();
        List<SaveAbilitiy> tmp = new List<SaveAbilitiy>();
        
        foreach (var i in tmpabli)
        {
            if (i.ability != null)
            {
                SaveAbilitiy SA = new SaveAbilitiy(i.ability.ID, i.ID);
                tmp.Add(SA);
            }
        }
        return tmp;
    }
    public void SetAbilitis(List<SaveAbilitiy> SA, List<ConnectToAbillity> CTA)
    {
        GameObject[] tmpconn = GameObject.FindGameObjectsWithTag("AbilitiyHandler");
        foreach (var i in SA)
        {
            
            if (i.IDabiliti != null)
            {
                Ability abiliti = GetAbiliti(i.IDabiliti);
                foreach (var j in CTA)
                {
                    ConnectToAbillity connector = j.GetComponent<ConnectToAbillity>();
                    foreach (var k in sk.SkillHandler.GetComponentsInChildren<Skill>())
                    {
                        if (i.IDabilitiHandler == connector.ID && k.ability.ID == abiliti.ID)
                        {
                            connector.skillinfo.abHolder = connector.abHolder;
                            connector.ConnectedSkill = k;
                            connector.abHolder.ability = abiliti;
                            connector.imageSkill.sprite = k.ImageDrag.GetComponent<Image>().sprite;
                            connector.text.text = k.name;
                        }
                    }
                }
            }
            
        }
    }
    public List<SaveEquipment> SendItems()
    {
        return character.GetInvetory();
    }
    public Ability GetAbiliti(int IDAbiliti)
    {
        foreach(var i in ListAbiliti)
        {
      
            if (IDAbiliti == i.ID)
                return i;
        }
        return null;
    }
}
