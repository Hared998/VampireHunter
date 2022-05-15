using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranged : MonoBehaviour
{
    public RangedItem item;
    public ARrow arrow;
    public double realDamage { get; private set; }

    public void ChangetWearableImage()
    {
        SpriteRenderer render = gameObject.GetComponent<SpriteRenderer>();
        if (render.sprite == null)
            render.sprite = item.WearableIcon;
        else
            render.sprite = null;
    }
    public void SetRealDamage(double damage)
    {
        realDamage = damage;
    }
    public void EquipRanged(ItemV2 item)
    {
        if (item is RangedItem)
        {
            this.item = (RangedItem)item;
            SetArrowDamage();
            ChangetWearableImage();
        }
    }
    public void UnequipRanged()
    {
        ChangetWearableImage();
        this.item = null;
    }
    public void SetArrowDamage()
    {
        arrow.damage = (float)realDamage;
    }
}
