﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {
    private Transform m_Transform;
	
	void Start () {
		
	}
	
	void Update () {
        gameObject.transform.Rotate(Vector3.up * Random.Range(30.0f, 50.0f));
	}
}
