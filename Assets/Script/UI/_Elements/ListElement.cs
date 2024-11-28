using System.Collections.Generic;
using System.Linq;
using cfEngine.Rt;
using UnityEngine.UIElements;

public class ListElement<T> : UIElement<ReadOnlyListView> where T : UIElement
{
    private IEnumerable<UIElement> _itemsSource;
    
    public override void AttachFromRoot(VisualElement root, string visualElementName = null)
    {
        AttachVisual(root.Q<ReadOnlyListView>(visualElementName));
    }

    protected override void OnVisualAttached()
    {
        base.OnVisualAttached();
        VisualElement.itemsSource = _itemsSource;
    }

    public void SetItemsSource(RtReadOnlyList<T> items)
    {
        _itemsSource = items.ToUISource();
    }

    public void SetItemsSource(IReadOnlyList<T> items)
    {
        _itemsSource = items.Select(t => (UIElement)t);
    }

    public override void Dispose()
    {
        base.Dispose();
        
        VisualElement.itemsSource = null;
        VisualElement._Clear();
    }
}

