using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	
	public void OpenDoor () {
		transform.Rotate(Vector3.up, -90.0f);
	}

	public void CloseDoor (){
        transform.Rotate(Vector3.up, 90.0f);

    }



}
