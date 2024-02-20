using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Window : MaterialModelBase
{
    private GameObject trigger = null;

    public override void Normal(){
        base.Normal();
        if (trigger != null){
            Destroy(trigger);
        }
    }

    protected override void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.name == "windowTrigger"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
            transform.rotation = coll.gameObject.transform.rotation;

            trigger = coll.gameObject;
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
