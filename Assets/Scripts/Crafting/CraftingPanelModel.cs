using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// Craftingpanel model.
/// </summary>
public class CraftingPanelModel : MonoBehaviour {

	// Use this for initialization
	void Awake() {

	}

	public string[] GetTabsIconName(){

		return new string[] { "Icon_House", "Icon_Weapon" };

	}

	public List<List<CraftingContentItem>> ByNameGetJsonData(string fileName){
		List<List<CraftingContentItem>> temp = new List<List<CraftingContentItem>>();
		string jsonString = Resources.Load<TextAsset>("JsonData/" + fileName).text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);
        for (int i = 0; i < jsonData.Count; i++){
			List<CraftingContentItem> tempList = new List<CraftingContentItem>();
			JsonData jd = jsonData[i]["Type"];
            for (int j = 0; j < jd.Count; j++){
				tempList.Add(JsonMapper.ToObject<CraftingContentItem>(jd[j].ToJson()));
				//tempList.Add(jd[j]["ItemName"].ToString());
			}
			temp.Add(tempList);
        }

		return temp;
    }
}
