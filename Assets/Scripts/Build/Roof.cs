using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roof : MaterialModelBase {

    protected override void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "WallToRoof"){
            IsCanPut = true;
            IsAttach = true;
            transform.position = coll.gameObject.transform.position;
        }

        if (coll.gameObject.tag == "Roof"){
            IsCanPut = true;
            IsAttach = true;

            Vector3 targetPos = coll.gameObject.transform.parent.position;
            Vector3 selfPos = Vector3.zero;

            switch (coll.gameObject.name){
                case "A":
                    selfPos = new Vector3(0, 0, 3.3f);
                    break;
                case "B":
                    selfPos = new Vector3(3.3f, 0, 0);
                    break;
                case "C":
                    selfPos = new Vector3(0, 0, -3.3f);
                    break;
                case "D":
                    selfPos = new Vector3(-3.3f, 0, 0);
                    break;
            }
            transform.position = targetPos + selfPos;
        }
    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.tag == "WallToRoof"){
            IsCanPut = false;
            IsAttach = false;
        }
    }




}
