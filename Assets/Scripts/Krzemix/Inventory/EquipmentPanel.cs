using System;
using UnityEngine;

public class EquipmentPanel : MonoBehaviour
{
    [SerializeField] Transform equipmentSlotParent;
    [SerializeField] EquipmentSlot[] equipmentSlots;

    public event Action<ItemV2> OnItemLeftClickedEvent;
    public event Action<ItemV2> OnItemRightClickedEvent;
    private void Start()
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            equipmentSlots[i].OnLeftClickEvent += OnItemLeftClickedEvent;
            equipmentSlots[i].OnRightClickEvent += OnItemRightClickedEvent;
        }
    }

    public EquipmentSlot[] GetEq()
    {
        return equipmentSlots;
    }
    private void OnValidate()
    {
        equipmentSlots = equipmentSlotParent.GetComponentsInChildren<EquipmentSlot>();
    }
    public bool AddItem(ItemV2 item, out ItemV2 previousItem)
    {
        
        for(int i=0; i < equipmentSlots.Length; i++)
        {
            if(equipmentSlots[i].equipmentClassType == item.typeItem)
            {
                previousItem = (ItemV2)equipmentSlots[i].itemInSlot;
                equipmentSlots[i].itemInSlot = item;
                equipmentSlots[i].iditem = item.idItem;
        
                return true;
            }
            else if(equipmentSlots[i].equipmentClassType == ItemType.Weapon && 
                (item.typeItem == ItemType.Petard || item.typeItem == ItemType.Sword || item.typeItem == ItemType.Ranged))
            {
                previousItem = (ItemV2)equipmentSlots[i].itemInSlot;
                equipmentSlots[i].itemInSlot = item;
                equipmentSlots[i].iditem = item.idItem;
     
                return true;
            }
        }
        previousItem = null;
        return false;
    }
    public bool RemoveItem(ItemV2 item)
    {
        for (int i = 0; i < equipmentSlots.Length; i++)
        {
            if (equipmentSlots[i].itemInSlot == item)
            {
                equipmentSlots[i].itemInSlot = null;
                return true;
            }
        }
        return false;
    }
}
