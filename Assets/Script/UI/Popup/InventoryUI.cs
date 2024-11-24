using System;
using cfEngine.Meta.Inventory;
using cfEngine.Rt;
using UnityEditor;
using UnityEngine.UIElements;
using StackId = System.Guid;

public class InventoryUI: UIPanel
{
    private RtSelectList<Guid, InventoryUI_Item> _items;
    private ReadOnlyListView itemListView;
    
    public InventoryUI(TemplateContainer template) : base(template)
    {
        var inventory = Game.Meta.Inventory;
        _items = inventory.GetPage(0).Select(stackId => new InventoryUI_Item(stackId));
        
        var itemUIListElement = template.Q<ReadOnlyListView>();
        itemUIListElement.rtItemsSource = _items;
    }

    public override void Dispose()
    {
        itemListView._Clear();
        _items.Dispose();
    }

    public class InventoryUI_Item
    {
        public string itemId;
        public readonly int count;
        public readonly string countText;
        
        public InventoryUI_Item(StackId stackId)
        {
            if(stackId == Guid.Empty) return;
            
            var item = Game.Meta.Inventory.StackMap[stackId];

            itemId = item.Id;
            count = item.ItemCount;
            countText = $"x{count}";
        }
    }
    
    [MenuItem("Test/Add Random Item")]
    public static void AddRandomItem()
    {
        var inventory = Game.Meta.Inventory;
        var random = new System.Random();
        var itemIds = new[] {"item1", "item2", "item3"};
        var itemId = itemIds[random.Next(itemIds.Length)];
        var count = random.Next(1, 10);
        inventory.AddItem(new InventoryController.UpdateInventoryRequest
        {
            ItemId = itemId,
            Count = count
        });
    }
    
    [MenuItem("Test/Show Inventory Panel")]
    public static void Show()
    {
        UIRoot.GetPanel<InventoryUI>().ShowPanel();
    }
}
