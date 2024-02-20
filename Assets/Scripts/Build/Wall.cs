using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MaterialModelBase {

    private GameObject trigger = null;

    public override void Normal(){
        base.Normal();
        if(trigger != null) trigger.name = trigger.name + "_Over";
    }

    protected override void OnTriggerEnter(Collider coll){
        if (coll.gameObject.tag == "PlatformToWall" && coll.gameObject.name.Length == 1)
        {
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
            transform.rotation = coll.gameObject.transform.rotation;
            trigger = coll.gameObject;
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
