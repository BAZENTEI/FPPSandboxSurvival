using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearBarView : MonoBehaviour {

	private Transform m_Transform;
	private Transform grid_Transform;

	private GameObject prefab_GearBarSlot;
	public GameObject Prefab_GearBarSlot { get { return prefab_GearBarSlot; } }
	public Transform M_Transform { get { return m_Transform; } }
	public Transform Grid_Transform { get { return grid_Transform; } }


	void Awake () {
		prefab_GearBarSlot = Resources.Load<GameObject>("GearBarSlot");
		m_Transform = gameObject.transform;
		grid_Transform = gameObject.transform.Find("Grid").transform;
	}
	
	
	void Update () {
		
	}
}
