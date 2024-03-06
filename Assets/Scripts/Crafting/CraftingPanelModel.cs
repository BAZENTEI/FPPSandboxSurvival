using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

/// <summary>
/// Craftingpanel model.
/// </summary>
public class CraftingPanelModel : MonoBehaviour {

	private Dictionary<int, CraftingMapItem> mapItemDic = null;
	void Awake() {
		mapItemDic = LoadMapContents("CraftingMapJsonData");
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

	private Dictionary<int, CraftingMapItem> LoadMapContents(string fileName){
		Dictionary<int, CraftingMapItem> temp = new Dictionary<int, CraftingMapItem>();
		string jsonString = Resources.Load<TextAsset>("JsonData/" + fileName).text;
		JsonData jsonData = JsonMapper.ToObject(jsonString);

		for (int i = 0; i < jsonData.Count; i++){
			int mapId = int.Parse(jsonData[i]["MapId"].ToString());
			string tempStr = jsonData[i]["MapContents"].ToString();
			string[] mapContents = tempStr.Split(',');
			string mapName = jsonData[i]["MapName"].ToString();
            int mapCount = int.Parse(jsonData[i]["MaterialsCount"].ToString());

            CraftingMapItem item = new CraftingMapItem(mapId ,mapContents, mapName, mapCount);
			temp.Add(mapId,item);
		}
		return temp;
	}

	public CraftingMapItem GetItemById(int id){
		CraftingMapItem temp = null;
		mapItemDic.TryGetValue(id, out temp);
		return temp;

	}

}
