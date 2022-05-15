public class EquipmentSlot : itemSlot
{
    public ItemType equipmentClassType;
    public int iditem;
    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = equipmentClassType.ToString() + " Slot";
    }
}
