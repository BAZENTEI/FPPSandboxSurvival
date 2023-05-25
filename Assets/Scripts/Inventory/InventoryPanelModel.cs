using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryPanelModel : MonoBehaviour
{

	void Awake()
	{

	}


	public List<InventoryItem> GetJsonData(string fileName){
		return JsonTools.LoadJson<InventoryItem>(fileName);
	}

}
