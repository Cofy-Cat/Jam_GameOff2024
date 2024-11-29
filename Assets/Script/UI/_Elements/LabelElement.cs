using cfEngine.Rt;
using UnityEngine.UIElements;

public class LabelElement: UIElement<Label>
{
    private Rt<string> _text = new(string.Empty);

    private SubscriptionHandle _onUpdateHandle;
    
    public override void Dispose()
    {
        base.Dispose();
        
        _onUpdateHandle.UnsubscribeIfNotNull();
        _text.Dispose();
        VisualElement.text = string.Empty;
    }
    
    public override void AttachFromRoot(VisualElement root, string visualElementName = null)
    {
        AttachVisual(root.Q<Label>(visualElementName));
    }
    
    public LabelElement(string defaultValue = ""): base()
    {
        _text.Set(defaultValue);
    }
    
    protected override void OnVisualAttached()
    {
        base.OnVisualAttached();

        _onUpdateHandle.UnsubscribeIfNotNull();
        VisualElement.text = _text.Value;
        _onUpdateHandle = _text.Events.Subscribe(onUpdate: (_, newText) =>
        {
            VisualElement.text = newText;
        });
    }
    
    public void SetText(string text)
    {
        _text.Set(text);
    }

}