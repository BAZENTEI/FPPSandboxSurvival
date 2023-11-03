using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyManagerType{
    NULL,
    CANNIBAL,//カニバル 人食い cannibalistic mutants
    BOAR
}
public class EnemyEntityManager : MonoBehaviour {
    private GameObject prefab_Cannibal;
    private GameObject prefab_Boar;
    private EnemyManagerType enemyManagerType = EnemyManagerType.NULL;
    private List<GameObject> entityList = new List<GameObject>(); //エネミーの参照

    private Transform[] posTransform;
    private List<Vector3> posList = new List<Vector3>(); //

    private int index = 0;
    public EnemyManagerType EnemyManagerType { get { return enemyManagerType; } set { enemyManagerType = value; }}

    [SerializeField] private int entity_attack;
    [SerializeField] private int entity_life = 20;

    void Start(){
        prefab_Cannibal = Resources.Load<GameObject>("AI/Cannibal");
        prefab_Boar = Resources.Load<GameObject>("AI/Boar");
       
        posTransform = transform.GetComponentsInChildren<Transform>(true);
      
        for (int i= 1;i < posTransform.Length; i++){
            posList.Add(posTransform[i].position);
        }
        GenerateEnemyEntityByEnum();
    }

    /// <summary>
    /// 
    /// </summary>
    private void GenerateEnemyEntityByEnum(){
      
        switch (enemyManagerType){
            case EnemyManagerType.CANNIBAL:
                Debug.Log("GenerateEnemyEntityByEnum : CANNIBAL");
                GenerateEnemyEntity(prefab_Cannibal, EnemyManagerType.CANNIBAL);
                break;
            case EnemyManagerType.BOAR:
                Debug.Log("GenerateEnemyEntityByEnum : BOAR");
                GenerateEnemyEntity(prefab_Boar, EnemyManagerType.BOAR);
                break;
            default:
                Debug.Log("GenerateEnemyEntityByEnum : default");
                break;
        }
    }

    /// <summary>
    /// エネミーの生成(初期化)
    /// </summary>
     private void GenerateEnemyEntity(GameObject prefab, EnemyManagerType enemyManagerType)
    {
        for(int i = 0; i < posTransform.Length - 1; i++){
            GameObject entity = Instantiate<GameObject>(prefab, transform.position, Quaternion.identity, transform);
            EnemyEntityController enemyEntityController = entity.GetComponent<EnemyEntityController>();
            enemyEntityController.Dir = posList[i];
            enemyEntityController.DirList = posList;
            enemyEntityController.EnemyManagerType = enemyManagerType;

            enemyEntityController.Life = entity_life;
            enemyEntityController.Attack = entity_attack;
            entityList.Add(entity);
                 
        }
     }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="enemyEntity"></param>
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
                entity.GetComponent<EnemyEntityController>().Dir = posList[index];
                entity.GetComponent<EnemyEntityController>().DirList = posList;
                entity.GetComponent<EnemyEntityController>().EnemyManagerType = EnemyManagerType.CANNIBAL;

                //
                break;
            case EnemyManagerType.BOAR:
                entity = Instantiate<GameObject>(prefab_Boar, transform.position, Quaternion.identity, transform);
                entity.GetComponent<EnemyEntityController>().Dir = posList[index];
                entity.GetComponent<EnemyEntityController>().DirList = posList;
                entity.GetComponent<EnemyEntityController>().EnemyManagerType = EnemyManagerType.BOAR;

                //
                break;
            default:
                Debug.Log("GenerateEnemyEntityByEnum : pass");
                break;
        }

        entity.GetComponent<EnemyEntityController>().Life = entity_life;
        entity.GetComponent<EnemyEntityController>().Attack = entity_attack;


        index++;
        index = index % posList.Count;
        entityList.Add(entity);
    }

  

}
