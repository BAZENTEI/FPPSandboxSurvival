using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 総マネージャー
/// </summary>
public class EnemyEntityManagers : MonoBehaviour {

	private Transform[] SpwanPoint;    //スポーン地点

	void Awake (){
		SpwanPoint = transform.GetComponentsInChildren<Transform>(false); //
		GenerateEnemyEntityManager();
	}
	
	private void GenerateEnemyEntityManager(){
		Debug.Log("SpwanPoint Count:" + (SpwanPoint.Length -1));
		for (int i = 1; i < SpwanPoint.Length; i++){
			
			if(i % 2 == 0){
			
				SpwanPoint[i].gameObject.AddComponent<EnemyEntityManager>().EnemyManagerType = EnemyManagerType.CANNIBAL;
            }else {
				SpwanPoint[i].gameObject.AddComponent<EnemyEntityManager>().EnemyManagerType = EnemyManagerType.BOAR;

			}

		}
    }
}
