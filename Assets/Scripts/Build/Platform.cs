using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MaterialModelBase
{
	private string indexName;
	private Transform targetPlatform;

	void Start(){
        IsCanPut = true;

    }

    public override void Normal(){
        base.Normal();
		if(targetPlatform != null)
		Destroy(targetPlatform.Find(indexName).gameObject);
        switch (indexName){
            case "A":
                Destroy(transform.Find("C").gameObject);
                break;
            case "B":
                Destroy(transform.Find("D").gameObject);
                break;
            case "C":
                Destroy(transform.Find("A").gameObject);
                break;
            case "D":
                Destroy(transform.Find("B").gameObject);
                break;
        }
    }

    protected void OnCollisionEnter(Collision coll){
		if (coll.collider.tag != "Terrain"){
			IsCanPut = false;
		}

	}

	protected void OnCollisionStay(Collision coll){
		if (coll.collider.tag != "Terrain"){
			IsCanPut = false;
		}
	}

	protected void OnCollisionExit(Collision coll){
		if (coll.collider.tag != "Terrain"){
			IsCanPut = true;
		}
	}

	protected override void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "PlatformToWall")
		{
			IsAttach = true;

			Vector3 modelPosition = Vector3.zero;
			Vector3 targetPos = coll.gameObject.transform.parent.position;
			targetPlatform = coll.gameObject.transform.parent;
            //Vector3 selfPos = transform.position;

            //transform.position = coll.gameObject.transform.position + new Vector3(3.3f, 0, 0);
            /*float x = selfPos.x - targetPos.x;
			float z = selfPos.z - targetPos.z;

			if (x > 0 && Mathf.Abs(z) < 0.4f){
				modelPosition = new Vector3(3.3f, 0, 0);
			}
			else if (x < 0 && Mathf.Abs(z) < 0.4f){
				modelPosition = new Vector3(-3.3f, 0, 0);
			}

			if (z > 0 && Mathf.Abs(x) < 0.4f){
				modelPosition = new Vector3(0, 0, 3.3f);
			}
			else if (z < 0 && Mathf.Abs(x) < 0.4f){
				modelPosition = new Vector3(0, 0, -3.3f);
			}*/

            switch (coll.gameObject.name){
                case "A":
                    modelPosition = new Vector3(-3.3f, 0.0f, 0.0f);
					indexName = "A";
                    break;
                case "B":
                    modelPosition = new Vector3(0.0f, 0.0f, 3.3f);
                    indexName = "B";
                    break;
                case "C":
                    modelPosition = new Vector3(3.3f, 0.0f, 0.0f);
                    indexName = "C";
                    break;
                case "D":
                    modelPosition = new Vector3(0.0f, 0.0f, -3.3f);
                    indexName = "D";
                    break;

            }
			//Debug.Log("OnTriggerEnter!!!!!!!!!!!!!!");
            transform.position = targetPos + modelPosition;
		}
	}

	protected override void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Platform"){
			IsAttach = false;
		}
	}
}
