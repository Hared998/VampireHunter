using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour
{
    [SerializeField] string characterName;
    [SerializeField] int Level = 1;
    [SerializeField] double Experience;
    [SerializeField] double startMaxPointExperience = 100;
    [SerializeField] List<object> animationList; //Do zmiany pod animacje
    [SerializeField] List<Statistic> statisticList;

    Inventory inventory;
    ItemManager itemManager;
    InventoryMenager invMenager;

    public void Awake()
    {
        invMenager = GameObject.FindWithTag("InventoryMenager").GetComponent<InventoryMenager>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    public List<SaveEquipment> GetInvetory()
    {
        return inventory.GetItemSlot();
    }
    public void LoadInventoryItems(List<SaveEquipment> listItem)
    {

        itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
      

        invMenager.setCharacter(this);
        if (listItem.Count > 0)
        {
            foreach (var i in itemManager.GetItemList())
            {
                foreach (var j in listItem)
                {
                    if (i.idItem == j.IDItem)
                    {
                        inventory.AddItem(i);
                        if (j.isEquiped)
                        {
                            
                            invMenager.gameObject.SetActive(true);
                            invMenager.EquipFromInventory(i);
                            
                        }
                    }
                }
            }
        }
        else
        {
            inventory.LoadStartItem();

        }
        invMenager.gameObject.SetActive(false);
    }
    public List<Statistic> ReturnStatiticList()
    {
        return statisticList;
    }

    public void AddBonusToStat(Efect efect)
    {
        double bonus = 0;

        foreach(var stat in statisticList)
        {
            if (stat.statisticId == efect.typeStatistic)
            {
                if (efect.isMultipler == true)
                    bonus = Math.Round(stat.bonusPoint + (stat.basePoint * efect.valueEfect / 100), 5);
                else
                    bonus = stat.bonusPoint + efect.valueEfect;
                stat.setBonusPoint(bonus);
                stat.setMaxPoint();
                if (efect.isMultipler == true)
                    stat.changeActualPoint(OperationName.Add, Math.Round(stat.basePoint * efect.valueEfect / 100, 5));
                else
                    stat.changeActualPoint(OperationName.Add, Math.Round(efect.valueEfect, 5));
            }
        }
    }
    public void DelBonusFromStat(Efect efect)
    {
        double bonus = 0;
        foreach (var stat in statisticList)
        {
            if (stat.statisticId == efect.typeStatistic)
            {
                
                if (efect.isMultipler == true)
                {
                    bonus = (Math.Round(stat.bonusPoint+(stat.basePoint * efect.valueEfect / 100 * (-1)),5));
                }
                else
                    bonus = (stat.bonusPoint + (efect.valueEfect*(-1)));
                stat.setBonusPoint(bonus);
                stat.setMaxPoint();
                if (efect.isMultipler == true)
                    stat.changeActualPoint(OperationName.Sub, stat.basePoint * efect.valueEfect / 100);
                else
                    stat.changeActualPoint(OperationName.Sub, efect.valueEfect);
            }
        }
    }
    public void GetEatableItem(Efect efect)
    {
        double point = 0;
        foreach (var stat in statisticList)
        {
            if (stat.statisticId == efect.typeStatistic)
            {
                if (efect.isMultipler == true)
                {
                    point = Math.Round(stat.basePoint * efect.valueEfect/100,5);
                }
                else
                    point = efect.valueEfect;
                if (point >= 0)
                {
                    stat.changeActualPoint(OperationName.Add, point);
                }
                else
                {
                    stat.changeActualPoint(OperationName.Sub, point);
                }
            }
        }
    }
    public void GetDamage(double damage)
    {
        double helth = 0;
        double armor = 0;
        if (this.statisticList.Count == 0)
            Debug.Log("StatisticList is empty");
        foreach (var stat in this.statisticList)
        {
            if (stat.statisticId == StatName.Helth)
            {
                helth = stat.actualPoint;
            }
            else if(stat.statisticId == StatName.Armor)
            {
                armor = stat.actualPoint;
            }
        }
        if(armor>0)
        {
            foreach (var stat in this.statisticList)
            {
                if (stat.statisticId == StatName.Armor)
                {
                    stat.changeActualPoint(OperationName.Sub, Math.Round(damage, 5));
                }
            }
        }
        else
        {
            foreach (var stat in this.statisticList)
            {
                if (stat.statisticId == StatName.Helth)
                {
                    stat.changeActualPoint(OperationName.Sub, Math.Round(damage, 5));
                }
            }
        }
    }
    
    public double TakeDamage()
    {
        double damage = 0;
        double rangeDamage = 0;
        foreach (var stat in this.statisticList)
        {
            if (stat.statisticId == StatName.Damage)
            {
                damage = stat.actualPoint;
            }
            else if(stat.statisticId == StatName.RangeDamage)
            {
                rangeDamage = stat.actualPoint;
            }
        }
        if (rangeDamage > 0)
            return Math.Round(rangeDamage, 5);
        return Math.Round(damage, 5);
    }
    public void AddItemToInventory(ItemV2 item)
    {
        inventory.AddItem(item);
    }
    private void Start()
    {

        itemManager = GameObject.FindWithTag("ItemManager").GetComponent<ItemManager>();
        foreach (var stat in statisticList)
        {
            stat.setFirstStartSetting();
        }
        //inventory = GameObject.FindObjectOfType<Inventory>();
    }
    public void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.V))
        {
            GetDamage(TakeDamage());
        }*/
    }

}
