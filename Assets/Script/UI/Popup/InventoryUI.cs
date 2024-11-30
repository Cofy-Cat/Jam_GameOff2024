using System;
using cfEngine.Meta;
using cfEngine.Meta.Inventory;
using cfEngine.Rt;
using StackId = System.Guid;

public class InventoryUI: UIPanel
{
    RtSelectList<Guid, InventoryUI_Item> _items;
    ListElement<InventoryUI_Item> itemList = new();
    LabelElement pageLabel = new();

    RtCount<InventoryController.PageRecord> _pageCount;

    SubscriptionHandle _pageCountSub;
    
    private const string CURRENT_PAGE = "current";
    private const string TOTAL_PAGE = "total";

    protected override void OnVisualAttached()
    {
        base.OnVisualAttached();
        
        AttachChild(itemList, "item-list");
        AttachChild(pageLabel, "page-label");
    }
    
    public InventoryUI(): base()
    {
        var inventory = Game.Meta.Inventory;
        
        _items = inventory.GetPage(0).Select(stackId => new InventoryUI_Item(stackId));
        itemList.SetItemsSource(_items);

        pageLabel.SetTemplate(TOTAL_PAGE, inventory.Pages.Count().SelectLocal(count => count.ToString()));
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
}
