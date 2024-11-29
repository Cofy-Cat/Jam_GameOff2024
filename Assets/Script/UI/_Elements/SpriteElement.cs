using cfEngine.Logging;
using cfEngine.Rt;
using UnityEngine;
using UnityEngine.UIElements;

public class SpriteElement: UIElement<VisualElement>
{
    public readonly Rt<string> spritePath = new();
    public readonly Rt<Sprite> sprite = new();
    
    private SubscriptionHandle _spritePathSub;
    private SubscriptionHandle _spriteSub;

    public override void Dispose()
    {
        base.Dispose();
        
        _spritePathSub.UnsubscribeIfNotNull();
        spritePath.Dispose();
        
        _spriteSub.UnsubscribeIfNotNull();
        sprite.Dispose();
    }

    public override void AttachFromRoot(VisualElement root, string visualElementName = null)
    {
        AttachVisual(root.Q(visualElementName));
    }
    
    protected override void OnVisualAttached()
    {
        base.OnVisualAttached();

        _spritePathSub.UnsubscribeIfNotNull();
        SetSprite(spritePath);
        _spritePathSub = spritePath.Events.Subscribe(onUpdate: (_, newSpritePath) =>
        {
            SetSprite(newSpritePath);
        });
        
        _spriteSub.UnsubscribeIfNotNull();
        VisualElement.style.backgroundImage = new StyleBackground(sprite);
        _spriteSub = sprite.Events.Subscribe(onUpdate: (_, newSprite) =>
        {
            VisualElement.style.backgroundImage = new StyleBackground(newSprite);
        });
    }

    private void SetSprite(string spritePath)
    {
        if (!string.IsNullOrEmpty(spritePath))
        {
            Game.Asset.LoadAsync<Sprite>(spritePath)
                .ContinueWithSynchronized(t =>
                {
                    if (!t.IsCompletedSuccessfully)
                    {
                        Log.LogException(t.Exception);
                        return;
                    }
                    
                    sprite.Set(t.Result);
                });
        }
    }
}