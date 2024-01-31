using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelController : MonoBehaviour {
	private BuildPanelView m_BuildPanelView;
	
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

	private int zIndex = 20;

	private bool isItemCtr = true;
	
	private GameObject tempBuildModel = null;
	private GameObject BuildModel = null;

	private Ray ray;
	private RaycastHit Hit;
	private float lingmindu = 3.0f; 

	void Start () {
		m_BuildPanelView = gameObject.GetComponent<BuildPanelView>();

		
		CreateItems();
	}

	void Update(){
		MouseRight();
		ScrollMenu();
		MouseLeft();
		SetModelPosition();
	}

	private void MouseLeft(){
		if (Input.GetMouseButtonDown(0)){
			//最初はヌル
			if (targetItem == null) return;
			if (targetItem.materialList.Count == 0){
				SetLeftKeyNull();
				SetUIHide();
				return;
			}
			if (tempBuildModel == null) isItemCtr = false;
			if (tempBuildModel != null && isShow){
				SetUIHide();
			}

			if (BuildModel != null && BuildModel.GetComponent<MaterialModelBase>().IsCanPut == false) return;
			if (BuildModel != null && BuildModel.GetComponent<MaterialModelBase>().IsCanPut){
				BuildModel.GetComponent<MaterialModelBase>().Normal();
				Destroy(BuildModel.GetComponent<MaterialModelBase>());
			}
			if (tempBuildModel != null){
				BuildModel = Instantiate<GameObject>(tempBuildModel, m_BuildPanelView.M_Player_Transform.position + new Vector3(0, 0, 10), Quaternion.identity, m_BuildPanelView.M_Models_Parent);
				isItemCtr = true;
			}
		}
	}

	private void ScrollMenu(){
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
	}

	private void MouseRight(){
		if (Input.GetMouseButtonDown(1)){
			if (isItemCtr == false){
				currentMaterial.Normal();
				isItemCtr = true;

			}else{
				ShowOrHide();
			}
		}
	}

    private void CreateItems() {
		for (int i = 0; i < 9; i++){
			GameObject item = Instantiate<GameObject>(m_BuildPanelView.M_Item_Prefab, m_BuildPanelView.M_BG_Transform);
			//item.transform.rotation = Quaternion.Euler(new Vector3(0, 0, i * 40));
			//item.transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
			itemList.Add(item.GetComponent<Item>());
		
			//item.GetComponent<Item>().Icons = materialIcons[i];
			if (m_BuildPanelView.Icons[i] == null){
				//item.transform.Find("Icon").GetComponent<Image>().enabled = false;
				item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), false, null, true);
				Debug.Log("itemList" + itemList);

			}else{
				//item.transform.Find("Icon").GetComponent<Image>().sprite = icons[i];
				//item.GetComponent<Image>().enabled = false;

				item.GetComponent<Item>().Init("Item", Quaternion.Euler(new Vector3(0, 0, i * 40)), true, m_BuildPanelView.Icons[i], false);
				Debug.Log("itemList2" + itemList);
				//materialIcons[i].Length == 3
				for (int j = 0; j < m_BuildPanelView.MaterialIcons[i].Length; j++){
					zIndex += 13;
					if(m_BuildPanelView.MaterialIcons[i][j] != null){
						GameObject material = Instantiate<GameObject>(m_BuildPanelView.M_Material_Prefab, m_BuildPanelView.M_BG_Transform);
						material.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zIndex));
						material.transform.Find("Icon").GetComponent<Image>().sprite = m_BuildPanelView.MaterialIcons[i][j];
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
			tempBuildModel = m_BuildPanelView.MaterialModels[index % itemList.Count][index_Material % targetItem.materialList.Count];
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
			m_BuildPanelView.M_BG_Transform.gameObject.SetActive(false);
			isShow = false;

		}else{
			m_BuildPanelView.M_BG_Transform.gameObject.SetActive(true);
			isShow = true;
			if(tempBuildModel != null) tempBuildModel = null;
			if (targetMaterial != null) targetMaterial.Normal();
		}
    }

	//アイテム名
	private void SetTextValue(){
		m_BuildPanelView.M_ItemName_Text.text = itemNames[index % itemNames.Length];

	}

	private void SetTextValueMaterial(){
		m_BuildPanelView.M_ItemName_Text.text = m_BuildPanelView.MaterialIconNames[index % itemList.Count][index_Material % targetItem.materialList.Count];

	}

	

	private void SetModelPosition(){
		if (BuildModel == null) return;
		Debug.Log("SetModelPosition");
		ray = m_BuildPanelView.WorldCamera.ScreenPointToRay(Input.mousePosition);
		float maxDistance = 12;
		if(Physics.Raycast(ray, out Hit, maxDistance, ~(1 << 11))){
			Debug.Log("SetModelPosition!!!!!");
			if (BuildModel.GetComponent<MaterialModelBase>().IsAttach == false){
				if (BuildModel != null) BuildModel.transform.position = Hit.point;
			}

			if(Vector3.Distance(Hit.point, BuildModel.transform.position) > lingmindu){
				BuildModel.GetComponent<MaterialModelBase>().IsAttach = false;
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
		m_BuildPanelView.M_BG_Transform.gameObject.SetActive(false);
		isShow = false;
	}
}
