using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrialItem : MonoBehaviour {

	private Image icon_Image;

	void Start () {
		icon_Image = transform.Find("Icon").GetComponent<Image>();
	}
	
	void Update () {		

	}

	public void Normal(){
		icon_Image.color = Color.white;
	}

	//ハイライト表示
	public void Highlight(){
		icon_Image.color = Color.red;
	}
}
