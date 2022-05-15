using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Equipment/Sword")]
public class SwordItem : WaeponItem
{
    SwordItem() : base()
    {
        this.idItem = (int)ItemType.Sword * 1000;
        this.typeItem = ItemType.Sword;
    }
    
}
