using System.Collections.Generic;
using System.Linq;
using cfEngine.Rt;
using UnityEngine.UIElements;

public class ListElement<T> : UIElement<ReadOnlyListView> where T : UIElement
{
    private Rt<IEnumerable<UIElement>> _itemsSource = new();
    
    private SubscriptionHandle _onUpdateHandle;
    
    public override void Dispose()
    {
        base.Dispose();
        
        _onUpdateHandle.UnsubscribeIfNotNull();
        _itemsSource.Dispose();
    }
    
    protected override void OnVisualAttached()
    {
        _onUpdateHandle.UnsubscribeIfNotNull();
        VisualElement.itemsSource = _itemsSource.Value;
        _onUpdateHandle = _itemsSource.Events.Subscribe(onUpdate: (_, newItem) =>
        {
            VisualElement.itemsSource = newItem.item;
        });
    }

    public void SetItemsSource(RtReadOnlyList<T> items)
    {
        _itemsSource.Set(items.ToUISource());
    }

    public void SetItemsSource(IReadOnlyList<T> items)
    {
        _itemsSource.Set(items.Select(t => (UIElement)t));
    }
}

