using System;
using cfEngine.Logging;
using UnityEngine.UIElements;

public interface IUIPanel: IDisposable
{
    void ShowPanel();
    void Init(TemplateContainer template);
}

public abstract class UIPanel<TPanel>: IUIPanel where TPanel : IUIPanel
{
    protected TemplateContainer template;

    public void ShowPanel()
    {
        if (template == null)
        {
            UI.Instance.LoadTemplate<TPanel>()
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Log.LogException(task.Exception);
                        return;
                    }
                    
                    Init(task.Result);
                    _ShowPanel();
                });
        }
        else
        {
            _ShowPanel();
        }
    }

    public virtual void Init(TemplateContainer loadedTemplate)
    {
        template = loadedTemplate;
        
        UI.Instance.AttachTemplate(template);
        template.dataSource = this;
    }

    public virtual void _ShowPanel()
    {
        if(template == null)
        {
            Log.LogException(new InvalidOperationException("UIPanel._ShowPanel: template is null, call UIPanel.Init first"));
            return;
        }
        
        template.enabledSelf = true;
        OnPanelShown();
    }

    protected virtual void OnPanelShown() { }
    
    public abstract void Dispose();
}