using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConnectToAbillity : MonoBehaviour, IDropHandler
{
    public int ID;
    public AbilityHolder abHolder;
    public Skill ConnectedSkill;
    public Image imageSkill;
    public Text text;
    public Text cooltext;

    public UISKillCooldown skillinfo;

    public void OnDrop(PointerEventData eventData)
    {
        if(ConnectedSkill != null)
        {
       
            if(eventData.pointerDrag.gameObject.GetComponent<Skill>().state == SkillState.bought);
            {
                ConnectedSkill.state = SkillState.bought;
            }
        }
       
        GameObject SkillInfo = eventData.pointerDrag.gameObject;
        ConnectedSkill = SkillInfo.GetComponent<Skill>();
        if (ConnectedSkill.state == SkillState.bought)
        {
            ConnectedSkill.state = SkillState.used;
            abHolder.ability = ConnectedSkill.ability;
            skillinfo.abHolder = abHolder;
            imageSkill.sprite = ConnectedSkill.ImageDrag.GetComponent<Image>().sprite;
            text.text = ConnectedSkill.name;
            ConnectedSkill.UpdateST();
        }
    }
    public void LoadGameFromSave()
    {
        Debug.Log(ConnectedSkill.name);
        Debug.Log(this.abHolder.ability.ID);
        abHolder.ability = ConnectedSkill.ability;
        imageSkill.sprite = ConnectedSkill.ImageDrag.GetComponent<Image>().sprite;
        text.text = ConnectedSkill.name;
       
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
