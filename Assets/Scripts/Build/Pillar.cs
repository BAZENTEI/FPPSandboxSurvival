using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MaterialModelBase
{


    protected override void OnTriggerEnter(Collider coll){ 
        if (coll.gameObject.tag == "PlatformToPillar"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
        }

    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.tag == "PlatformToPillar"){
            IsCanPut = false;
            IsAttach = false;
        }
    }
}
