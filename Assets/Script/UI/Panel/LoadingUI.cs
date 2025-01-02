using cfUnityEngine.UI;
using UnityEngine.UIElements;

public class LoadingUI: UIPanel
{
    public string message = "Loading...";

    protected override void OnVisualAttached()
    {
        
    }

    public override void Dispose()
    {
        base.Dispose();
    }
}
