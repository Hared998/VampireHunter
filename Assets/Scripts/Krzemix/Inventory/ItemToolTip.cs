using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    [SerializeField] Text itemName;
    [SerializeField] Text statisticItem;
    [SerializeField] Text efectNameItem;

    public void ShowInfo(ItemV2 item)
    {
        itemName.text = item.itemName + " info:";
        statisticItem.text = item.setDescriptionStatItem();
        efectNameItem.text = item.setDescriptionEfectItem();
    }
    public void HideInfo()
    {
        itemName.text = "";
        statisticItem.text = "";
        efectNameItem.text = "";
    }
}
