using System;
using cfEngine.Rt;
using UnityEngine.UIElements;

public abstract class UIElementBase: IDisposable
{
    public abstract void Dispose();
}

public class UIElement<TVisualType>: UIElementBase, IDisposable where TVisualType: VisualElement
{
    protected TVisualType VisualElement { get; private set; }
    
    public void AssignVisualElement(TVisualType visualElement)
    {
        if (VisualElement != null)
        {
            VisualElement.dataSource = null;
        }
        
        VisualElement = visualElement;
        visualElement.dataSource = this;
    }

    public override void Dispose()
    {
        if (VisualElement != null)
        {
            VisualElement.dataSource = null;
        }

        VisualElement = null;
    }
}

public class UIElement : UIElement<VisualElement>
{
    
}

public class ListElement<T> : UIElement<ReadOnlyListView> where T : UIElementBase
{
    
}

public static class UIElementExtension
{
    public static RtReadOnlyList<UIElement> ToUISource<T>(this RtReadOnlyList<T> source) where T : UIElement
    {
        return source.SelectLocal(t => (UIElement)t);
    }
}
