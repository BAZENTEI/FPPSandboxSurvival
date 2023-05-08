using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crafting 
/// controller.
/// </summary>
public class CraftingPanelController : MonoBehaviour {
	private CraftingPanelView m_CraftingPanelView;
	private CraftingPanelModel m_CraftingPanelModel;

	private int tabsNum = 2;
	private List<GameObject> tabsList;
	private List<GameObject> contentsList;
	// Use this for initialization
	void Start () {
		m_CraftingPanelView = gameObject.GetComponent<CraftingPanelView>();
		m_CraftingPanelModel = gameObject.GetComponent<CraftingPanelModel>();
		tabsList = new List<GameObject>();
		contentsList = new List<GameObject>();

		CreateAlltabs();
		CreateAllContents();

		ResetTabsAndContents(0);
	}
	
	private void CreateAlltabs(){
		for(int i = 0; i < tabsNum; i++){
			GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_TabsItem, m_CraftingPanelView.Tabs_Transform);
			Sprite temp = m_CraftingPanelView.ByNameGetSprite(m_CraftingPanelModel.GetTabsIconName()[i]);
			go.GetComponent<CraftingTabItemController>().InitItem(i, temp);
			tabsList.Add(go);
		}

	}

	private void CreateAllContents(){
		List<List<string>> tempList = m_CraftingPanelModel.ByNameGetJsonData("CraftingContentsJsonData");
        for (int i = 0; i < tabsNum; i++){
			GameObject go = GameObject.Instantiate<GameObject>(m_CraftingPanelView.Prefab_Content, m_CraftingPanelView.Contents_Transform);
			go.GetComponent<CraftingContentController>().InitContent(i, m_CraftingPanelView.Prefab_ContentItem, tempList[i]);
			contentsList.Add(go);

		}
	}

	private void ResetTabsAndContents(int index) {
		for(int i = 0; i < tabsList.Count ; i++){
			tabsList[i].GetComponent<CraftingTabItemController>().NormalTab();
			contentsList[i].SetActive(false);
		}

		Debug.Log("ResetTabsAndContents()" + index);
		tabsList[index].GetComponent<CraftingTabItemController>().ActiveTab();
		contentsList[index].SetActive(true);

	}



}
