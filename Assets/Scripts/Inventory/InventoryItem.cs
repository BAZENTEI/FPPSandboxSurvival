using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
	private string itemName;
	private int itemNum;

	public string ItemName{
		get { return itemName; }
		set { itemName = value; }
	}

	public int ItemNum{
		get { return itemNum; }
		set { itemNum = value; }
	}

	public InventoryItem(){}
	public InventoryItem(string itemName,int itemNum){
		this.ItemName = itemName;
		this.itemNum = itemNum;

	}

	public override string ToString(){
		return string.Format("");
	}
}
