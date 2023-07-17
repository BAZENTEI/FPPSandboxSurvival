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

	private Vector3 startPos;   //照準のアニメーション
	private Vector3 endPos;
	private Vector3 startRot;
	private Vector3 endRot;

	private GameObject prefab_Bullet;	//弾
	private Transform m_Crosshair;	//クロスヘア
	private Transform muzzlePos;  //マズルの位置
	private Transform ejectionPos; //エジェクションポートの位置
	private GameObject prefab_Shell;    //薬莢

	public GameObject Prefab_Bullet { get { return prefab_Bullet; } }
	public Transform M_Crosshair { get { return m_Crosshair; } }
	public Transform MuzzlePos { get { return muzzlePos; } }
	public Transform M_EjectionPos { get { return ejectionPos; } }
	public GameObject Prefab_Shell { get { return prefab_Shell; } }

	private Transform firetParent;	
	private Transform shellParent;
	public Transform M_FiretParent { get { return firetParent; } }
	public Transform M_ShellParent { get { return shellParent; } }

	void Awake(){
		m_Animator = gameObject.GetComponent<Animator>();
		m_EnvCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();

		startPos = gameObject.transform.localPosition;
		startRot = gameObject.transform.localRotation.eulerAngles;
		endPos = new Vector3(-0.065f, -1.85f, 0.25f);
		endRot = new Vector3(2.8f, 1.3f, 0.08f);

		//コンポーネントプロパティ
		muzzlePos = gameObject.transform.Find("Assault_Rifle/Pos_Muzzle");
		ejectionPos = gameObject.transform.Find("Assault_Rifle/Pos_Ejection");
		m_Crosshair = GameObject.Find("Crosshair").gameObject.transform;
		prefab_Bullet = Resources.Load<GameObject>("Bullet");
		prefab_Shell = Resources.Load<GameObject>("Animation/Ejection/Shell");

		firetParent = GameObject.Find("ObjectPool/Fire_Parent").transform;
		shellParent = GameObject.Find("ObjectPool/Shell_Parent").transform;
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
