using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
    Helmet = 0,
    Chestplate,
    Leggings,
    Boots,
    Sword,
    Ranged,
    Petard,
    Mixture,
    Weapon,
    Other
}
public class XStat
{
    public StatName statName;
    public bool isMultipe;
    public double value;
    public XStat(StatName statName, bool isMultipe, double value)
    {
        this.statName = statName;
        this.isMultipe = isMultipe;
        this.value = value;
    }
}

[CreateAssetMenu(fileName = "NewItem", menuName = "Equipment/Item")]
public class ItemV2 : ScriptableObject
{
    public int idItem = 0;
    public string itemName = "Item";
    public ItemType typeItem = ItemType.Other;
    public string descriptionItem = "";
    public bool isEnemyGet = true;
    public bool isActive = false;
    public List<Efect> efectList = new List<Efect>();
    public Animation animation;
    public Sprite itemIcon = null;
    public Sprite WearableIcon;
    public Sprite AnimationIcon;

    public void changeActiveItemStatus()
    {
        if (isActive == true)
            isActive = false;
        else
            isActive = true;
    }
   
    public string setDescriptionEfectItem()
    {
        string describe = "";
        foreach (var efect in efectList)
        {
            describe += "(" + efect.efectName + ")\n";
        }
        return describe;
    }
    public string setDescriptionStatItem()
    {
        string description = "";
        List<XStat> xStats = new List<XStat>();
        foreach(var efect in efectList)
        {
            if (isInTab(xStats, efect.typeStatistic, efect.isMultipler))
            {
                xStats[positionInTab(xStats, efect.typeStatistic, efect.isMultipler)].value += efect.valueEfect;
            }
            else
                xStats.Add(new XStat(efect.typeStatistic, efect.isMultipler, efect.valueEfect));
        }
        foreach(var x in xStats)
        {
            description += x.statName.ToString() + ": ";
            if(x.value > 0)
            {
                description += "+";
            }
            else if(x.value < 0)
            {
                description += "-";
            }
            description += x.value;
            if (x.isMultipe == true)
            {
                description += "%";
            }
            description += "\n";
        }
        return description;
    }
    private bool isInTab(List<XStat> tab, StatName statName, bool isMultiple)
    {
        if (tab == null)
            return false;
        for(int i=0;i<tab.Count;i++)
        {
            if (tab[i].statName == statName && tab[i].isMultipe == isMultiple)
                return true;
        }
        return false;
    }
    private int positionInTab(List<XStat> tab, StatName statName, bool isMultiple)
    {
        int i = 0;
        for(;i<tab.Count;i++)
        {
            if (tab[i].statName == statName && tab[i].isMultipe == isMultiple)
                break;
        }
        return i;
    }
}
