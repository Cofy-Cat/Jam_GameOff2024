using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
public class UI: MonoInstance<UI>
{
    public new static Func<UI> createMethod => () => Game.Asset.Load<UI>("UIRoot");
    public override bool persistent => true;
    
    [SerializeField] private UIDocument uiRootDocument;
    
#if UNITY_EDITOR
    private void OnValidate()
    {
        if (uiRootDocument == null)
        {
            uiRootDocument = GetComponent<UIDocument>();
        }
    }
#endif
}