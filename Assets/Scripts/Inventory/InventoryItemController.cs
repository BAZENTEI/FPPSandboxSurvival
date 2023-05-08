using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour {
	private Transform m_Transform;
	private Image m_Image;
	private Text m_Text;

	void Awake(){
		m_Transform = gameObject.transform;
		m_Image = gameObject.GetComponent<Image>();
		m_Text = m_Transform.Find("Number").GetComponent<Text>();
	}
	
	// init item
	public void InitItem(string name,int num){
		m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
		m_Text.text = num.ToString();
	}
}
