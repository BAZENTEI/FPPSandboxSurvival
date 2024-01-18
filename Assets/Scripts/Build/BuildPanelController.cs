using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelController : MonoBehaviour {
	private Transform BG_Transform;
	private GameObject item_Prefab;
	private Text itemName_Text;
	private List<Sprite> icons = new List<Sprite>();

	private bool isShow = true;
	private List<Item> itemList = new List<Item>();

	private float scrollNum = 90000.0f;
	private int index = 0;
	private Item currentItem = null;
	private Item targetItem = null;

	private string[] itemNames = new string[] { "", "1", "2", "3", "4", "5", "6", "7", "8" };
	void Start () {
		Debug.Log("!!!!!!!!!!!!!!!!!!!");
		Init();
		LoadIcons();
		CreateItems();
	}

	void Update(){
		if (Input.GetMouseButtonDown(1)){
			ShowOrHide();
		}
        if (isShow){
			if (Input.GetAxis("Mouse ScrollWheel") != 0){
				Debug.Log("GetAxis !!!!!");
				Debug.Log("GetAxis" + Input.GetAxis("Mouse ScrollWheel"));
				//5倍
				scrollNum += Input.GetAxis("Mouse ScrollWheel") * 5;
				index = Mathf.Abs((int)scrollNum);
				Debug.Log("index:" + index);
				targetItem = itemList[index % itemList.Count];
				if(targetItem != currentItem){
					currentItem.Hide();
					targetItem.Show();
					currentItem = targetItem;
					SetTextValue();
				}
			}
		}
	}

	private void Init() {
		BG_Transform = transform.Find("WheelBG");
		item_Prefab = Resources.Load<GameObject>("Build/Prefab/Item");
		itemName_Text = transform.Find("WheelBG/ItemName").GetComponent<Text>();
	}

	private void LoadIcons() {
		//icons = Resources.LoadAll<Sprite>("Build/Icon");
		icons.Add(null);
		icons.Add(Resources.Load<Sprite>("Build/Icon/Question Mark"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Roof_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Stairs_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Window_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Door_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Wall_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Floor_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Foundation_Category"));
	}

	private void CreateItems() {
		Debug.Log("CreateItems");
		for (int i = 0; i < 9; i++){
			GameObject item = Instantiate<GameObject>(item_Prefab, BG_Transform);
			//item.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * 40));
			//item.transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);

			item.transform.Find("Icon").GetComponent<Image>().sprite = icons[i];

			if (icons[i] == null){
				//item.transform.Find("Icon").GetComponent<Image>().enabled = false;
				item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), false, null, true);
				itemList.Add(item.GetComponent<Item>());
				Debug.Log("itemList");
				Debug.Log(itemList);

			}else{
				//item.transform.Find("Icon").GetComponent<Image>().sprite = icons[i];
				//item.GetComponent<Image>().enabled = false;

				item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), true, icons[i], false);
				itemList.Add(item.GetComponent<Item>());
				Debug.Log("itemList2");
				Debug.Log(itemList);

			}
		}

		currentItem = itemList[0];
		SetTextValue();
	}

	private void ShowOrHide(){
        if (isShow){
			GameObject.Find("WheelBG").SetActive(true);
			//BG_Transform.gameObject.SetActive(true);
			isShow = false;

		}else{
			GameObject.Find("WheelBG").SetActive(false);
			//BG_Transform.gameObject.SetActive(false);
			isShow = true;
		}
    }
	

	private void SetTextValue(){
		itemName_Text.text = itemNames[index % itemNames.Length];

	}
}
