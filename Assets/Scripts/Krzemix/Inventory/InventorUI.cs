using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorUI : MonoBehaviour
{
    public InventoryMenager inventor;
    private bool isActive = false;

    
    
    public void getInfoAboutSelectItem()
    {
        /*nameItem.text = "Nazwa Itemu";
        classItem.text = "Klasa itemu";
        typeItem.text = "Typ itemu";
        description.text = "Opis itemu \n tak\ntak";*/
    }
    public void changeVisibility()
    {
        if(isActive==true)
        {
            isActive = false;
            inventor.gameObject.SetActive(isActive);
        }
        else
        {
            isActive = true;
            inventor.gameObject.SetActive(isActive);
        }
    }

    private void Start()
    {
        inventor.gameObject.active = true;
        inventor.setCharacter(GameObject.FindObjectOfType<Character>());
        inventor.gameObject.active = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            changeVisibility();
        }
    }
}
