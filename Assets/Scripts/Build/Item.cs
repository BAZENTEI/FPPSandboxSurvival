using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour {

	private Image icon_Image;
	//private Image icon_BG;
	//private Sprite[] icons;
	/*public Sprite[] Icons {
        get { return icons; }
        set { icons = value; }
	}*/
	public List<GameObject> materialList = new List<GameObject>();

	void Awake () {
		icon_Image = transform.Find("Icon").GetComponent<Image>();
	}
	
	public void Init (string name, Quaternion quaternion, bool isIcon, Sprite sprite, bool isShow){
		gameObject.name = name;
		//this.name
		transform.rotation = quaternion;
		transform.Find("Icon").rotation = Quaternion.Euler(Vector3.zero);
		icon_Image.enabled = isIcon;
		icon_Image.sprite = sprite;
		transform.GetComponent<Image>().enabled = isShow;

		//gameObject.GetComponent<Image>().enabled = isShow;
	}

	public void Show(){
		transform.GetComponent<Image>().enabled = true;
		ShowAndHide(true);
		//ShowInfo();
	}

	public void Hide(){
		transform.GetComponent<Image>().enabled = false;
		ShowAndHide(false);
	}

	public void MaterialListAdd(GameObject material)
    {
		materialList.Add(material);

	}

	private void ShowAndHide(bool active){
		if (materialList == null) return;
		for (int i = 0; i < materialList.Count; i++){
			materialList[i].SetActive(active);
		}
	}

	//テスト
	/*private void ShowInfo(){
		Debug.Log("ShowInfo");
		if (icons == null) {
			Debug.Log("icons == null");
			return;
		}
		for (int i = 0;i < icons.Length; i++){
			Debug.Log(icons[i].name);
			//Debug.Log(icons[i]);
		}
	}*/
}
