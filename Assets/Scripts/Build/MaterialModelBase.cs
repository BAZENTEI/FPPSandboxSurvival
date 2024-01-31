﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MaterialModelBase : MonoBehaviour {

	private bool isCanPut = true;
	private bool isAttach = false;

	private Material oldMaterial;
	private Material newMaterial;

	public bool IsCanPut { get { return isCanPut; } set { isCanPut = value; } }
	public bool IsAttach { get { return isAttach; } set { isAttach = value; } }

	void Awake(){
		newMaterial = Resources.Load<Material>("Build/Building Preview");
		oldMaterial = gameObject.GetComponent<MeshRenderer>().material;
		gameObject.GetComponent<MeshRenderer>().material = newMaterial;
	}
	

	void Update()
	{
		if (isCanPut)
		{
			gameObject.GetComponent<MeshRenderer>().material.color = new Color32(0, 255, 0, 100);
		}
		else
		{
			gameObject.GetComponent<MeshRenderer>().material.color = new Color32(255, 0, 0, 100);
		}
	}

	public void Normal()
	{
		gameObject.GetComponent<MeshRenderer>().material = oldMaterial;
	}

	protected abstract void OnCollisionEnter(Collision coll);
	protected abstract void OnCollisionStay(Collision coll);
	protected abstract void OnCollisionExit(Collision coll);
	protected abstract void OnTriggerEnter(Collider coll);
	protected abstract void OnTriggerExit(Collider coll);
	


}
