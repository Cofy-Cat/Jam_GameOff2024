using System;
using cfEngine.Logging;
using UnityEngine.UIElements;

public abstract class UIPanel: UIElement
{
    protected readonly TemplateContainer Template;

    public UIPanel(TemplateContainer template)
    {
        Template = template;
        AssignVisualElement(template.contentContainer);
    }
    
    public void ShowPanel()
    {
        _ShowPanel();
    }

    protected virtual void _ShowPanel()
    {
        Template.enabledSelf = true;
        Template.RemoveFromClassList("hide");
        Template.AddToClassList("show");
        OnPanelShown();
    }

    protected virtual void OnPanelShown() { }

    public virtual void HidePanel()
    {
        Template.RemoveFromClassList("show");
        Template.AddToClassList("hide");
        Template.enabledSelf = false;
    }
    
    public abstract void Dispose();
}