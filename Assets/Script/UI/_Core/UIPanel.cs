using System;
using cfEngine.Logging;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

public abstract class UIPanel: UIElement<TemplateContainer>
{

    [Preserve]
    protected UIPanel()
    {
    }

    public override void AttachFromRoot(VisualElement root, string visualElementName = null)
    {
        Log.LogException(new InvalidOperationException("UIPanel cannot be attached from root. UIPanel is created from template, use constructor to create UIPanel."));
    }

    public void ShowPanel()
    {
        _ShowPanel();
    }

    protected virtual void _ShowPanel()
    {
        VisualElement.enabledSelf = true;
        VisualElement.RemoveFromClassList("hide");
        VisualElement.AddToClassList("show");
        OnPanelShown();
    }

    protected virtual void OnPanelShown() { }

    public virtual void HidePanel()
    {
        VisualElement.RemoveFromClassList("show");
        VisualElement.AddToClassList("hide");
        VisualElement.enabledSelf = false;
    }
    
    public abstract void Dispose();
}