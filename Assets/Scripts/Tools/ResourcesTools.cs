using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ResourcesTools {
	static public Dictionary<string, Sprite> LoadFolderAssets(string folderName, Dictionary<string, Sprite> dic){
		Sprite[] tempSprite = Resources.LoadAll<Sprite>(folderName);
		for (int i = 0; i < tempSprite.Length; i++){
			dic.Add(tempSprite[i].name, tempSprite[i]);

		}
		return dic;
	}

	static public Sprite GetAsset(string fileName, Dictionary<string, Sprite> dic){
		Sprite temp = null;
		dic.TryGetValue(fileName, out temp);
		return temp;

	}

}
