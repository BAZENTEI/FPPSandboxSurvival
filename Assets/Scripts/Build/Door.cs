using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MaterialModelBase
{
    private GameObject trigger = null;
    
    public override void Normal(){
        base.Normal();
        if(trigger != null){
            Destroy(trigger);
        }
    }



    protected override void OnTriggerEnter(Collider coll){
        if (coll.gameObject.name == "DoorTrigger"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.parent.Find("DoorPos").position;
            transform.rotation = coll.gameObject.transform.rotation;

            transform.parent = coll.gameObject.transform.parent;
            trigger = coll.gameObject;

        }
    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.name == "DoorTrigger"){
            IsCanPut = false;
            IsAttach = false;

            transform.parent = null;
        }

    }

    void Start () {
		
	}
	
	void Update () {
		
	}

}
