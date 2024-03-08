using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlayerEnterTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider coll){
        if (coll.gameObject.name == "FPSController"){
            if (gameObject.transform.parent.Find("Door(Clone)") != null){
                gameObject.transform.parent.Find("Door(Clone)").GetComponent<DoorController>().OpenDoor();
            }
        }
    }

    void OnTriggerExit(Collider coll){
        if (coll.gameObject.name == "FPSController") {
            
            if (gameObject.transform.parent.Find("Door(Clone)") != null){
                gameObject.transform.parent.Find("Door(Clone)").GetComponent<DoorController>().CloseDoor();
            }
        }
    }
}
