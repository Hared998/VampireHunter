using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] List<ItemV2> items;
    [SerializeField] Transform itemInventoryParent;
    [SerializeField] itemSlot[] itemSlots;
    [SerializeField] List<ItemV2> startItems;
    public event Action<ItemV2> OnItemLeftClickedEvent;
    public event Action<ItemV2> OnItemRightClickedEvent;
    public List<SaveEquipment> GetItemSlot()
    {
        List<SaveEquipment> itemList = new List<SaveEquipment>();
        foreach(var i in items)
        {
            SaveEquipment item = new SaveEquipment(i.idItem, false);
            itemList.Add(item);
        }
        foreach(var i in transform.parent.GetComponent<InventoryMenager>().GetEqPanel().GetEq())
        {
            if (i.itemInSlot != null)
            {
                SaveEquipment item = new SaveEquipment(i.iditem, true);
                itemList.Add(item);
            }
        }

        return itemList;
    }
    private void Start()
    {

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].OnLeftClickEvent += OnItemLeftClickedEvent;
            itemSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
        UpdateUI();
    }
    
    private void OnValidate()
    {
        if(itemInventoryParent != null)
        {
            itemSlots = itemInventoryParent.GetComponentsInChildren<itemSlot>();
            UpdateUI();
        }
    }
    private void UpdateUI()
    {
     
        int i = 0;
        if (items != null)
        {
         
            for (; i < items.Count && i < itemSlots.Length; i++)
            {
                itemSlots[i].itemInSlot = items[i];
            }
            for (; i < itemSlots.Length; i++)
            {
                
                itemSlots[i].itemInSlot = null;
            }
        }
    }
    public void LoadStartItem()
    {
        foreach (var i in startItems)
        {

            AddItem(i);
        }
    }
    public bool isFull()
    {
        return items.Count >= itemSlots.Length;
    }
    public bool AddItem(ItemV2 item)
    {
        if (isFull())
            return false;
        items.Add(item);
        gameconsole.SendInfo("Added item: " + item.itemName);
        UpdateUI();
        return true;
    }
    public bool RemoveItem(ItemV2 item)
    {


        if (items.Remove(item))
        {
            UpdateUI();
            return true;
        }
        return false;
    }
}
