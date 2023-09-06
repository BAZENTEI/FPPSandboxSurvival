using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class WeaponControllerBase : MonoBehaviour
{
    private WeaponViewBase m_WeaponViewBase;

    private Ray ray;    //発射の射線
    private RaycastHit hit; //目標物

    private AudioClip audioClip;    //発射のサウンド
    private GameObject effect;  //発射炎エフェクト

    [SerializeField] private int id;
    [SerializeField] private WeaponType weaponType;  //銃の種類
    [SerializeField] private int damage;     //基礎攻撃力
    [SerializeField] private int durable;    //最大耐久値
    

    public WeaponViewBase M_WeaponViewBase { get { return m_WeaponViewBase; } set { m_WeaponViewBase = value; }}

    public Ray MuzzleRay { get { return ray; } set { ray = value; }}
    public RaycastHit Hit { get { return hit; } set { hit = value; }}

    public AudioClip AudioClip { get { return audioClip; } set { audioClip = value; }}
    public GameObject Effect { get { return effect; } set { effect = value; }}

    public int Id{ get { return id; } set { id = value; }}
    public WeaponType WeaponType { get { return weaponType; } set { weaponType = value; }}
    public int Damage{ get { return damage; } set { damage = value; }}
    public int Durable{
        get { return durable; }
        set {
            durable = value;
            if (durable < 0){
                GameObject.Destroy(gameObject);
                GameObject.Destroy(m_WeaponViewBase.M_Crosshair.gameObject);
            }
        }
    }


    private bool readyToFire = true;

    public virtual void Start(){
        m_WeaponViewBase = gameObject.GetComponent<WeaponViewBase>();
        LoadAsset();
        Init();
    }


    void Update(){
        PreShoot();
        Controller();
    }

    //発射
    public void PreShoot(){
        ray = new Ray(M_WeaponViewBase.MuzzlePos.position, M_WeaponViewBase.MuzzlePos.forward);
        //デバッグ用射線
        //Debug.DrawLine(M_WeaponViewBase.MuzzlePos.position, M_WeaponViewBase.MuzzlePos.forward * 1000, Color.cyan);


        if (Physics.Raycast(ray, out hit)){
            Debug.Log("あたり");
        }else{
            Debug.Log("はずれ");
            hit.point = Vector3.zero;
        }
    }

    //発射のサウンドエフェクト
    public void PlayFireAudio(){
        AudioSource.PlayClipAtPoint(AudioClip, M_WeaponViewBase.MuzzlePos.position);
    }

    public void MouseButtonDownLeft() {
        Shoot();
        //発射のアニメーション
        M_WeaponViewBase.M_Animator.SetTrigger("fire");
        //追加処理
        PlayFireAudio();
        PlayFireEffect();
        PlayFireAnimation();
    }

    public void MouseButtonRight(){
        //照準
        M_WeaponViewBase.M_Animator.SetBool("holdPose", true);
        M_WeaponViewBase.HoldPoseStart();
        M_WeaponViewBase.M_Crosshair.gameObject.SetActive(false);       
    }

    public void MouseButtonUpRight(){
        //照準を解除
        M_WeaponViewBase.M_Animator.SetBool("holdPose", false);
        M_WeaponViewBase.HoldPoseEnd();
        M_WeaponViewBase.M_Crosshair.gameObject.SetActive(true);
    }

    public void Controller() {
        if (Input.GetMouseButtonDown(0)&& readyToFire)
        {
            MouseButtonDownLeft();
        }

        if (Input.GetMouseButton(1)){
            MouseButtonRight();
        }

        if (Input.GetMouseButtonUp(1)){
            MouseButtonUpRight();
        }

    }

    //一定時間後にプールによって管理される
    public IEnumerator Delay(ObjectPool objectPool, GameObject gameObject, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        objectPool.AddObject(gameObject);

    }

    public void ReadyToFire(int state){
        if (state == 0) readyToFire = false;
        else readyToFire = true;
    }

    public abstract void Init();

    public abstract void LoadAsset();

    public abstract void Shoot();

    public abstract void PlayFireEffect();

    public abstract void PlayFireAnimation();

}
