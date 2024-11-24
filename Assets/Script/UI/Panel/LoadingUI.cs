using UnityEngine.UIElements;

public class LoadingUI: UIPanel
{
    public string message = "Loading...";
    
    public LoadingUI(TemplateContainer template) : base(template)
    {
    }
    
    public override void Dispose()
    {
    }
}
