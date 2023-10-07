using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 総マネージャー
/// </summary>
public class EnemyEntityManagers : MonoBehaviour {

	private Transform[] SpwanPoints;    //スポーン地点

	void Awake (){
		SpwanPoints = transform.GetComponentsInChildren<Transform>();
		GenerateEnemyEntityManager();
	}
	
	private void GenerateEnemyEntityManager(){
		
		for (int i = 1; i < SpwanPoints.Length; i++){
			if(i % 2 == 0){
				SpwanPoints[i].gameObject.AddComponent<EnemyEntityManager>().EnemyManagerType = EnemyManagerType.CANNIBAL;
            }else {
				SpwanPoints[i].gameObject.AddComponent<EnemyEntityManager>().EnemyManagerType = EnemyManagerType.BOAR;

			}

		}
    }
}
