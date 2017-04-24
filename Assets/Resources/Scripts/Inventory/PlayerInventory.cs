using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : Inventory {
    public ItemDatabase ItemDB;

    public override void Start()
    {
        base.Start();

        ItemDB = base.database;
    }
}
