using UnityEngine.UIElements;

public class LoadingPanel: UIPanel
{
    public string message = "Loading...";
    
    public LoadingPanel(TemplateContainer template) : base(template)
    {
    }
    
    public override void Dispose()
    {
    }
}
