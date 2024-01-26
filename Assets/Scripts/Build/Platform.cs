using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MaterialModelBase
{

	protected override void OnCollisionEnter(Collision coll){
		if (coll.collider.tag != "Terrain"){
			IsCanPut = false;
		}

	}

	protected override void OnCollisionStay(Collision coll){
		if (coll.collider.tag != "Terrain"){
			IsCanPut = false;
		}
	}

	protected override void OnCollisionExit(Collision coll){
		if (coll.collider.tag != "Terrain"){
			IsCanPut = true;
		}
	}

	protected override void OnTriggerEnter(Collider coll)
	{
		if (coll.gameObject.tag == "Platform")
		{
			IsAttach = true;

			Vector3 modelPosition = Vector3.zero;
			Vector3 targetPos = coll.gameObject.transform.position;
			Vector3 selfPos = transform.position;

			//transform.position = coll.gameObject.transform.position + new Vector3(3.3f, 0, 0);
			float x = selfPos.x - targetPos.x;
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
			}

			transform.position = targetPos + modelPosition;
		}
	}

	protected override void OnTriggerExit(Collider coll){
		if (coll.gameObject.tag == "Platform"){
			IsAttach = false;
		}
	}
}
