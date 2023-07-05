using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifleController : MonoBehaviour {
	private AssaultRifleView m_AssaultRifleView;

	private int id;
	private int damage;
	private int durable;
	private WeaponType weaponType;

	private AudioClip audioClip;
	private GameObject effect;
	private Ray ray;
	private RaycastHit hit;


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

	}

	void Update () {
		Shoot();
		InputControl();

	}

	//発射
	private void Shoot(){
		ray = new Ray(m_AssaultRifleView.WeaponPoint.position, m_AssaultRifleView.WeaponPoint.forward);
		Debug.DrawLine(m_AssaultRifleView.WeaponPoint.position, m_AssaultRifleView.WeaponPoint.forward * 300, Color.cyan);

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
				Debug.Log("玉のあり");
				GameObject.Instantiate<GameObject>(m_AssaultRifleView.Bullet, hit.point, Quaternion.identity);
			}
			else{
				Debug.Log("玉のなし");

			}
		}

		//照準
		if (Input.GetMouseButton(1)){
			m_AssaultRifleView.M_Animator.SetBool("holdPose", true);
			m_AssaultRifleView.HoldPoseStart();
		}

		//照準を解除
		if (Input.GetMouseButtonUp(1)){
			m_AssaultRifleView.M_Animator.SetBool("holdPose", false);
			m_AssaultRifleView.HoldPoseEnd();
		}
	}

	//発射のサウンドエフェクト
	private void PlayFireAudio(){

	}

	//サウンドエフェクト
	private void PlayAudioEffect(){

    }


}
