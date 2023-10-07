using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    void Update(){
        if (Input.GetKeyDown(KeyCode.Space)){
            Death();
        }
    }

    private void Death(){
        GameObject.Destroy(gameObject);
        SendMessageUpwards("EnemyEntityDeath", gameObject);
    }
}
