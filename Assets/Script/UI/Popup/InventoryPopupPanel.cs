using cfEngine.Rt;
using UnityEngine.UIElements;

public class InventoryPopupPanel: UIPanel
{
    private RtReadOnlyList<InventoryPopupPanel_Item> _inventoryItemList;

    public InventoryPopupPanel(TemplateContainer template) : base(template)
    {
        var inventory = Game.Meta.Inventory;
        _inventoryItemList = inventory.StackMap.RtValues.Select(item => new InventoryPopupPanel_Item(item));
    }
    
    public override void Dispose()
    {
    }

    public class InventoryPopupPanel_Item
    {
        public InventoryPopupPanel_Item(in InventoryItem item)
        {
            
        }
    }
}
