using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
	private bool isCanPut = true;
	private bool isAttach = false;

	public bool IsCanPut{ get { return isCanPut; }}
	public bool IsAttach { get { return isAttach; } set { isAttach = value; } }

	void Start () {
		
	}
	
	void Update () {
		if (isCanPut){
			gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
		}else{
			gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
		}
	}

	public void Normal(){
		gameObject.GetComponent<MeshRenderer>().material.color = Color.white;
	}

	void OnCollisionEnter(Collision coll){
		if (coll.collider.tag != "Terrain"){
			isCanPut = false;
		}
	}

	void OnCollisionStay(Collision coll){
		if (coll.collider.tag != "Terrain"){
			isCanPut = false;
		}
	}

	void OnCollisionExit(Collision coll){
		if (coll.collider.tag != "Terrain"){
			isCanPut = true;
		}
	}

	void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Platform")
		{
			isAttach = true;
			transform.position = coll.gameObject.transform.position + new Vector3(3.3f, 0, 0);
		}
	}

	void OnTriggerExit(Collider coll)
	{
		if (coll.gameObject.tag == "Platform")
		{
			isAttach = false;
		}
	}
}
