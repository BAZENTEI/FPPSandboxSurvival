using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	private Queue<GameObject> pool = null;
	
	void Awake(){
		pool = new Queue<GameObject>();
	}
	
	//追加
	public void AddObject(GameObject gameObject){
		gameObject.SetActive(false);
		pool.Enqueue(gameObject);
	}

	//取り出す
	public GameObject GetObject(){
		GameObject temp = null;
		if(pool.Count > 0){
			temp = pool.Dequeue();
			temp.SetActive(true);
		}
		return temp;
	}

	//空であるかどうか
	public bool isEmpty(){
		if(pool.Count > 0){
			return false;
		}else{
			return true;
		}
	}



}
