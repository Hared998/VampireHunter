using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Equipment/Ranged")]
public class RangedItem : WaeponItem
{
    RangedItem():base()
    {
        {
            this.idItem = (int)ItemType.Ranged * 1000;
            this.typeItem = ItemType.Ranged;
        }
    }
}
