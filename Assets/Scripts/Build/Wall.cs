using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MaterialModelBase {
    protected override void OnCollisionEnter(Collision coll){
     
    }

    protected override void OnCollisionExit(Collision coll){
    }

    protected override void OnCollisionStay(Collision coll){
    }

    protected override void OnTriggerEnter(Collider coll){
        if (coll.gameObject.tag == "PlatformToWall"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
            transform.rotation = coll.gameObject.transform.rotation;

        }
    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.tag == "PlatformToWall"){
            IsCanPut = false;
            IsAttach = false;
        }
    }

    void Start (){
		
	}
	
	void Update (){
		
	}
}
