using System;
using cfEngine.Rt;
using UnityEngine.UIElements;

public class UIElement: IDisposable
{
    protected VisualElement VisualElement { get; private set; }
    
    public void AssignVisualElement(VisualElement visualElement)
    {
        if (VisualElement != null)
        {
            VisualElement.dataSource = null;
        }
        
        VisualElement = visualElement;
        visualElement.dataSource = this;
    }

    public void Dispose()
    {
        if (VisualElement != null)
        {
            VisualElement.dataSource = null;
        }

        VisualElement = null;
    }
}

public class ListElement<T> : UIElement where T : UIElement
{
    
}

public static class UIElementExtension
{
    public static RtReadOnlyList<UIElement> ToUISource<T>(this RtReadOnlyList<T> source) where T : UIElement
    {
        return source.SelectLocal(t => (UIElement)t);
    }
}
