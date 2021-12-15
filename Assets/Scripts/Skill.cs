using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Skill : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Ability ability;
    public int ID;
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

        if (used)
        {
            gameObject.GetComponent<Image>().color = Color.green;
        }
        else if (!used && bought)
        {
            gameObject.GetComponent<Image>().color = Color.yellow;
        }
        else if (requiredSkill != null && !requiredSkill.bought)
        {
            gameObject.GetComponent<Image>().color = Color.red;
        }
        else
            gameObject.GetComponent<Image>().color = Color.black;

    }
    public void buy()
    {
        if (skillTree.SkillPoints > 0 && !bought && (requiredSkill != null && requiredSkill.bought || requiredSkill == null) && skillTree.Level >= requiredLevel)
        {
            bought = true;
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
        Debug.Log("OnPointerDown");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (bought)
        {
            canvasGroup.blocksRaycasts = false;
            canvasGroup.alpha = 0.8f;
            Debug.Log("BeginDrag");
        }
    
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (bought)
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
