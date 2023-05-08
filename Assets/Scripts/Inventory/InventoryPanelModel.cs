using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class InventoryPanelModel : MonoBehaviour {
	
	void Awake(){
		
	}
	

	public List<InventoryItem> GetJsonData(string fileName){
		List<InventoryItem> tempList = new List<InventoryItem>();
		//TextAsset tempTextAsset = Resources.Load<TextAsset>("JsonData" + fileName);
		//string tempJsonStr = tempTextAsset.text;
		string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;
		Debug.Log(tempJsonStr);
		//dump
		JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
		for(int i = 0;i < jsonData.Count; i++){
			InventoryItem temp = JsonMapper.ToObject<InventoryItem>(jsonData[i].ToJson());
			
			tempList.Add(temp);
			//tempList.ItemName = jsonData[i].;
			//tempList.ItemNum =  jsonData[i].ItemNum;
		}

		return tempList;
	}
	
}
