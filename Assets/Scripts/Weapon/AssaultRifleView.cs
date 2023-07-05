using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleView : MonoBehaviour {
	//コンポーネント参照
	private Animator m_Animator;
	private Camera m_EnvCamera;

	public Animator M_Animator { get { return m_Animator; } }
	public Camera M_EnvCamera { get { return m_EnvCamera; } }

	//照準のアニメーション
	private Vector3 startPos;
	private Vector3 endPos;
	private Vector3 startRot;
	private Vector3 endRot;

	private Transform weaponPoint;  //マズルの位置
	private GameObject bullet;
	public Transform WeaponPoint { get { return weaponPoint; } }    
	public GameObject Bullet { get { return bullet; } }

	void Awake(){
		m_Animator = gameObject.GetComponent<Animator>();
		m_EnvCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();

		startPos = gameObject.transform.localPosition;
		startRot = gameObject.transform.localRotation.eulerAngles;
		endPos = new Vector3(-0.065f, -1.85f, 0.25f);
		endRot = new Vector3(2.8f, 1.3f, 0.08f);

		weaponPoint = gameObject.transform.Find("Assault_Rifle/EffectPos_Muzzle");

		bullet = Resources.Load<GameObject>("Bullet");
	}

	//照準のアニメーション
	public void HoldPoseStart(){
		gameObject.transform.DOLocalMove(endPos, 0.2f);
		gameObject.transform.DOLocalRotate(endRot, 0.2f);
		m_EnvCamera.DOFieldOfView(40, 0.2f);
	}

	//照準(解除)のアニメーション
	public void HoldPoseEnd(){
		gameObject.transform.DOLocalMove(startPos, 0.2f);
		gameObject.transform.DOLocalRotate(startRot, 0.2f);

		m_EnvCamera.DOFieldOfView(60, 0.2f);

	}
}
