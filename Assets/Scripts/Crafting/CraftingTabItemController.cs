using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// CraftingTabItemController
/// </summary>
public class CraftingTabItemController : MonoBehaviour {
	private Transform m_Transform;
	private Button m_Button;
	private GameObject m_ButtonBG;
	private Image m_Icon;

	private int index = -1;

	// Use this for initialization
	void Awake () {
		m_Transform = gameObject.transform;
		m_Button = gameObject.GetComponent<Button>();
		m_ButtonBG = m_Transform.Find("Background").gameObject;
		m_Icon = m_Transform.Find("Icon").GetComponent<Image>();
		m_Button.onClick.AddListener(ButtonOnClick);

	}
	
	public void InitItem(int index,Sprite icon){
		this.index = index;
		gameObject.name = "Tab" + index;
		m_Icon.sprite = icon;
			//Resources.Load<Sprite>("TabIcon/" + name);

	}

	public void NormalTab(){
		if(m_ButtonBG.activeSelf == false){
			m_ButtonBG.SetActive(true);
		}
    }

	public void ActiveTab(){
		m_ButtonBG.SetActive(false);
	}

	private void ButtonOnClick(){
		Debug.Log("ccc");
		SendMessageUpwards("ResetTabsAndContents", index);
	}


}
