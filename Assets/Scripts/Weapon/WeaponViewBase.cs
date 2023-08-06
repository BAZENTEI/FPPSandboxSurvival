using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
public abstract class WeaponViewBase : MonoBehaviour {

	//コンポーネント参照
	private Animator m_Animator;
	private Camera m_EnvCamera;

	public Animator M_Animator { get { return m_Animator; } }
	public Camera M_EnvCamera { get { return m_EnvCamera; } }

	private Vector3 startPos;   //照準のアニメーション
	private Vector3 endPos;
	private Vector3 startRot;
	private Vector3 endRot;

	public Vector3 M_StartPos { get { return startPos; } set { startPos = value; } }
	public Vector3 M_EndPos { get { return endPos; } set { endPos = value; } }
	public Vector3 M_StartRot { get { return startRot; } set { startRot = value; }}
	public Vector3 M_EndRot { get { return endRot; } set { endRot = value; }}

	private Transform m_Crosshair;	//クロスヘア
	public Transform M_Crosshair { get { return m_Crosshair; } set { m_Crosshair = value; }}

	private Transform muzzlePos;  //マズルの位置
	public Transform MuzzlePos { get { return muzzlePos; } set { muzzlePos = value; }}

	public virtual void Awake(){
        //コンポーネントプロパティ
        m_Animator = gameObject.GetComponent<Animator>();
		m_EnvCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();
        MuzzlePos = gameObject.transform.Find("Assault_Rifle/Pos_Muzzle");
        HoldPoseInit();
		CrosshairInit();
        Init();
	}


    public abstract void Init();
	//初期化
	public abstract void HoldPoseInit();
	public abstract void CrosshairInit();


    public abstract void HoldPoseStart();
    public abstract void HoldPoseEnd();


}
