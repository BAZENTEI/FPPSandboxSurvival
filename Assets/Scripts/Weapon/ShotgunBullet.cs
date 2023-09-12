using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : MonoBehaviour {

	private Rigidbody m_Rigidbody;
	// Use this for initialization
	void Awake () {
		m_Rigidbody = gameObject.GetComponent<Rigidbody>();
	}
	
	public void Shoot (int force, Vector3 dir) {
		m_Rigidbody.AddForce(dir * force);
	}

	void OnCollisionEnter(Collision coll){
		m_Rigidbody.Sleep();

	}
}
