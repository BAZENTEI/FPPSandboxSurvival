using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour{
    private bool isEnter = false;       //連打ち

	void Update () {
		if( Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return) ){
            if (isEnter == false){
                isEnter = true;
                SceneManager.LoadScene("Game");
            }
        }
	}
}
