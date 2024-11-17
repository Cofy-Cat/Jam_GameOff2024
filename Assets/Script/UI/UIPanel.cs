using System;
using cfEngine.Logging;
using UnityEngine.UIElements;

public abstract class UIPanel: IDisposable
{
    protected TemplateContainer template;
    
    protected abstract string Key { get; }
    
    public void ShowPanel()
    {
        if (template == null)
        {
            UI.Instance.LoadTemplate(Key)
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Log.LogException(task.Exception);
                        return;
                    }
                    
                    template = task.Result;

                    Init(template);
                    ShowPanel(template);
                });
        }
        else
        {
            ShowPanel(template);
        }
    }

    public virtual void Init(TemplateContainer template)
    {
        template.dataSource = this;
        UI.Instance.AttachTemplate(template);
    }

    public virtual void ShowPanel(TemplateContainer template)
    {
        template.enabledSelf = true;
        
        OnPanelShown();
    }

    protected virtual void OnPanelShown() { }
    
    public abstract void Dispose();
}