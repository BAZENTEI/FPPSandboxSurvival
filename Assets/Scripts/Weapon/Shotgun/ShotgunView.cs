using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ShotgunView : WeaponViewBase{

    private GameObject prefab_Bullet;   //弾
    private GameObject prefab_Shell;    //薬莢
    private Transform ejectionPos;      //エジェクションポートの位置
    private AudioClip effectAudio;

    public GameObject Prefab_Shell { get { return prefab_Shell; } }
    public GameObject Prefab_Bullet { get { return prefab_Bullet; } }
    public Transform M_EjectionPos { get { return ejectionPos; } }
    public AudioClip M_EffectAudio { get { return effectAudio; } }

    protected override void Init(){
        M_MuzzlePos = gameObject.transform.Find("Armature/Weapon/Pos_Muzzle");
        ejectionPos = gameObject.transform.Find("Armature/Weapon/Pos_Ejection");
        effectAudio = Resources.Load<AudioClip>("Aduio/Weapon/Shotgun_Pump");
        prefab_Shell = Resources.Load<GameObject>("Animation/Ejection/Shotgun_Shell");
        prefab_Bullet = Resources.Load<GameObject>("Animation/Ejection/Shotgun_Bullet");
    }

    protected override void CrosshairInit(){
        M_Crosshair = GameObject.Find("Crosshair").gameObject.transform;
    }

    protected override void HoldPoseInit(){
        M_StartPos = gameObject.transform.localPosition;
        M_StartRot = gameObject.transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(-0.125f, -1.78f, 0.03f);
        M_EndRot = new Vector3(0.0f, 0.0f, 0.0f);
    }

    public override void HoldPoseStart(){
        gameObject.transform.DOLocalMove(M_EndPos, 0.2f);
        gameObject.transform.DOLocalRotate(M_EndRot, 0.2f);
        M_EnvCamera.DOFieldOfView(40, 0.2f);
    }

    public override void HoldPoseEnd(){
        gameObject.transform.DOLocalMove(M_StartPos, 0.2f);
        gameObject.transform.DOLocalRotate(M_StartRot, 0.2f);

        M_EnvCamera.DOFieldOfView(60, 0.2f);
    }

}
