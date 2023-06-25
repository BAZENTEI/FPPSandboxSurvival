using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	private Transform m_Transform;
	private GameObject m_Blueprint;
	private Animator m_Animator;
	//
	private bool isNormal = false;

	void Start () {
		m_Transform = gameObject.transform;
		m_Blueprint = m_Transform.Find("CharacterCamera/Building Plan").gameObject;
		m_Animator = m_Blueprint.GetComponent<Animator>();
	}
	
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.M)){
			Changed();
        }


	}

	private void Changed(){
        if (isNormal){
			m_Blueprint.SetActive(true);
			isNormal = false;

		}
		else
		{
			m_Animator.SetTrigger("Holster");
			StartCoroutine("DelayTime");
			isNormal = true;
		}
    }

	IEnumerator DelayTime()
    {
		yield return new WaitForSeconds(1);
		m_Blueprint.SetActive(false);

	}
}
