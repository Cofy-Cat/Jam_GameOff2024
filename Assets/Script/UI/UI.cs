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
        public UIPanel panel;
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
    
    public void Register<T>(string panelPath, T panel) where T : UIPanel
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

    public Task<TemplateContainer> LoadTemplate<T>() where T: UIPanel
    {
        if (!_registeredPanelMap.TryGetValue(typeof(T), out var config))
        {
            var ex = new KeyNotFoundException($"UI.LoadPanel: panel type not registered: {typeof(T)}");
            Log.LogException(ex);
            return Task.FromException<TemplateContainer>(ex);
        }
        
        if (_panelLoadMap.TryGetValue(typeof(T), out var loadTask))
        {
            return Task.FromResult(loadTask.Result);
        }
        
        TaskCompletionSource<TemplateContainer> loadTaskSource = new();

        Game.Asset.LoadAsync<VisualTreeAsset>(config.path, Game.TaskToken)
            .ContinueWith(t =>
            {
                if (t.IsFaulted && t.Exception != null)
                {
                    loadTaskSource.SetException(t.Exception);
                    return;
                }

                var template = t.Result.Instantiate();
                template.dataSource = config.panel;
                _panelLoadMap[typeof(T)] = loadTaskSource.Task;
                
                loadTaskSource.SetResult(template);
            }, Game.TaskToken);

        return loadTaskSource.Task;
    }

    public void Dispose()
    {
        foreach (var (type, config) in _registeredPanelMap)
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