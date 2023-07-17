using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifleController : MonoBehaviour {
	private AssaultRifleView m_AssaultRifleView;

	private int id;
	private int damage;     //基礎攻撃力
	private int durable;    //最大耐久値
	private WeaponType weaponType;	//銃の種類

	private AudioClip audioClip;	//発射のサウンド
	private GameObject effect;  //発射炎エフェクト
	private Ray ray;	//発射の射線
	private RaycastHit hit; //目標物

	private ObjectPool []objectPools;	//オブジェクトプール

	#region
	public int Id{
		get { return id; }
		set { id = value; }
	}
	public int Damage{
		get { return damage; }
		set { damage = value; }
	}

	public int Durable{
		get { return durable; }
		set { durable = value; }
	}

	public WeaponType WeaponType{
		get { return weaponType; }
		set { weaponType = value; }
	}

	public AudioClip AudioClip{
		get { return audioClip; }
		set { audioClip = value; }
	}

	public GameObject Effect{
		get { return effect; }
		set { effect = value; }
	}
    #endregion

    void Start () {
		Init();
	}
	
	private void Init(){
		m_AssaultRifleView = gameObject.GetComponent<AssaultRifleView>();
		audioClip = Resources.Load<AudioClip>("Audio/Weapon/Drum_gun");
		effect = Resources.Load<GameObject>("Effect/Muzzle/AssaultRifle_GunPoint_Effect");
		objectPools = gameObject.GetComponents<ObjectPool>();

	}

	void Update () {
		Shoot();
		InputControl();

	}

	//発射
	private void Shoot(){
		ray = new Ray(m_AssaultRifleView.MuzzlePos.position, m_AssaultRifleView.MuzzlePos.forward);
		//デバッグ用射線
		Debug.DrawLine(m_AssaultRifleView.MuzzlePos.position, m_AssaultRifleView.MuzzlePos.forward * 1000, Color.cyan);

		if(Physics.Raycast(ray, out hit)){
			Debug.Log("あたり");
        }else {
			Debug.Log("はずれ");
			hit.point = Vector3.zero;

		}	
    }

	private void InputControl(){
		//弾を発射
		if (Input.GetMouseButtonDown(0)){
			m_AssaultRifleView.M_Animator.SetTrigger("fire");
			if (hit.point != Vector3.zero){
				Debug.Log("玉あり");
				//的に生成
                Instantiate(m_AssaultRifleView.Prefab_Bullet, hit.point, Quaternion.identity);
			}
			else{
				Debug.Log("玉なし");
			}
			//追加処理
			PlayFireAudio();
			PlayFireEffect();
			PlayFireAnimation();
		}

		//照準
		if (Input.GetMouseButton(1)){
			m_AssaultRifleView.M_Animator.SetBool("holdPose", true);
			m_AssaultRifleView.HoldPoseStart();
			m_AssaultRifleView.M_Crosshair.gameObject.SetActive(false);
		}

		//照準を解除
		if (Input.GetMouseButtonUp(1)){
			m_AssaultRifleView.M_Animator.SetBool("holdPose", false);
			m_AssaultRifleView.HoldPoseEnd();
			m_AssaultRifleView.M_Crosshair.gameObject.SetActive(true);

		}
	}

	//発射のサウンドエフェクト
	private void PlayFireAudio(){
		AudioSource.PlayClipAtPoint(audioClip, m_AssaultRifleView.MuzzlePos.position);
	}

	//発射のエフェクト
	//発射炎
	private void PlayFireEffect(){
		GameObject fire = null;

		if(objectPools[0].isEmpty()){
			//非アクティブなオブジェクトがない場合新規生成
			fire = GameObject.Instantiate<GameObject>(effect, m_AssaultRifleView.MuzzlePos.position, Quaternion.identity,m_AssaultRifleView.M_FiretParent);
			fire.name = "Fire";
		}else{
			//オブジェクト再利用
			fire = objectPools[0].GetObject();
			fire.transform.position = m_AssaultRifleView.MuzzlePos.position; 
		}
	
		fire.GetComponent<ParticleSystem>().Play();
		StartCoroutine(Delay(objectPools[0], fire, 1.0f));
		
	}

	//発射のアニメーション
	private void PlayFireAnimation()
	{
		GameObject shell = null;
		if(objectPools[1].isEmpty()){
			//非アクティブなオブジェクトがない場合新規生成
			shell =  GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Shell, m_AssaultRifleView.M_EjectionPos.position, Quaternion.identity, m_AssaultRifleView.M_ShellParent);
			shell.name = "Shell";
		}else{
			//オブジェクト再利用
			shell = objectPools[1].GetObject();
			shell.GetComponent<Rigidbody>().isKinematic = true;
			shell.transform.position = m_AssaultRifleView.M_EjectionPos.position; 
			shell.GetComponent<Rigidbody>().isKinematic = false;
		}

		Rigidbody shellRigidbody = shell.GetComponent<Rigidbody>();
		//エジェクションポートから空薬莢が弾き出される
		shellRigidbody.AddForce(m_AssaultRifleView.M_EjectionPos.up * Random.Range(60, 70));
		StartCoroutine(Delay(objectPools[1], shell, 3.0f));
	}

	//一定時間後にプールによって管理される
	private IEnumerator Delay (ObjectPool objectPool, GameObject gameObject, float delayTime){
		yield return new WaitForSeconds(delayTime);
		objectPool.AddObject(gameObject);
		
	}




}
