using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyManagerType{
    NULL,
    CANNIBAL,
    BOAR
}
public class EnemyEntityManager : MonoBehaviour {
    private GameObject prefab_Cannibal;
    private GameObject prefab_Boar;
    private EnemyManagerType enemyManagerType = EnemyManagerType.NULL;
    private List<GameObject> entityList = new List<GameObject>(); //
    public EnemyManagerType EnemyManagerType { get { return enemyManagerType; } set { enemyManagerType = value; }}


    void Start(){
        prefab_Cannibal = Resources.Load<GameObject>("AI/Cannibal");
        prefab_Boar = Resources.Load<GameObject>("AI/Boar");
        GenerateEnemyEntityByEnum();
    }

    private void GenerateEnemyEntityByEnum(){
      
        switch (enemyManagerType){
            case EnemyManagerType.CANNIBAL:
                GenerateEnemyEntity(prefab_Cannibal);
                break;
            case EnemyManagerType.BOAR:
                GenerateEnemyEntity(prefab_Boar);
                break;
            default:
                Debug.Log("GenerateEnemyEntityByEnum : pass");
                break;
        }
    }

     private void GenerateEnemyEntity(GameObject prefab){
        for(int i = 0; i < 5; i++){
            entityList.Add(Instantiate<GameObject>(prefab, transform.position, Quaternion.identity, transform));
                 
        }
     }

    private void EnemyEntityDeath(GameObject enemyEntity){
        entityList.Remove(enemyEntity);
        StartCoroutine("GenerateEnemyEntityOnlyOne");
        
    }

    private IEnumerator GenerateEnemyEntityOnlyOne(){
        Debug.Log("EnemyEntity relod!");
        yield return new WaitForSeconds(2);

        GameObject entity = null;
        switch (enemyManagerType){
            case EnemyManagerType.CANNIBAL:
                entity = Instantiate<GameObject>(prefab_Cannibal, transform.position, Quaternion.identity, transform);
                break;
            case EnemyManagerType.BOAR:
                entity = Instantiate<GameObject>(prefab_Boar, transform.position, Quaternion.identity, transform);
                break;
            default:
                Debug.Log("GenerateEnemyEntityByEnum : pass");
                break;
        }
        //entityList.Add(entity);
    }

}
