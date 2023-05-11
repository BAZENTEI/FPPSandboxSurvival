using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingContentItemController : MonoBehaviour {

	private Transform m_Transform;
	private Text m_Text;
	private Button m_Button;
	private GameObject m_BG;

	private int id;
	private string m_name;

	void Awake () {
		m_Transform = gameObject.transform;
		m_Text = m_Transform.Find("Text").GetComponent<Text>();
		m_Button = gameObject.GetComponent<Button>();
		m_BG = m_Transform.Find("Background").gameObject;

		m_BG.SetActive(false);
		m_Button.onClick.AddListener(ItemButtonClick);
	}

	public void Init(CraftingContentItem item){

		this.id = item.ItemID;
		this.m_name = item.ItemName;
		gameObject.name = "Item" + id;
		m_Text.text = " " + m_name;

	}

	public void NormalItem(){
		m_BG.SetActive(false);
    }

	public void ActiveItem(){
		m_BG.SetActive(true);
	}

	private void ItemButtonClick(){
		
		SendMessageUpwards("ResetItemState", this);
    }


}
