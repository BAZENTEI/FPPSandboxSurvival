using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotController : MonoBehaviour {

	private Transform m_Transform;
	private Image m_Image;

	// Update is called once per frame
	void Awake () {
		m_Transform = gameObject.transform;
		m_Image = m_Transform.Find("Item").GetComponent<Image>();
		m_Image.gameObject.SetActive(false);
	}

	public void Init(Sprite sprite){
		m_Image.gameObject.SetActive(true);
		m_Image.sprite = sprite;
	}

	public void Reset(){
		m_Image.gameObject.SetActive(false);

	}
}
