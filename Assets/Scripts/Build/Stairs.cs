using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MaterialModelBase {
    private GameObject trigger = null;

    public override void Normal(){
        base.Normal();
        if(trigger != null) { trigger.name = "E"; }
    }


    protected override void OnTriggerEnter(Collider coll){
        if(coll.gameObject.tag == "PlatformToWall"){
            IsAttach = true;
            IsCanPut = true;

            Vector3 modelPos = Vector3.zero;
            Vector3 modelRot = Vector3.zero;
            Vector3 targetPos = coll.gameObject.transform.parent.position;

            switch (coll.gameObject.name[0].ToString()) {
                case "A":
                    modelPos = new Vector3(-2.5f, 0.0f, 0.0f);
                    modelRot = new Vector3(0.0f, 0.0f, 0.0f);

                    break;
                case "B":
                    modelPos = new Vector3(0.0f, 0.0f, 2.5f);
                    modelRot = new Vector3(0.0f, 90, 0.0f);

                    break;
                case "C":
                    modelPos = new Vector3(2.5f, 0.0f, 0.0f);
                    modelRot = new Vector3(0.0f, 180, 0.0f);

                    break;
                case "D":
                    modelPos = new Vector3(0.0f, 0.0f, -2.5f);
                    modelRot = new Vector3(0.0f, 270, 0.0f);

                    break;

            }

            transform.position = targetPos + modelPos;
            transform.rotation = Quaternion.Euler(modelRot);
            trigger = coll.gameObject;
        }

    }

    protected override void OnTriggerExit(Collider coll){
        if (coll.gameObject.tag == "PlatformToWall"){
            IsAttach = false;
            IsCanPut = false;

        }

    }

    void Start () {
		
	}
	
	void Update () {
		
	}





}
