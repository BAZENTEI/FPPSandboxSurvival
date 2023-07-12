using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearBarSlotController : MonoBehaviour {
	private Text m_Text;
	private Button m_Button;
	private Image m_Image;
	private bool selfState = false;//false:アクティブ状態 true:アクティブ状態

	void Awake() {
		m_Text = gameObject.transform.Find("Shortcut").GetComponent<Text>();
		m_Button = gameObject.GetComponent<Button>();
		m_Button.onClick.AddListener(ButtonClick);
	}

	public void Init(string name, int Shortcut){
		gameObject.name = name;
		m_Text.text = Shortcut.ToString();
		m_Image = gameObject.GetComponent<Image>();

	}

	private void ButtonClick(){
		Debug.Log(selfState);
		
		if (selfState){
			Normal();
        }
        else {
			Active();
		}
		//親オブジェクトにアクティブ状態を管理してもらう
		SendMessageUpwards("SwitchActiveSlot", gameObject);
    }

	//非アクティブにする
	public void Normal(){
		m_Image.color = Color.white;
		selfState = false;
    }

	//アクティブにする
	private void Active(){
		m_Image.color = Color.green;
		selfState = true;

	}

}
