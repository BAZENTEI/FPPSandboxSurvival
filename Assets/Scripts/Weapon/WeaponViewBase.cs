using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///
public abstract class WeaponViewBase : MonoBehaviour {

	//コンポーネント参照
	private Animator m_Animator;
	private Camera m_EnvCamera;

	private Vector3 startPos;   //照準のアニメーション
	private Vector3 endPos;
	private Vector3 startRot;
	private Vector3 endRot;

	private Transform m_Crosshair;  //クロスヘア
	private Transform muzzlePos;  //マズルの位置

	public Animator M_Animator { get { return m_Animator; }}
	public Camera M_EnvCamera { get { return m_EnvCamera; }}
	public Vector3 M_StartPos { get { return startPos; } set { startPos = value; }}
	public Vector3 M_EndPos { get { return endPos; } set { endPos = value; }}
	public Vector3 M_StartRot { get { return startRot; } set { startRot = value; }}
	public Vector3 M_EndRot { get { return endRot; } set { endRot = value; }}
	public Transform M_Crosshair { get { return m_Crosshair; } set { m_Crosshair = value; }}
    public Transform MuzzlePos { get { return muzzlePos; } set { muzzlePos = value; }}

    public virtual void Awake(){
		//コンポーネントプロパティの初期化
		m_Animator = gameObject.GetComponent<Animator>();
		m_EnvCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();

        HoldPoseInit();
		CrosshairInit();
        Init();
	}

	//子グラスの初期化
	public abstract void Init();

	//照準の初期化
	public abstract void HoldPoseInit();
	//照準のアニメーション
	public abstract void HoldPoseStart();
	//照準(解除)のアニメーション
	public abstract void HoldPoseEnd();

	//クロスヘアの初期化
	public abstract void CrosshairInit();

}
