﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelController : MonoBehaviour {
	private Transform BG_Transform;
	private GameObject item_Prefab;
	private GameObject material_Prefab;
	private Text itemName_Text;
	private List<Sprite> icons = new List<Sprite>();

	private bool isShow = true;
	private List<Item> itemList = new List<Item>();

	//メインメニュー
	private float scrollNum = 90000.0f;
	private int index = 0;
	private Item currentItem = null;
	private Item targetItem = null;

	//サブメニュー
	private float scrollNum_Material = 0.0f;
	private int index_Material = 0;
	private MaterialItem currentMaterial = null;
	private MaterialItem targetMaterial = null;

	private string[] itemNames = new string[] { "", "[1]", "[2]", "[3]", "[4]", "[5]", "[6]", "[7]", "[8]" };
	private List<Sprite[]> materialIcons = new List<Sprite[]>();

	private int zIndex = 20;
	private List<string[]> materialIconNames = new List<string[]>();
	private List<GameObject[]> materialModels = new List<GameObject[]>();

	private bool isItemCtr = true;
	private Transform player_Transform;
	private GameObject tempBuildModel = null;
	private GameObject BuildModel = null;

	private Camera WorldCamera = null;
	private Ray ray;
	private RaycastHit Hit;

	void Start () {
		Init();
		LoadIcons();
		LoadMaterialIcons();
		SetmaterialIconNames();
		LoadMaterialModels();
		CreateItems();
	}

	void Update(){
		if (Input.GetMouseButtonDown(1)){
            if (isItemCtr == false){
				currentMaterial.Normal();
				isItemCtr = true;

			}else{
				ShowOrHide();
			}
		}
		//メインメニュー
        if (isShow && isItemCtr){
			if (Input.GetAxis("Mouse ScrollWheel") != 0){
				MouseScrollWheel();
			}
		}
		//サブメニュー
		if (isShow && isItemCtr == false){
			if (Input.GetAxis("Mouse ScrollWheel") != 0){
				MouseScrollWheelMaterial();
			}
		}
		if (Input.GetMouseButtonDown(0)){
			//最初はヌル
			if (targetItem == null) return;
			if (targetItem.materialList.Count == 0){
				SetLeftKeyNull();
				SetUIHide();
				return;
            }
			if (tempBuildModel == null) isItemCtr = false;
			if(tempBuildModel != null && isShow){
				SetUIHide();
			}

			if (BuildModel != null && BuildModel.GetComponent<Platform>().IsCanPut == false) return;
			if (BuildModel != null && BuildModel.GetComponent<Platform>().IsCanPut){
				BuildModel.GetComponent<Platform>().Normal();
				Destroy(BuildModel.GetComponent<Platform>());
			}
			if (tempBuildModel != null){
				BuildModel = Instantiate<GameObject>(tempBuildModel, player_Transform.position + new Vector3(0, 0, 10), Quaternion.identity);
				isItemCtr = true;
			}
		}
		SetModelPosition();
	}

	private void Init() {
		BG_Transform = transform.Find("WheelBG");
		item_Prefab = Resources.Load<GameObject>("Build/Prefab/Item");
		itemName_Text = transform.Find("WheelBG/ItemName").GetComponent<Text>();
		material_Prefab = Resources.Load<GameObject>("Build/Prefab/MaterialBG");
		player_Transform = GameObject.Find("FPPController").transform;
		WorldCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();
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

	private void LoadMaterialIcons(){
		materialIcons.Add(null);
		materialIcons.Add(new Sprite[]{ LoadIcon("Ceiling Light"), LoadIcon("Pillar_Wood"), LoadIcon("Wooden Ladder")});
		materialIcons.Add(new Sprite[] { null, LoadIcon("Roof_Metal"), null });
		materialIcons.Add(new Sprite[] { LoadIcon("Stairs_Wood"), LoadIcon("L Shaped Stairs_Wood"), null });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Window_Wood"), null });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Wooden Door"), null });
		materialIcons.Add(new Sprite[] { LoadIcon("Wall_Wood"), LoadIcon("Doorway_Wood"), LoadIcon("Window Frame_Wood") });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Floor_Wood"), null });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Platform_Wood"), null });
	}

	private void SetmaterialIconNames(){
		materialIconNames.Add(null);
		materialIconNames.Add(new string[] { "Ceiling Light", "Wood Pillar", "Ladder"});
		materialIconNames.Add(new string[] {  "Wood Pillar"});
		materialIconNames.Add(new string[] { "Ceiling Light2", "Wood Pillar"});
		materialIconNames.Add(new string[] { "Wood Pillar"});
		materialIconNames.Add(new string[] { "Wood Pillar"});
		materialIconNames.Add(new string[] { "Ceiling Light5", "Wood Pillar", "Ladder"});
		materialIconNames.Add(new string[] { "Wood Pillar"});
		materialIconNames.Add(new string[] { "Wood Pillar"});
	}

	private void LoadMaterialModels(){
		materialModels.Add(null);
		materialModels.Add(new GameObject[] { LoadBuildModel("Ceiling_Light") , LoadBuildModel("Pillar"), LoadBuildModel("Ladder")});
		materialModels.Add(new GameObject[] { LoadBuildModel("Roof")});
		Debug.Log("materialModels!!!!!!!!!!!!" + materialModels[1][1].name);
		materialModels.Add(new GameObject[] { LoadBuildModel("Stairs"), LoadBuildModel("L_Shaped_Stairs")});
		materialModels.Add(new GameObject[] { LoadBuildModel("Window")});
		materialModels.Add(new GameObject[] { LoadBuildModel("Door")});
		materialModels.Add(new GameObject[] { LoadBuildModel("Wall"), LoadBuildModel("Doorway"), LoadBuildModel("Window_Frame")});
		materialModels.Add(new GameObject[] { LoadBuildModel("Floor") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Platform") });

	}


	private void CreateItems() {
		for (int i = 0; i < 9; i++){
			GameObject item = Instantiate<GameObject>(item_Prefab, BG_Transform);
			//item.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * 40));
			//item.transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
			itemList.Add(item.GetComponent<Item>());
		
			//item.GetComponent<Item>().Icons = materialIcons[i];
			if (icons[i] == null){
				//item.transform.Find("Icon").GetComponent<Image>().enabled = false;
				item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), false, null, true);
				Debug.Log("itemList" + itemList);

			}else{
				//item.transform.Find("Icon").GetComponent<Image>().sprite = icons[i];
				//item.GetComponent<Image>().enabled = false;

				item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), true, icons[i], false);
				Debug.Log("itemList2" + itemList);
				//materialIcons[i].Length == 3
				for (int j = 0; j < materialIcons[i].Length; j++){
					zIndex += 13;
					if(materialIcons[i][j] != null)
                    {
						GameObject material = Instantiate<GameObject>(material_Prefab, BG_Transform);
						material.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zIndex));
						material.transform.Find("Icon").GetComponent<Image>().sprite = materialIcons[i][j];
						material.transform.Find("Icon").transform.rotation = Quaternion.Euler(Vector3.zero);
						material.transform.SetParent(item.transform);
						item.GetComponent<Item>().MaterialListAdd(material);
					}
				}
				item.GetComponent<Item>().Hide();
			}
		}

		currentItem = itemList[0];
		SetTextValue();
	}

	private void MouseScrollWheel(){
		//Debug.Log("GetAxis !!!!!");
		//Debug.Log("GetAxis" + Input.GetAxis("Mouse ScrollWheel"));
		//5倍
		scrollNum += Input.GetAxis("Mouse ScrollWheel") * 5;
		index = Mathf.Abs((int)scrollNum);
		targetItem = itemList[index % itemList.Count];
		if (targetItem != currentItem){
			currentItem.Hide();
			targetItem.Show();
			currentItem = targetItem;
			SetTextValue();
		}
	}

	private void MouseScrollWheelMaterial(){
		scrollNum_Material += Input.GetAxis("Mouse ScrollWheel") * 5;
		index_Material = Mathf.Abs((int)scrollNum_Material);
		targetItem = itemList[index % itemList.Count];
		Debug.Log(targetItem.materialList.Count);
		Debug.Log(currentMaterial);
		targetMaterial = targetItem.materialList[index_Material % targetItem.materialList.Count].GetComponent<MaterialItem>();

		if (targetMaterial != currentMaterial){
			tempBuildModel = materialModels[index % itemList.Count][index_Material % targetItem.materialList.Count];
			targetMaterial.Highlight();
			if (currentMaterial != null){
				currentMaterial.Normal();
			}
			currentMaterial = targetMaterial;
			SetTextValueMaterial();
			//SetTextValue();
		}
	}

	private void ShowOrHide(){
        if (isShow){
			//GameObject.Find("WheelBG").SetActive(false) ;
			BG_Transform.gameObject.SetActive(false);
			isShow = false;

		}else{
			BG_Transform.gameObject.SetActive(true);
			isShow = true;
			if(tempBuildModel != null) tempBuildModel = null;
			if (targetMaterial != null) targetMaterial.Normal();
		}
    }

	//アイテム名
	private void SetTextValue(){
		itemName_Text.text = itemNames[index % itemNames.Length];

	}

	private void SetTextValueMaterial(){
		itemName_Text.text = materialIconNames[index % itemList.Count][index_Material % targetItem.materialList.Count];

	}

	private Sprite LoadIcon(string name){
		return Resources.Load<Sprite>("Build/MaterialIcon/" + name);
    }

	private GameObject LoadBuildModel(string name){
		return Resources.Load<GameObject>("Build/Prefab/Material/" + name);
    }

	private void SetModelPosition(){
		Debug.Log("SetModelPosition");
		ray = WorldCamera.ScreenPointToRay(Input.mousePosition);
		float maxDistance = 12;
		if(Physics.Raycast(ray, out Hit, maxDistance, ~(1 << 11))){
			Debug.Log("SetModelPosition!!!!!");
			if (BuildModel.GetComponent<Platform>().IsAttach == false){
				if (BuildModel != null) BuildModel.transform.position = Hit.point;
			}

			if(Vector3.Distance(Hit.point, BuildModel.transform.position) > 1){
				BuildModel.GetComponent<Platform>().IsAttach = false;
            }
		}
    }


	private void SetLeftKeyNull(){
		if (tempBuildModel != null) tempBuildModel = null;
		if(BuildModel != null){
			Destroy(BuildModel);
			BuildModel = null;
		}
	}

	private void SetUIHide(){
		BG_Transform.gameObject.SetActive(false);
		isShow = false;
	}
}