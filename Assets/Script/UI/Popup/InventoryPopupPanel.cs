using System;
using System.Collections.Generic;
using cfEngine.Logging;
using cfEngine.Meta;
using cfEngine.Rt;
using UnityEditor;
using UnityEngine.UIElements;
using StackId = System.Guid;

public class InventoryPopupPanel: UIPanel
{
    private RtReadOnlyList<InventoryPopupPanel_Item> _items;
    
    private List<VisualElement> itemElements = new();

    public InventoryPopupPanel(TemplateContainer template) : base(template)
    {
        var inventory = Game.Meta.Inventory;
        _items = inventory.GetPage(0).Select(stackId => new InventoryPopupPanel_Item(stackId));

        var itemUIListElement = template.Q(nameof(itemElements));
        itemElements.AddRange(itemUIListElement.Children());

        if (itemElements.Count < _items.Count)
        {
            Log.LogException(new InvalidOperationException($"Not enough item visual element for 1 page in the template. ve: {itemElements.Count}, page: {_items.Count}"));
        }
        else
        {
            for (var i = 0; i < _items.Count; i++)
            {
                itemElements[i].dataSource = _items[i];
            }
        }
    }
    
    public override void Dispose()
    {
        itemElements.Clear();
        _items.Dispose();
    }

    public class InventoryPopupPanel_Item
    {
        public InventoryPopupPanel_Item(StackId stackId)
        {
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
}
