using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public sealed class JsonTools {
	static public List<T> LoadJson<T>(string fileName){
		List<T> tempList = new List<T>();
		//TextAsset tempTextAsset = Resources.Load<TextAsset>("JsonData" + fileName);
		//string tempJsonStr = tempTextAsset.text;
		string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;
		Debug.Log(tempJsonStr);
		//dump
		JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
		for (int i = 0; i < jsonData.Count; i++){
			T temp = JsonMapper.ToObject<T>(jsonData[i].ToJson());

			tempList.Add(temp);
		}

		return tempList;
	}

    public static List<T> LoadJsonFile<T>(string fileName)
    {
        List<T> tempList = new List<T>();
        string tempJsonStr = Resources.Load<TextAsset>("JsonData/" + fileName).text;

        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++)
        {
            T ii = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(ii);
        }

        return tempList;
    }
}
