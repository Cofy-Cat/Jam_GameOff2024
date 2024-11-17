using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cfEngine.Logging;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UI: MonoInstance<UI>, IDisposable
{
    public class PanelConfig
    {
        public string path;
        public IUIPanel panel;
    }
    
    public new static Func<UI> createMethod => () => Game.Asset.Load<UI>("UIRoot");

    public override bool persistent => true;
    
    [SerializeField] private UIDocument uiRootDocument;
    
    private Dictionary<Type, PanelConfig> _registeredPanelMap = new();
    private Dictionary<Type, Task<TemplateContainer>> _panelLoadMap = new();

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (uiRootDocument == null)
        {
            uiRootDocument = GetComponent<UIDocument>();
        }
    }
#endif
    
    public void Register<T>(string panelPath, T panel) where T : IUIPanel
    {
        var type = typeof(T);
        if (_registeredPanelMap.ContainsKey(type))
        {
            Log.LogException(new ArgumentException($"UI.Register: panel type already registered: {type}"));
            return;
        }
        
        _registeredPanelMap.Add(type, new PanelConfig
        {
            path = panelPath,
            panel = panel
        });
    }

    public Task<TemplateContainer> LoadTemplate<T>() where T: IUIPanel
    {
        var type = typeof(T);
        if (!_registeredPanelMap.TryGetValue(type, out var config))
        {
            var ex = new KeyNotFoundException($"UI.LoadPanel: panel type not registered: {type}");
            Log.LogException(ex);
            return Task.FromException<TemplateContainer>(ex);
        }
        
        if (_panelLoadMap.TryGetValue(type, out var loadTask) && loadTask.IsCompletedSuccessfully)
        {
            return loadTask;
        }
        
        TaskCompletionSource<TemplateContainer> loadTaskSource = new();
        _panelLoadMap[type] = loadTaskSource.Task;

        Game.Asset.LoadAsync<VisualTreeAsset>(config.path, Game.TaskToken)
            .ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    loadTaskSource.SetException(t.Exception);
                    return;
                }

                var template = t.Result.Instantiate();
                loadTaskSource.SetResult(template);
            }, Game.TaskToken);

        return loadTaskSource.Task;
    }

    public void AttachTemplate(TemplateContainer template)
    {
        template.enabledSelf = false;
        uiRootDocument.rootVisualElement.Add(template);
    }

    public void Dispose()
    {
        foreach (var config in _registeredPanelMap.Values)
        {
            config.panel.Dispose();
        }
        
        _registeredPanelMap.Clear();
        
        foreach (var task in _panelLoadMap.Values)
        {
            task.Dispose();
        }
        
        _panelLoadMap.Clear();
        
        uiRootDocument.rootVisualElement.Clear();
    }
}