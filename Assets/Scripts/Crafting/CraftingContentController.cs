using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingContentController : MonoBehaviour {

	private Transform m_Transform;
	private int index = -1;
	private CraftingContentItemController current = null;

	// Use this for initialization
	//start->awake
	void Awake () {
		m_Transform = gameObject.transform;
	}
	
	// Update is called once per frame
	public void InitContent (int index, GameObject prefab, List<CraftingContentItem> strList) {
		this.index = index;
		gameObject.name = "Content" + index;
		CreateAllItems(prefab, strList);
	}

	private void CreateAllItems(GameObject prefab, List<CraftingContentItem> strList){
        for (int i = 0; i < strList.Count; i++){
			GameObject go = GameObject.Instantiate<GameObject>(prefab, m_Transform);

			go.GetComponent<CraftingContentItemController>().Init(strList[i]);

		}
    }

	private void ResetItemState(CraftingContentItemController item){
		if (item == current) return;
		Debug.Log(item.name);
		if(current != null){
			current.NormalItem();
        }
		item.ActiveItem();
		current = item;
	}

}
