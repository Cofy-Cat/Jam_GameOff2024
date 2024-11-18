using System;
using System.Threading.Tasks;
using cfEngine.Logging;
using UnityEngine.UIElements;

public interface IUIPanel: IDisposable
{
    void ShowPanel();
    void AssignTemplate(TemplateContainer template);
}

public abstract class UIPanel<TPanel>: IUIPanel where TPanel : IUIPanel
{
    protected TemplateContainer template;

    public void ShowPanel()
    {
        if (template == null)
        {
            Log.LogWarning("UIPanel.ShowPanel: template is null, calling Init first, dont rely on auto init.");
            Init().ContinueWithSynchronized(t => _ShowPanel(), Game.TaskToken);
        }
        else
        {
            _ShowPanel();
        }
    }

    public virtual Task Init()
    {
        return UI.Instance.LoadTemplate<TPanel>()
            .ContinueWithSynchronized(task =>
            {
                if (task.IsFaulted)
                {
                    Log.LogException(task.Exception);
                    return;
                }
                
                AssignTemplate(task.Result);
                Init(task.Result);
            }, Game.TaskToken);
    }

    public virtual void Init(TemplateContainer template)
    {
        
    }

    public virtual void AssignTemplate(TemplateContainer loadedTemplate)
    {
        template = loadedTemplate;
        
        UI.Instance.AttachTemplate(template);
        template.dataSource = this;
    }

    protected virtual void _ShowPanel()
    {
        if(template == null)
        {
            Log.LogException(new InvalidOperationException("UIPanel._ShowPanel: template is null, call UIPanel.Init first"));
            return;
        }
        
        template.enabledSelf = true;
        template.RemoveFromClassList("hide");
        template.AddToClassList("show");
        OnPanelShown();
    }

    protected virtual void OnPanelShown() { }

    public virtual void HidePanel()
    {
        template.RemoveFromClassList("show");
        template.AddToClassList("hide");
        template.enabledSelf = false;
    }
    
    public abstract void Dispose();
}