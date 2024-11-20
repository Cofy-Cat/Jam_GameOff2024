using System.Collections.Generic;
using cfEngine.Meta;
using cfEngine.Rt;
using Unity.Properties;
using UnityEngine.UIElements;

public class InventoryPopupPanel: UIPanel
{
    [CreateProperty]
    private RtReadOnlyList<InventoryPopupPanel_Item> _inventoryItemList;
    
    public List<InventoryPopupPanel_Item> testList = new List<InventoryPopupPanel_Item>();

    public InventoryPopupPanel(TemplateContainer template) : base(template)
    {
        var inventory = Game.Meta.Inventory;
        _inventoryItemList = inventory.StackMap.RtValues.Select(item => new InventoryPopupPanel_Item(item));

        var listView = template.Q<ListView>();
        listView.dataSource = testList;
        listView.SetBinding("itemsSource", new DataBinding(){ dataSourcePath = new PropertyPath("testList") });
        
        testList.Add(new InventoryPopupPanel_Item(null));
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
