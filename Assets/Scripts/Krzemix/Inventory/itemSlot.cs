using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class itemSlot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image slotImage;
    [SerializeField] ItemToolTip itemToolTip;

    public event Action<ItemV2> OnLeftClickEvent;
    public event Action<ItemV2> OnRightClickEvent;
    private ItemV2 _item;
    public ItemV2 itemInSlot
    {
        get { return _item; }
        set
        {
            _item = value;
            if (_item == null) { slotImage.sprite = null; slotImage.enabled = false; }
            else
            {
                slotImage.sprite = _item.itemIcon;
                slotImage.enabled = true;
            }
        }
    }
    public void RemoveItemSettings()
    {
        slotImage.sprite = null;
        slotImage.enabled = false;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData != null && eventData.button == PointerEventData.InputButton.Left)
        {
            if (itemInSlot != null && OnLeftClickEvent != null)
            {
                OnLeftClickEvent(itemInSlot);
            }
        }
        if (eventData != null && eventData.button == PointerEventData.InputButton.Right)
        {
            if (itemInSlot != null && OnLeftClickEvent != null)
            {
                OnRightClickEvent(itemInSlot);
            }
        }
    }

    protected virtual void OnValidate()
    {
        if(slotImage == null)
        {
            slotImage = GetComponent<Image>();
        }
        if(itemToolTip == null)
        {
            itemToolTip = FindObjectOfType<ItemToolTip>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData != null)
        {
            itemToolTip.ShowInfo(itemInSlot);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (eventData != null)
        {
            itemToolTip.HideInfo();
        }
    }


}
