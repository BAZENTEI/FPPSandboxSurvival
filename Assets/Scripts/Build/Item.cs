using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	private Image icon_Image;
	//private Image icon_BG;

	void Awake () {
		icon_Image = transform.Find("Icon").GetComponent<Image>();
	}
	
	
	public void Init (string name, Quaternion quaternion, bool isIcon, Sprite sprite, bool isShow) {
		gameObject.name = name;
		//this.name
		transform.rotation = quaternion;
		transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
		icon_Image.enabled = isIcon;
		icon_Image.sprite = sprite;
		transform.GetComponent<Image>().enabled = isShow;

		//gameObject.GetComponent<Image>().enabled = isShow;
	}

	public void Show()
    {
		transform.GetComponent<Image>().enabled = true;

	}

	public void Hide()
	{
		transform.GetComponent<Image>().enabled = false;
	}

}
