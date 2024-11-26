using cfEngine.Rt;
using UnityEngine.UIElements;

public class UIElement
{
    protected VisualElement VisualElement { get; private set; }
    
    public virtual void AssignVisualElement(VisualElement visualElement)
    {
        if (VisualElement != null)
        {
            VisualElement.dataSource = null;
        }
        
        VisualElement = visualElement;
        visualElement.dataSource = this;
    }
}

public static class UIElementExtension
{
    public static RtReadOnlyList<UIElement> ToUISource<T>(this RtReadOnlyList<T> source) where T : UIElement
    {
        return source.SelectLocal(t => (UIElement)t);
    }
}
