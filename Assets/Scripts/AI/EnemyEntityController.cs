using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// エネミーのアニメーション状態
/// </summary>
public enum AIState
{
    NULL,
    IDLE,
    WALK,
    ENTERRUN,
    EXITRUN,
    ENTERATTACK,
    EXITATTACK,
    DEATH
}
public class EnemyEntityController : MonoBehaviour {
    private NavMeshAgent m_navMeshAgent;
    private Animator m_Animator;
    private AIState m_AIState = AIState.IDLE;
    private EnemyManagerType enemyManagerType;
    private EnemyEntityRagdoll enemyEntityRagdoll = null;
    private GameObject prefab_FleshEffect;

    private Vector3 dir;
    private List<Vector3> dirList = new List<Vector3>();
    private Transform playerTransform;
    
    private AIState M_AIState { get { return m_AIState; } set { m_AIState = value; } }
    public EnemyManagerType EnemyManagerType { get { return enemyManagerType; } set { enemyManagerType = value; } }
    public Vector3 Dir { get { return dir; } set { dir = value; } }
    public List<Vector3> DirList { get { return dirList; } set { dirList = value; } }

    [SerializeField] private int life;

    private PlayerController m_PlayerController;
    private int attack;
    public int Life { 
        get { return life; }
        set { 
            life = value; 
            if (life <= 0) ToggleState(AIState.DEATH);
        } 
    }
    public int Attack { get { return attack; } set { attack = value; } }

    void Start(){
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        Debug.Log("m_navMeshAgent:" + m_navMeshAgent);
        m_Animator = GetComponent<Animator>();
        prefab_FleshEffect = Resources.Load<GameObject>("Weapon/BulletHoles/Effects/Bullet Impact FX_Flesh");
        m_navMeshAgent.SetDestination(dir);
        //CANNIBALだけはragdollを使う
        if (enemyManagerType == EnemyManagerType.CANNIBAL){
            Debug.Log("enemyEntityRagdoll::" + enemyEntityRagdoll);
            enemyEntityRagdoll = GetComponent<EnemyEntityRagdoll>();
        }

        playerTransform = GameObject.Find("FPPController").transform;
        m_PlayerController = playerTransform.GetComponent<PlayerController>();
        m_AIState = AIState.IDLE;
    }


    void Update(){
        Distance();
        EntityFollowPlayer();
        EntityAttackPlayer();

        if (Input.GetKeyDown(KeyCode.K)){
            ToggleState(AIState.DEATH);
        }

        if (Input.GetKeyDown(KeyCode.P)){
            HitHead();
        }

        if (Input.GetKeyDown(KeyCode.L)){
            HitNormal();
        }

        //Debug.Log("m_AIState:" + m_AIState);
    }

   

    /// <summary>
    /// 巡回の制御
    /// </summary>
    private void Distance(){
        if (m_AIState == AIState.IDLE || m_AIState == AIState.WALK){
            if (Vector3.Distance(transform.position, dir) < 1.0f){

                int index = Random.Range(0, dirList.Count);
                //Debug.Log("Distance" + index);
                dir = dirList[index];
                
                m_navMeshAgent.SetDestination(dir);

                ToggleState(AIState.IDLE);
            }else{
                ToggleState(AIState.WALK);
            }

        }
    }

    /// <summary>
    /// //プレヤー探しの制御
    /// </summary>
    private void EntityFollowPlayer(){
        if(Vector3.Distance(transform.position, playerTransform.position) <= 10.0f){
            //プレヤーにフォロー
            ToggleState(AIState.ENTERRUN);
            Debug.Log("EntityFollowPlayer");
        }
        else{
            //フォローしない
            ToggleState(AIState.EXITRUN);
        }
    }

    /// <summary>
    /// プレヤーに攻撃
    /// </summary>
    private void EntityAttackPlayer(){
        //|| m_AIState == AIState.WALK
        if (m_AIState == AIState.ENTERRUN ){
            if (Vector3.Distance(transform.position, playerTransform.position) <= 1.0f){
                //攻撃状態に移る
                Debug.Log("EntityAttackPlayer" + AIState.ENTERATTACK);
                ToggleState(AIState.ENTERATTACK);
            }else{
                //攻撃状態->ランニング
                Debug.Log("EntityAttackPlayer" + AIState.EXITATTACK);
                ToggleState(AIState.EXITATTACK);
            }
        }        
    }

    /// <summary>
    /// 状態の切り替えり
    /// </summary>
    private void ToggleState(AIState aiState){
        //Debug.Log("ToggleState :" + aiState);

        switch (aiState){
            case AIState.IDLE:
                IdleState();
                break;
            case AIState.WALK:
                WalkState();
                break;
            case AIState.ENTERRUN:
                EnterRunState();
                break;
            case AIState.EXITRUN:
                ExitRunState();
                break;
            case AIState.ENTERATTACK:
                EnterAttackState();
                break;
            case AIState.EXITATTACK:
                ExitAttackState();
                break;
            case AIState.DEATH:
                Death();
                break;
            default:
                break;
        }
    }

    //ディフォルト
    private void IdleState(){
        m_Animator.SetBool("Walk", false);
        m_AIState = AIState.IDLE;
    }

    //歩き状態
    private void WalkState(){
        m_Animator.SetBool("Walk", true);
        m_AIState = AIState.WALK;
    }

    /// <summary>
    /// ランニングに移る
    /// </summary>
    private void EnterRunState(){
        m_Animator.SetBool("Run", true);
        m_AIState = AIState.ENTERRUN;
        //
        m_navMeshAgent.speed = 2.0f;
        m_navMeshAgent.enabled = true;
        m_navMeshAgent.SetDestination(playerTransform.position);
    }

    /// <summary>
    /// ランニング終了
    /// </summary>
    private void ExitRunState(){
        m_Animator.SetBool("Run", false);
        ToggleState(AIState.WALK);
        m_navMeshAgent.speed = 0.8f;
        m_navMeshAgent.SetDestination(dir);
    }

    //攻撃を始め
    private void EnterAttackState(){
        m_Animator.SetBool("Attack", true);
        m_AIState = AIState.ENTERATTACK;
  
        m_navMeshAgent.enabled = false;
        
    }

    /// <summary>
    /// 攻撃を終了
    /// </summary>
    private void ExitAttackState(){
        m_Animator.SetBool("Attack", false);
        ToggleState(AIState.ENTERRUN);
        
        m_navMeshAgent.enabled = true;
       
    }

    //普通の攻撃
    private void HitNormal(){
        m_Animator.SetTrigger("HitNormal");
        Debug.Log("HitNormal");
    }

    /// <summary>
    /// ヘッドショット
    /// </summary>
    private void HitHead(){
        m_Animator.SetTrigger("HitHead");
        Debug.Log("HitHead");
    }


    /// <summary>
    /// 死亡の状態
    /// </summary>
    private void DeathState(){
        m_AIState = AIState.DEATH;
        m_navMeshAgent.isStopped = true;
        
      
        if (enemyManagerType == EnemyManagerType.BOAR){
            m_Animator.SetTrigger("Death");
        }else if (enemyManagerType == EnemyManagerType.CANNIBAL){
            m_Animator.enabled = false;
            enemyEntityRagdoll.StartRagdoll();  
        }
        
        StartCoroutine("Death");
    }

    private IEnumerator Death(){
        yield return new WaitForSeconds(3.0f);
        Destroy(gameObject);
        SendMessageUpwards("EnemyEntityDeath", gameObject);
    }

    /// <summary>
    /// 
    /// </summary>
    public void PlayFleshEffect(RaycastHit hit){
        Debug.Log("PlayFleshEffect");
        GameObject fleshEffect = Instantiate<GameObject>(prefab_FleshEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(fleshEffect, 5);
    }

    /// <summary>
    /// ボディ　ダメージを受けた
    /// </summary>
    public void NormalHit(int damage) {
        HitNormal();
        Life -= damage;
        Debug.Log("NormalHit");
    }

    /// <summary>
    /// ヘッドショット
    /// </summary>
    public void HeadHit(int damage){
        HitHead();
        Life -= damage;
        Debug.Log("HeadHit");
    }

    /// <summary>
    /// 攻撃
    /// </summary>
    private void AttackPlayer(){
        m_PlayerController.CutHP(this.attack);
    }
}
