using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanelView : MonoBehaviour {

    private Transform m_Transform;
	private Transform grid_transform;
	private GameObject prefab_slot;
	private GameObject prefab_item;

// Use this for initialization
	void Awake () {
		Debug.Log("1");
		m_Transform = gameObject.transform;
		grid_transform = m_Transform.Find("Background/Grid").transform;
		prefab_slot = Resources.Load<GameObject>("InventorySlot");
		prefab_item = Resources.Load<GameObject>("InventoryItem");
	}
	

	public Transform GetTransform(){
		return m_Transform;
	}

	public Transform GetGridTransform(){
		return grid_transform;
	}

	public GameObject Prefab_Slot(){
		return prefab_slot;
	}

	public GameObject Prefab_Item(){
		return prefab_item;
	}



	
}
