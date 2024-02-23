using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
	private int itemId;
	private string itemName;
	private int itemNum;
    private bool itemBar;

    public int ItemId
	{
		get { return itemId; }
		set { itemId = value; }
	}

	public string ItemName{
		get { return itemName; }
		set { itemName = value; }
	}

	public int ItemNum{
		get { return itemNum; }
		set { itemNum = value; }
	}

    public bool ItemBar
    {
        get { return itemBar; }
        set { itemBar = value; }
    }

    public InventoryItem(){}
	public InventoryItem(int itemId, string itemName,int itemNum){
		this.itemId = itemId;
		this.ItemName = itemName;
		this.itemNum = itemNum;

	}

	public override string ToString(){
		return string.Format("");
	}
}
