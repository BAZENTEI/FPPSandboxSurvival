using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelController : MonoBehaviour {
	private InventoryPanelView m_InventoryPanelView;
	private InventoryPanelModel m_InventoryPanelModel;
	private int slotNum = 27;
	private List<GameObject> slotList = new List<GameObject>();

	
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
			slotList.Add(tempSlot);
		}
	}

	//create item 
	private void CreateItemAll(){
		List<InventoryItem> tempList = new List<InventoryItem>();
		tempList = m_InventoryPanelModel.GetJsonData("InventoryJsonData");

		for(int i = 0; i < tempList.Count ; i++){
			
			GameObject temp = GameObject.Instantiate<GameObject>(m_InventoryPanelView.Prefab_Item(), slotList[i].transform);
			temp.GetComponent<InventoryItemController>().InitItem(tempList[i].ItemName, tempList[i].ItemNum);
		}
		

	}
}
