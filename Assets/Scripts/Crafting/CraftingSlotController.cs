using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingSlotController : MonoBehaviour {

	private Transform m_Transform;
	private Image m_Image;
	private CanvasGroup m_CanvasGroup;

	private bool isOpen = false;
	public bool IsOpen { get { return isOpen; } } 

	private int id = -1;    //スロットのid
	public int Id { get { return id; } }

	// Update is called once per frame
	void Awake () {
		m_Transform = gameObject.transform;
		m_Image = m_Transform.Find("Item").GetComponent<Image>();
		m_CanvasGroup = m_Transform.Find("Item").GetComponent<CanvasGroup>();
		m_Image.gameObject.SetActive(false);
	}

	public void Init(Sprite sprite, int id){
		m_Image.gameObject.SetActive(true);
		m_Image.sprite = sprite;
		//
		m_CanvasGroup.blocksRaycasts = false;
		//
		isOpen = true;
		this.id = id;
	}

	public void Reset(){
		m_Image.gameObject.SetActive(false);

	}
}
