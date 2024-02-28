using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class InventoryPanelModel : MonoBehaviour
{

	void Awake()
	{

	}


	public List<InventoryItem> GetJsonData(string fileName){
		return JsonTools.LoadJsonFileByIO<InventoryItem>(fileName);
	}

	public void ObjectToJson(List<GameObject> list, string fileName)
    {
        List<InventoryItem> tempList = new List<InventoryItem>();
        for (int i = 0; i < list.Count; i++)
        {
            Transform tempTransform = list[i].GetComponent<Transform>();
            InventoryItem item = null;
            if(tempTransform.childCount != 0) 
            {
                InventoryItemController iic = tempTransform.Find("InventoryItem").GetComponent<InventoryItemController>();
                item = new InventoryItem(iic.Id, iic.GetImageName(), iic.Num, iic.IsHaveBar());

            }else if(tempTransform.childCount == 0)
            {
                item = new InventoryItem(0,"",0, false);
            }
            tempList.Add(item);
        }
        string str = JsonMapper.ToJson(tempList);
       
        File.Delete(Application.dataPath + @"\Resources\JsonData\" + fileName);
        StreamWriter sw = new StreamWriter(Application.dataPath + @"\Resources\JsonData\" + fileName);
        sw.Write(str);
        sw.Close();
    }

}
