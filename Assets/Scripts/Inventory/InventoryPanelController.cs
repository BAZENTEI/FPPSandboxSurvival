﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour ,IUIPanelShowHide{
	public static InventoryPanelController Instance;

	private InventoryPanelView m_InventoryPanelView;
	private InventoryPanelModel m_InventoryPanelModel;
	private int slotNum = 27;
	private List<GameObject> slotList = new List<GameObject>();

	void Awake()  {
		Instance = this;
    }

	void Start () {
		m_InventoryPanelView = gameObject.GetComponent<InventoryPanelView>();
		m_InventoryPanelModel = gameObject.GetComponent<InventoryPanelModel>();

		CreateSlotAll();
		CreateItemAll();
	}
	
	//create slot
	private void CreateSlotAll(){
		for(int i = 0; i < slotNum; i++){
			GameObject tempSlot = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Slot(), m_InventoryPanelView.GetGridTransform());
			tempSlot.name = "InventorySlot" + i;
			slotList.Add(tempSlot);
		}
	}

	//create item 
	private void CreateItemAll(){
		List<InventoryItem> tempList = new List<InventoryItem>();
		tempList = m_InventoryPanelModel.GetJsonData("InventoryJsonData");

		for(int i = 0; i < tempList.Count ; i++){
			
			GameObject temp = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Item(), slotList[i].transform);
			temp.GetComponent<InventoryItemController>().InitItem(tempList[i].ItemId, tempList[i].ItemName, tempList[i].ItemNum, tempList[i].ItemBar);
		}	
	}

	public void AddItems(List<GameObject> itemList){
		int itemIndex = 0;
		for (int i = 0; i < slotList.Count; i++){
			if (slotList[i].transform.Find("InventoryItem") == null && itemIndex < itemList.Count){
				itemList[itemIndex].transform.SetParent(slotList[i].transform);
				itemList[itemIndex].GetComponent<InventoryItemController>().InInventory = true;
				itemIndex++;
			}
		}	
	}

    public void UIPanelShow(){
		//gameObject.SetActive(true);
		GetComponent<RectTransform>().offsetMin = new Vector2(0, 0);
    }

    public void UIPanelHide(){
        //gameObject.SetActive(false);
        GetComponent<RectTransform>().offsetMin = new Vector2(100000000000, 0);
    }

}
