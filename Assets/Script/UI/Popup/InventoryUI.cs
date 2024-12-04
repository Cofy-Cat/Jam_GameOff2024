using System;
using cfEngine.Meta;
using cfEngine.Rt;
using cfEngine.Util;
using StackId = System.Guid;

public class InventoryUI: UIPanel
{
    ListElement<InventoryUI_Item> itemList = new();
    LabelElement pageLabel = new();
    ButtonElement previousPageButton = new();
    ButtonElement nextPageButton = new();

    RtSelectList<Guid, InventoryUI_Item> _items;
    Rt<int> currentPage = new(0);

    Subscription _pageCountSub;
    Subscription _currentPageSub;
    
    private const string CURRENT_PAGE = "current";
    private const string TOTAL_PAGE = "total";

    protected override void OnVisualAttached()
    {
        AttachChild(itemList, "item-list");
        AttachChild(pageLabel, "page-label");
        AttachChild(previousPageButton, "previous-page-button");
        AttachChild(nextPageButton, "next-page-button");
    }
    
    public InventoryUI()
    {
        var inventory = Game.Meta.Inventory;
       
        UpdateItems();
        _currentPageSub = currentPage.Events.OnChange(UpdateItems);
        void UpdateItems()
        {
            _items?.Dispose();
            _items = inventory.GetPage(currentPage.Value).SelectNew(stackId => new InventoryUI_Item(stackId));
            itemList.SetItemsSource(_items);
        }

        pageLabel.SetTemplate(CURRENT_PAGE, currentPage.Select(count => (++count).ToString()));
        pageLabel.SetTemplate(TOTAL_PAGE, inventory.Pages.Count().Select(count => count.ToString()));
        
        previousPageButton.SetOnClick(() =>
        {
            if(currentPage.Value > 0)
                currentPage.Set(currentPage.Value - 1);
        });
        nextPageButton.SetOnClick(() =>
        {
            if(currentPage.Value < inventory.Pages.Count - 1)
                currentPage.Set(currentPage.Value + 1);
        });
    }

    public override void Dispose()
    {
        base.Dispose();
        
        _pageCountSub.UnsubscribeIfNotNull();
        _currentPageSub.UnsubscribeIfNotNull();
        
        itemList.Dispose();
        pageLabel.Dispose();
        _items.Dispose();
        
        currentPage.Dispose();
        
        previousPageButton.Dispose();
        nextPageButton.Dispose();
    }

    public class InventoryUI_Item: UIElement
    {
        LabelElement titleLabel = new();
        LabelElement countLabel = new();
        SpriteElement iconSprite = new();

        protected override void OnVisualAttached()
        {
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
            countLabel.Dispose();
            iconSprite.Dispose();
        }
    }
}
