using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Crafting panel view.
/// </summary>
public class CraftingPanelView : MonoBehaviour {
	private Transform m_Transform;
	private Transform tabs_Transform;
	private Transform contents_Transform;
	private Transform center_Transform;

	private GameObject prefab_TabsItem;
	private GameObject prefab_Content;
	private GameObject prefab_ContentItem;
	private GameObject prefab_Slot;

	private Dictionary<string, Sprite> tabIconDic;
	private Dictionary<string,Sprite> materialIconDic;

	public Transform M_Transform { get { return m_Transform; } }
	public Transform Tabs_Transform { get { return tabs_Transform; } }
	public Transform Contents_Transform { get { return contents_Transform; } }
	public Transform Center_Transform { get { return center_Transform; } }

	public GameObject Prefab_TabsItem{ get { return prefab_TabsItem; } }
	public GameObject Prefab_Content { get { return prefab_Content; } }
	public GameObject Prefab_ContentItem { get { return prefab_ContentItem; } }
	public GameObject Prefab_Slot { get { return prefab_Slot; } }

	// Use this for initialization
	void Awake () {
		m_Transform = gameObject.transform;
		tabs_Transform = m_Transform.Find ("Left/Tabs").transform;
		contents_Transform = m_Transform.Find ("Left/Contents").transform;
		center_Transform = m_Transform.Find("Middle").transform;

		prefab_TabsItem = Resources.Load<GameObject>("CraftingTabsItem");
		prefab_Content = Resources.Load<GameObject>("CraftingContent");
		prefab_ContentItem = Resources.Load<GameObject>("CraftingContentItem");
		prefab_Slot = Resources.Load<GameObject>("CraftingSlot");

		tabIconDic = new Dictionary<string, Sprite>();
		materialIconDic = new Dictionary<string, Sprite>();

		TabsIconLoad();
		MaterialIconLoad();
	}
	
	private void TabsIconLoad(){
		Sprite []tempSprite = Resources.LoadAll<Sprite>("TabIcon");
        for (int i = 0; i < tempSprite.Length ; i++){
			tabIconDic.Add(tempSprite[i].name, tempSprite[i]);

		}
    }

	public Sprite ByNameGetSprite(string name){
		Sprite temp = null;
		tabIconDic.TryGetValue(name,out temp);
		return temp;

	}

	private void MaterialIconLoad(){
		Sprite[] tempSprite = Resources.LoadAll<Sprite>("Material");
		for (int i = 0; i < tempSprite.Length; i++){
			materialIconDic.Add(tempSprite[i].name, tempSprite[i]);
		}
	}

	public Sprite ByNameGetMaterialIconSprite(string name){
		Sprite temp = null;
		materialIconDic.TryGetValue(name,out temp);
		return temp;
	}


}
