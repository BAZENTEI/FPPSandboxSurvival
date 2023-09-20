using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowContorller : MonoBehaviour {

	private Rigidbody m_Rigidbody;
	private BoxCollider m_BoxCollider;

    //Awake必須
	void Awake () {
		m_Rigidbody = GetComponent<Rigidbody>();
		m_BoxCollider = GetComponent<BoxCollider>();
	}
	
	public void Shoot(Vector3 dir, int force, int damage){
		m_Rigidbody.AddForce(dir * force);
    }


	void OnCollisionEnter(Collision coll){
        Debug.Log("アロー:あたり");
        m_Rigidbody.Sleep();
        Destroy(m_BoxCollider);
        //矢が刺さったものに付く
        transform.SetParent(coll.collider.gameObject.transform);

     
        if(coll.collider.gameObject.layer == LayerMask.NameToLayer("Env")){
       
            //todo:ダメージ計算
			
        }

	}
}
