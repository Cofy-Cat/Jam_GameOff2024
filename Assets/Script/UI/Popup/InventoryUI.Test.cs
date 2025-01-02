using cfEngine.Meta.Inventory;
using cfUnityEngine.UI;
using UnityEditor;

public class InventoryUI_Test
{
    [MenuItem("Test/Dispose UI")]
    public static void DisposeUI()
    {
        UIRoot.Instance.Dispose();
    }
    
    [MenuItem("Test/Add Random Item")]
    public static void AddRandomItem()
    {
        var inventory = Game.Meta.Inventory;
        var random = new System.Random();
        var itemId = $"item{random.Next(1, 10)}";
        var count = random.Next(1, 1);
        inventory.AddItem(new InventoryController.UpdateRequest
        {
            ItemId = itemId,
            Count = count
        });
    }
    
    [MenuItem("Test/Show Inventory Panel")]
    public static void Show()
    {
        UIRoot.GetPanel<InventoryUI>().ShowPanel() ;
    }
}