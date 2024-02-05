using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CeilingLight : MaterialModelBase {

    protected override void OnTriggerEnter(Collider coll){
        if (coll.gameObject.name == "LightTrigger"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
        }
    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.name == "LightTrigger"){
            IsCanPut = false;
            IsAttach = false;
        }
    }
}
