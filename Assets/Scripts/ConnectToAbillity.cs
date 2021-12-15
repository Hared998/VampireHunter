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

    public void OnDrop(PointerEventData eventData)
    {
        if(ConnectedSkill != null)
        {
            ConnectedSkill.used = false;
        }
        GameObject SkillInfo = eventData.pointerDrag.gameObject;
        ConnectedSkill = SkillInfo.GetComponent<Skill>();
        if (ConnectedSkill.bought)
        {
            abHolder.ability = ConnectedSkill.ability;
            imageSkill.sprite = ConnectedSkill.ImageDrag.GetComponent<Image>().sprite;
            text.text = ConnectedSkill.name;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
