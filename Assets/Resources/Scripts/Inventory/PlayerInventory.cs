using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory {
    public ItemDatabase ItemDB;

    public override void Start()
    {
        base.Start();

        ItemDB = base.database;

        AddItem(0);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(2);
        AddItem(3);
        AddItem(4);
        AddItem(5);

        AddMoney(400);
    }
}
