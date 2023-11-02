using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private GameObject m_BuildingPlan;
	private GameObject m_WoodenSpear;

	private GameObject presentItem;   //今持っている道具
	private GameObject targetItem;	//切り替える道具

	void Start(){
		//m_BuildingPlan = m_Transform.Find("CharacterCamera/Building Plan").gameObject;
		//m_WoodenSpear = m_Transform.Find("CharacterCamera/Wooden Spear").gameObject;

		//m_WoodenSpear.SetActive(false);
		presentItem = m_BuildingPlan;
		targetItem = m_BuildingPlan;
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.G)){
			targetItem = m_BuildingPlan;
			SwitchItem();

		}

		if (Input.GetKeyDown(KeyCode.H)){
			targetItem = m_WoodenSpear;
			SwitchItem();
		}
	}

	private void SwitchItem(){
		presentItem.GetComponent<Animator>().SetTrigger("holster");
		StartCoroutine("DelayTime");
		//presentItem.SetActive(false);
		//targetItem.SetActive(true);
		//presentItem = targetItem;
	}

	private IEnumerator DelayTime() {
		yield return new WaitForSeconds(1);

		presentItem.SetActive(false);
		targetItem.SetActive(true);
		presentItem = targetItem;
	}
}
