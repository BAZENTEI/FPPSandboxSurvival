using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLight : MaterialModelBase {
    private GameObject trigger = null;

    public override void Normal(){
        base.Normal();
        if (trigger != null){
            Destroy(trigger);
        }
    }

    protected override void OnTriggerEnter(Collider coll){
        if (coll.gameObject.name == "LightTrigger"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;

            trigger = coll.gameObject;
        }
    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.name == "LightTrigger"){
            IsCanPut = false;
            IsAttach = false;
        }
    }
}
