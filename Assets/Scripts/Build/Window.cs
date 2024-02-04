using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MaterialModelBase
{

    protected override void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "windowTrigger"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
            transform.rotation = coll.gameObject.transform.rotation;
        }
    }

    protected override void OnTriggerExit(Collider coll)
    {
        if (coll.gameObject.name == "windowTrigger"){
            IsCanPut = false;
            IsAttach = false;
        }
    }
}
