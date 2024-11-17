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
    
    private Dictionary<string, PanelConfig> _registeredPanelMap = new();
    private Dictionary<string, Task<TemplateContainer>> _panelLoadMap = new();

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (uiRootDocument == null)
        {
            uiRootDocument = GetComponent<UIDocument>();
        }
    }
#endif
    
    public void Register<T>(string key, string panelPath, T panel) where T : UIPanel
    {
        var type = typeof(T);
        if (_registeredPanelMap.ContainsKey(key))
        {
            Log.LogException(new ArgumentException($"UI.Register: panel key already registered: {key}"));
            return;
        }
        
        _registeredPanelMap.Add(key, new PanelConfig
        {
            path = panelPath,
            panel = panel
        });
    }

    public Task<TemplateContainer> LoadTemplate(string key)
    {
        if (!_registeredPanelMap.TryGetValue(key, out var config))
        {
            var ex = new KeyNotFoundException($"UI.LoadPanel: panel key not registered: {key}");
            Log.LogException(ex);
            return Task.FromException<TemplateContainer>(ex);
        }
        
        if (_panelLoadMap.TryGetValue(key, out var loadTask))
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
                template.enabledSelf = false;
                
                _panelLoadMap[key] = loadTaskSource.Task;
                
                loadTaskSource.SetResult(template);
            }, Game.TaskToken);

        return loadTaskSource.Task;
    }

    public void AttachTemplate(TemplateContainer template)
    {
        uiRootDocument.rootVisualElement.Add(template);
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