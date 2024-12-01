using System;
using cfEngine.Rt;
using UnityEngine.UIElements;

public class ButtonElement: UIElement<Button>
{
    Rt<Action> onClickAction = new();

    public override void Dispose()
    {
        base.Dispose();

        if (onClickAction.Value != null && VisualElement != null)
        {
            VisualElement.clicked -= onClickAction.Value;
        }
        
        onClickAction.Dispose();
    }

    protected override void OnVisualAttached()
    {
        if (onClickAction.Value != null)
        {
            VisualElement.clicked += onClickAction.Value;
        }
        
        onClickAction.Events.Subscribe(onUpdate: (oldAction, newAction) =>
        {
            VisualElement.clicked -= oldAction.item;
            VisualElement.clicked += newAction.item;
        });
    }
    
    public void SetOnClick(Action onClick)
    {
        onClickAction.Set(onClick);
    }
}