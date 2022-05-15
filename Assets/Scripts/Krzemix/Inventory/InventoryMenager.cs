using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMenager : MonoBehaviour
{
    [SerializeField] Inventory inventory;
    [SerializeField] EquipmentPanel equipmentPanel;
    [SerializeField] Character character;
    private void Start()
    {
        
    }
    public EquipmentPanel GetEqPanel()
    {
        return equipmentPanel;
    }
    private void Awake()
    {
        equipmentPanel.OnItemLeftClickedEvent += UnequipFromEquipmentPanel;
        inventory.OnItemLeftClickedEvent += EquipFromInventory;
        inventory.OnItemRightClickedEvent += DeleteItemFromInventory;
        equipmentPanel.OnItemRightClickedEvent += DeleteItemFromEquipment;
    }
    private void DeleteItemFromInventory(ItemV2 item)
    {
        inventory.RemoveItem(item);
    }
    private void DeleteItemFromEquipment(ItemV2 item)
    {
        equipmentPanel.RemoveItem(item);
    }
    public void setCharacter(Character character)
    {
        this.character = character;
    }
    public void EquipFromInventory(ItemV2 item)
    {
        Equip(item);
      
   
    }
    private void UnequipFromEquipmentPanel(ItemV2 item)
    {
        Unequip(item);
    }
    public void AddItemToInventory(ItemV2 item)
    {
        inventory.AddItem(item);
    }
    public void Equip(ItemV2 item)
    {

        if(item is EatableItem)
        {
            gameconsole.SendInfo("You eat: " + item.name);
            foreach (var efekt in item.efectList)
            {
                character.GetEatableItem(efekt);
            }
            inventory.RemoveItem(item);
        }
        else if (inventory.RemoveItem(item))
        {
            foreach (var efekt in item.efectList)
            {
                character.AddBonusToStat(efekt);
            }
            if (item.typeItem == ItemType.Sword)
            {
                
                foreach (var stat in character.ReturnStatiticList())
                {
                    if (stat.statisticId == StatName.Damage)
                    {
                        character.GetComponentInChildren<Sword>(true).gameObject.SetActive(true);
                        character.GetComponentInChildren<Sword>(true).SetRealDamage(stat.actualPoint);
                        character.GetComponentInChildren<Sword>(true).EquipSword(item);
                    }
                }
            }
            if (item.typeItem == ItemType.Ranged)
            {
                foreach (var stat in character.ReturnStatiticList())
                {
                    if (stat.statisticId == StatName.RangeDamage)
                    {
                        character.GetComponentInChildren<Ranged>(true).gameObject.SetActive(true);
                        character.GetComponentInChildren<Ranged>(true).SetRealDamage(stat.actualPoint);
                        character.GetComponentInChildren<Ranged>(true).EquipRanged(item);
                    }
                }
            }
            ItemV2 previousItem;
            if(equipmentPanel.AddItem(item,out previousItem))
            {
                if(previousItem != null)
                {
                    inventory.AddItem(previousItem);
                    foreach (var efekt in previousItem.efectList)
                    {
                        character.DelBonusFromStat(efekt);
                    }
                    if (previousItem.typeItem == ItemType.Sword)
                    {
                        foreach (var stat in character.ReturnStatiticList())
                        {
                            if (stat.statisticId == StatName.Damage)
                            {
                                character.GetComponentInChildren<Sword>(true).gameObject.SetActive(false);
                                character.GetComponentInChildren<Sword>(true).SetRealDamage(stat.actualPoint);
                                character.GetComponentInChildren<Sword>(true).UnequipSword();
                            }
                        }
                    }
                    if (item.typeItem == ItemType.Sword)
                    {

                        foreach (var stat in character.ReturnStatiticList())
                        {
                            if (stat.statisticId == StatName.Damage)
                            {
                                character.GetComponentInChildren<Sword>(true).gameObject.SetActive(true);
                                character.GetComponentInChildren<Sword>(true).SetRealDamage(stat.actualPoint);
                                character.GetComponentInChildren<Sword>(true).EquipSword(item);
                            }
                        }
                    }
                    if (previousItem.typeItem == ItemType.Ranged)
                    {
                        foreach (var stat in character.ReturnStatiticList())
                        {
                            if (stat.statisticId == StatName.Damage)
                            {
                                character.GetComponentInChildren<Ranged>(true).gameObject.SetActive(false);
                                character.GetComponentInChildren<Ranged>(true).SetRealDamage(stat.actualPoint);
                                character.GetComponentInChildren<Ranged>(true).UnequipRanged();
                            }
                        }
                    }
                    if (item.typeItem == ItemType.Ranged)
            {
                foreach (var stat in character.ReturnStatiticList())
                {
                    if (stat.statisticId == StatName.RangeDamage)
                    {
                        character.GetComponentInChildren<Ranged>(true).gameObject.SetActive(true);
                        character.GetComponentInChildren<Ranged>(true).SetRealDamage(stat.actualPoint);
                        character.GetComponentInChildren<Ranged>(true).EquipRanged(item);
                    }
                }
            }
                }
            }
            else
            {
                inventory.AddItem(item);
            }
        }
    }
    public void Unequip(ItemV2 item)
    {
        if (!inventory.isFull() && equipmentPanel.RemoveItem(item))
        {
            inventory.AddItem(item);
            foreach (var efekt in item.efectList)
            {
                character.DelBonusFromStat(efekt);
            }
            if (item.typeItem == ItemType.Sword)
            {
                foreach (var stat in character.ReturnStatiticList())
                {
                    if (stat.statisticId == StatName.Damage)
                    {
                        character.GetComponentInChildren<Sword>(true).gameObject.SetActive(false);
                        character.GetComponentInChildren<Sword>(true).SetRealDamage(stat.actualPoint);
                    }
                }
            }
            if (item.typeItem == ItemType.Ranged)
            {
                foreach (var stat in character.ReturnStatiticList())
                {
                    if (stat.statisticId == StatName.Damage)
                    {
                        character.GetComponentInChildren<Ranged>(true).gameObject.SetActive(false);
                        character.GetComponentInChildren<Ranged>(true).SetRealDamage(stat.actualPoint);
                    }
                }
            }
        }
    }
}
