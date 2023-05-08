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
	 

	private GameObject prefab_TabsItem;
	private GameObject prefab_Content;
	private GameObject prefab_ContentItem;

	private Dictionary<string, Sprite> tabIconDic;
	public Transform M_Transform{ get { return m_Transform; } }
	public Transform Tabs_Transform { get { return tabs_Transform; } }
	public Transform Contents_Transform{ get { return contents_Transform; } }
	public GameObject Prefab_TabsItem{ get { return prefab_TabsItem; } }
	public GameObject Prefab_Content { get { return prefab_Content; } }
	public GameObject Prefab_ContentItem { get { return prefab_ContentItem; } }

	// Use this for initialization
	void Awake () {
		m_Transform = gameObject.transform;
		tabs_Transform = m_Transform.Find ("Left/Tabs").transform;
		contents_Transform = m_Transform.Find ("Left/Contents").transform;

		prefab_TabsItem = Resources.Load<GameObject>("CraftingTabsItem");
		prefab_Content = Resources.Load<GameObject>("CraftingContent");
		prefab_ContentItem = Resources.Load<GameObject>("CraftingContentItem");

		tabIconDic = new Dictionary<string, Sprite>();

		TabsIconLoad();
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


}
