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
#if UNITY_EDITOR
                template.name = $"EditorName-{type}";
#endif
                loadTaskSource.SetResult(template);
            }, Game.TaskToken, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

        return loadTaskSource.Task;
    }

    public void AttachTemplate(TemplateContainer template)
    {
        template.enabledSelf = false;
        uiRootDocument.rootVisualElement.Add(template);
        template.StretchToParentSize();
    }
    
    public static T GetPanel<T>() where T : IUIPanel
    {
        var type = typeof(T);
        if (!Instance._registeredPanelMap.TryGetValue(type, out var config))
        {
            Log.LogException(new KeyNotFoundException($"UI.GetPanel: panel type not registered: {type}"));
            return default;
        }

        if (config.panel is T panel)
        {
            return panel;
        }
        
        Log.LogException(new InvalidCastException($"UI.GetPanel: panel type mismatch: {type}"));
        return default;
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