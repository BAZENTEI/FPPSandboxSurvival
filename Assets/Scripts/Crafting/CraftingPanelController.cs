using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crafting 
/// controller.
/// </summary>
public class CraftingPanelController : MonoBehaviour {
    public static CraftingPanelController Instance;

    private CraftingPanelView m_CraftingPanelView;
	private CraftingPanelModel m_CraftingPanelModel;
	private CraftingController m_CraftingController;

	private int tabsNum = 2;
	private List<GameObject> tabsList;
	private List<GameObject> contentsList;

	private int slotsNum = 25;
	private List<GameObject> slotsList;

	private int currentIndex = -1;

	private Transform m_Transform;
    private int materialsCount = 0;         
    private int dargMaterialsCount = 0;     
    private List<GameObject> materialsList;

    private void Awake(){
        Instance = this;
    }

    void Start() {
		Init();

		CreateAlltabs();
		CreateAllContents();
		//最初のtab
		ResetTabsAndContents(1);
		CreateAllSlots();

	}

	private void Init() {
		m_Transform = gameObject.transform;
		m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();
		m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();
		m_CraftingController = m_Transform.Find("Right").GetComponent<CraftingController>();

		tabsList = new List<GameObject>();
		contentsList = new List<GameObject>();
		slotsList = new List<GameObject>();
        materialsList = new List<GameObject>();

        m_CraftingController.Prefab_InventoryItem = m_CraftingPanelView.Prefab_InventoryItem;
    }

	private void CreateAlltabs() {
		for (int i = 0; i < tabsNum; i++) {
			GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_TabsItem, m_CraftingPanelView.Tabs_Transform);
			Sprite temp = m_CraftingPanelView.ByNameGetSprite(m_CraftingPanelModel.GetTabsIconName()[i]);
			go.GetComponent<CraftingTabItemController>().InitItem(i, temp);
			tabsList.Add(go);
		}

	}

	private void CreateAllContents() {

		List<List<CraftingContentItem>> tempList = m_CraftingPanelModel.ByNameGetJsonData("CraftingContentsJsonData");
		for (int i = 0; i < tabsNum; i++) {
			GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Content, m_CraftingPanelView.Contents_Transform);
			go.GetComponent<CraftingContentController>().InitContent(i, m_CraftingPanelView.Prefab_ContentItem, tempList[i]);
			contentsList.Add(go);

		}
	}

	private void ResetTabsAndContents(int index) {
		if (currentIndex == index) return;
		for (int i = 0; i < tabsList.Count; i++) {
			tabsList[i].GetComponent<CraftingTabItemController>().NormalTab();
			contentsList[i].SetActive(false);
		}

		Debug.Log("ResetTabsAndContents():" + index);
		tabsList[index].GetComponent<CraftingTabItemController>().ActiveTab();
		contentsList[index].SetActive(true);
		currentIndex = index;
	}



	private void CreateAllSlots() {
		for (int i = 0; i < slotsNum; i++) {
			GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Slot, m_CraftingPanelView.Center_Transform);
			go.name = "Slot" + i;
			slotsList.Add(go);
		}
	}

	private void CreateSlotContents(int id) {
		CraftingMapItem temp = m_CraftingPanelModel.GetItemById(id);
		if (temp != null) {
			//
			ResetSlotContents();
			//
			ResetMaterials();

			for (int j = 0; j < temp.MapContents.Length; j++) {
				if (temp.MapContents[j] != "0") {
					
					Sprite sp = m_CraftingPanelView.ByNameGetMaterialIconSprite(temp.MapContents[j]);
					slotsList[j].GetComponent<CraftingSlotController>().Init(sp, int.Parse(temp.MapContents[j]));
				}
              
                //クラフトアイテムの表示
                m_CraftingController.Init(temp.MapId, temp.MapName);
                //Debug.Log(tempList[i].ToString());
                materialsCount = temp.MaterialsCount;
            }
		}

	}

	private void ResetSlotContents() {
		for (int i = 0; i < slotsList.Count; i++) {
			slotsList[i].GetComponent<CraftingSlotController>().Reset();

		}
	}

	/// <summary>
	/// 
	/// </summary>
	private void ResetMaterials(){
		List<GameObject> materialsList = new List<GameObject>();
		for(int i = 0;i < slotsList.Count; i++){
			if(slotsList[i].transform.Find("InventoryItem") != null){
                Transform tempTransform = slotsList[i].transform.Find("InventoryItem");

                //materialsList.Add(slotsList[i].transform.Find("InventoryItem").gameObject);
				if(tempTransform != null)
				{
					materialsList.Add(tempTransform.gameObject);
				}
            }
        }
		InventoryPanelController.Instance.AddItems(materialsList);
	}

    public void DargMaterilasItem(GameObject item){
        materialsList.Add(item);
        dargMaterialsCount++;
		Debug.Log("dargMaterialsCount"+ dargMaterialsCount + "materialsCount" + materialsCount);
        
        if (materialsCount == dargMaterialsCount){
            m_CraftingController.ActiveButton();
        }
    }

    private void CraftingOK(){
        for (int i = 0; i < materialsList.Count; i++){
            InventoryItemController iic = materialsList[i].GetComponent<InventoryItemController>();

            if (iic.Num == 1){
                Destroy(materialsList[i]);
            }else{
                iic.Num = iic.Num - 1;
            }
        }
        StartCoroutine("ResetMap");
    }

    private IEnumerator ResetMap(){
        yield return new WaitForSeconds(2);
        ResetMaterials();
        dargMaterialsCount = 0;
        materialsList.Clear();
    }
}
