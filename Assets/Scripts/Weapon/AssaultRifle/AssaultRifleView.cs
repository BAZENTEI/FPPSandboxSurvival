using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssaultRifleView : WeaponViewBase {

	private GameObject prefab_Bullet;   //弾
	private GameObject prefab_Shell;    //薬莢
	private Transform ejectionPos; //エジェクションポートの位置

	private Transform fireParent;
	private Transform shellParent;

	public GameObject Prefab_Bullet { get { return prefab_Bullet; }}
	public GameObject Prefab_Shell { get { return prefab_Shell; }}
	public Transform M_EjectionPos { get { return ejectionPos; }}

	public Transform M_FireParent { get { return fireParent; }}
	public Transform M_ShellParent { get { return shellParent; }}


    override protected void Init(){
        M_MuzzlePos = gameObject.transform.Find("Assault_Rifle/Pos_Muzzle");

        prefab_Bullet = Resources.Load<GameObject>("Weapon/Bullet");
		prefab_Shell = Resources.Load<GameObject>("Animation/Ejection/Shell");
		ejectionPos = gameObject.transform.Find("Assault_Rifle/Pos_Ejection");

		fireParent = GameObject.Find("ObjectPool/Fire_Parent").transform;
        shellParent = GameObject.Find("ObjectPool/Shell_Parent").transform;
    }

	override protected void HoldPoseInit(){
		M_StartPos = gameObject.transform.localPosition;
		M_StartRot = gameObject.transform.localRotation.eulerAngles;
		M_EndPos = new Vector3(-0.065f, -1.85f, 0.25f);
		M_EndRot = new Vector3(2.8f, 1.3f, 0.08f);
	}

	override public void HoldPoseStart(){
		gameObject.transform.DOLocalMove(M_EndPos, 0.2f);
		gameObject.transform.DOLocalRotate(M_EndRot, 0.2f);
		M_EnvCamera.DOFieldOfView(40, 0.2f);
	}

    override public void HoldPoseEnd(){
		gameObject.transform.DOLocalMove(M_StartPos, 0.2f);
		gameObject.transform.DOLocalRotate(M_StartRot, 0.2f);

		M_EnvCamera.DOFieldOfView(60, 0.2f);

	}

	override protected void CrosshairInit(){
		M_Crosshair = GameObject.Find("Crosshair").gameObject.transform;
	}
}
