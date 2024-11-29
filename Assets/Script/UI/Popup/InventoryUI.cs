using System;
using cfEngine.Logging;
using cfEngine.Meta;
using cfEngine.Meta.Inventory;
using cfEngine.Rt;
using UnityEditor;
using UnityEngine;
using StackId = System.Guid;

public class InventoryUI: UIPanel
{
    RtSelectList<Guid, InventoryUI_Item> _items;
    ListElement<InventoryUI_Item> itemList = new();
    ListElement<InventoryUI_PaginationButton> paginationButtonList = new();
    
    protected override void OnVisualAttached()
    {
        base.OnVisualAttached();
        
        AttachChild(itemList, "item-list");
    }
    
    public InventoryUI(): base()
    {
        var inventory = Game.Meta.Inventory;
        
        _items = inventory.GetPage(0).Select(stackId => new InventoryUI_Item(stackId));
        itemList.SetItemsSource(_items);

        paginationButtonList = new ListElement<InventoryUI_PaginationButton>();
    }

    public override void Dispose()
    {
        itemList.Dispose();
        _items.Dispose();
    }

    public class InventoryUI_Item: UIElement
    {
        LabelElement titleLabel = new();
        LabelElement countLabel = new();
        SpriteElement iconSprite = new();

        protected override void OnVisualAttached()
        {
            base.OnVisualAttached();
            
            AttachChild(titleLabel, "title-label");
            AttachChild(countLabel, "count-label");
            AttachChild(iconSprite, "icon-sprite");
        }
        
        public InventoryUI_Item(StackId stackId)
        {
            if(stackId == Guid.Empty) return;
            
            var item = Game.Meta.Inventory.StackMap[stackId];
            
            titleLabel.SetText(item.Id);
            countLabel.SetText($"x{item.ItemCount}");
            
            var info = Game.Info.Get<InventoryInfoManager>().GetOrDefault(item.Id);
            iconSprite.spritePath.Set(info.iconKey);
        }

        public override void Dispose()
        {
            base.Dispose();
            
            titleLabel.Dispose();
            titleLabel = null;
            countLabel.Dispose();
            countLabel = null;
        }
    }
    
    public class InventoryUI_PaginationButton: UIElement
    {
        public readonly int page;
        
        public InventoryUI_PaginationButton(int page)
        {
            this.page = page;
        }
    }

    [MenuItem("Test/Add Random Item")]
    public static void AddRandomItem()
    {
        var inventory = Game.Meta.Inventory;
        var random = new System.Random();
        var itemId = $"item{random.Next(1, 10)}";
        var count = random.Next(1, 30);
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
