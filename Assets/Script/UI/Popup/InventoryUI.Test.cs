using cfEngine.Meta.Inventory;
using UnityEditor;

public class InventoryUI_Test
{
    [MenuItem("Test/Add Random Item")]
    public static void AddRandomItem()
    {
        var inventory = Game.Meta.Inventory;
        var random = new System.Random();
        var itemId = $"item{random.Next(1, 10)}";
        var count = random.Next(1, 30);
        inventory.AddItem(new InventoryController.UpdateInventoryRequest
        {
            ItemId = itemId,
            Count = count
        });
    }
    
    [MenuItem("Test/Show Inventory Panel")]
    public static void Show()
    {
        UIRoot.GetPanel<InventoryUI>().ShowPanel();
    }
}