using System;
using cfEngine.Meta;
using cfEngine.Rt;
using cfUnityEngine.UI;
using StackId = System.Guid;

public class InventoryUI: UIPanel
{
    private readonly ListElement<InventoryUI_Item> _itemList = new();
    private readonly LabelElement _pageLabel = new();
    private readonly ButtonElement _previousPageButton = new();
    private readonly ButtonElement _nextPageButton = new();
    
    private readonly Rt<int> _currentPage = new(0);
    
    private Subscription _pageCountSub;
    private Subscription _currentPageSub;
    private Subscription _itemUpdateSub;
    
    private const string CURRENT_PAGE = "current";
    private const string TOTAL_PAGE = "total";

    protected override void OnVisualAttached()
    {
        AttachChild(_itemList, "item-list");
        AttachChild(_pageLabel, "page-label");
        AttachChild(_previousPageButton, "previous-page-button");
        AttachChild(_nextPageButton, "next-page-button");
    }
    
    public InventoryUI()
    {
        var inventory = Game.Meta.Inventory;
        
        UpdateItems();
        _currentPageSub = _currentPage.Events.OnChange(UpdateItems);
        void UpdateItems()
        {
            _itemList.SetItemsSource(inventory.GetPage(_currentPage.Value).selectNew(stackId => new InventoryUI_Item(stackId)));
        }
        
        _pageLabel.SetTemplate(CURRENT_PAGE, _currentPage.select(count => (++count).ToString()));
        _pageLabel.SetTemplate(TOTAL_PAGE, inventory.Pages.count().select(count => count.ToString()));
        
        _previousPageButton.SetOnClick(() =>
        {
            if(_currentPage.Value > 0)
                _currentPage.Set(_currentPage.Value - 1);
        });
        _nextPageButton.SetOnClick(() =>
        {
            if(_currentPage.Value < inventory.Pages.Count - 1)
                _currentPage.Set(_currentPage.Value + 1);
        });
    }

    public override void Dispose()
    {
        base.Dispose();
        
        _pageCountSub.UnsubscribeIfNotNull();
        _currentPageSub.UnsubscribeIfNotNull();
        
        _itemList.Dispose();
        _pageLabel.Dispose();
        
        _currentPage.Dispose();
        _previousPageButton.Dispose();
        _nextPageButton.Dispose();
    }
    
    public class InventoryUI_Item: UIElement
    {
        private readonly LabelElement _titleLabel = new();
        private readonly LabelElement _countLabel = new();
        private readonly SpriteElement _iconSprite = new();
    
        protected override void OnVisualAttached()
        {
            AttachChild(_titleLabel, "title-label");
            AttachChild(_countLabel, "count-label");
            AttachChild(_iconSprite, "icon-sprite");
        }
        
        public InventoryUI_Item(StackId stackId)
        {
            if(stackId == Guid.Empty) return;
            
            var item = Game.Meta.Inventory.StackMap[stackId];
            
            _titleLabel.SetText(item.Id);
            _countLabel.SetText($"x{item.ItemCount}");
            
            var info = Game.Info.Get<InventoryInfoManager>().GetOrDefault(item.Id);
            _iconSprite.spritePath.Set(info.iconKey);
        }
    
        public override void Dispose()
        {
            base.Dispose();
            
            _titleLabel.Dispose();
            _countLabel.Dispose();
            _iconSprite.Dispose();
        }
    }
}
