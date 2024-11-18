using System;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPopupPanel: UIPanel<InventoryPopupPanel>
{
    private List<InventoryItemUI> _inventoryItemList = new();
    public IReadOnlyList<InventoryItemUI> InventoryItemList => _inventoryItemList;

    public InventoryPopupPanel()
    {
        
    }
    
    public override void Dispose()
    {
    }
}

public class InventoryItemUI
{
    
}