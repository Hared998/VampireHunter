using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[System.Serializable]
public enum SkillState
{
    forbuy,
    bought,
    used
}

public class Skill : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Ability ability;
    public string name;
    [TextArea]
    public string description;
    public int requiredLevel;
    public Skill requiredSkill;
    public Skill ParentSkill;
    public bool bought;
    public bool used;

    public Text nameText;
    public Text requiredText;

    private SkillTree skillTree;

    public GameObject ImageDrag;

    private CanvasGroup canvasGroup;
    private RectTransform rectTransformation;
    private RectTransform startRectTransform;

    public SkillState state;

    private ConnectToAbillity cta;

    public void SetSkillTree(SkillTree tree)
    {
        skillTree = tree;
    }
    public void start()
    {
        state = SkillState.forbuy;
    }
    
    // Start is called before the first frame update
    public void Start()
    {
        skillTree = GameObject.FindGameObjectWithTag("Skill Tree").GetComponent<SkillTree>();
        nameText.text = name;
        name = ability.Name;
        nameText.text = ability.Name;
        requiredText.text = "Level: " + requiredLevel;
    }
    public void UpdateST()
    {


        if (state == SkillState.used)
        {
 
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else if (state != SkillState.used && state == SkillState.bought)
        {
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else if ((requiredSkill != null && requiredSkill.state != SkillState.bought) || skillTree.Level < requiredLevel || skillTree.SkillPoints < 0)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
            gameObject.GetComponent<Image>().color = Color.black;

    }
    public void buy()
    {
        if (skillTree.SkillPoints > 0 && state == SkillState.forbuy && (requiredSkill != null && requiredSkill.state != SkillState.forbuy || requiredSkill == null) && skillTree.Level >= requiredLevel)
        {
            state = SkillState.bought;
            skillTree.SkillPoints--;
            skillTree.SpendPoints++;
        }
        skillTree.UpdateAllSkills();
    }
    private void Awake()
    {
        canvasGroup = ImageDrag.GetComponent<CanvasGroup>();
        rectTransformation = ImageDrag.GetComponent<RectTransform>();
        startRectTransform = rectTransformation;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
       
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (state == SkillState.bought)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;
  
        }
    
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (state == SkillState.bought)
        {
            rectTransformation.anchoredPosition += eventData.delta;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransformation.anchoredPosition = new Vector2(0,0);
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
}
