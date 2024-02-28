using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

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

    public static List<T> LoadJsonFileByIO<T>(string fileName){
        List<T> tempList = new List<T>();
        string tempJsonStr = File.ReadAllText(Application.dataPath + @"\Resources\JsonData\" + fileName);

        JsonData jsonData = JsonMapper.ToObject(tempJsonStr);
        for (int i = 0; i < jsonData.Count; i++){
            T ii = JsonMapper.ToObject<T>(jsonData[i].ToJson());
            tempList.Add(ii);
        }

        return tempList;
    }
}
