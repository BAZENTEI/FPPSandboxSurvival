using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBowView : WeaponViewBase {
    private GameObject arrow;
    public GameObject M_Arrow { get { return arrow; } }
    protected override void Init(){
    
        M_MuzzlePos = gameObject.transform.Find("Armature/Arm_L/Forearm_L/Wrist_L/Weapon/Pos_Muzzle");
        arrow = Resources.Load<GameObject>("Weapon/Arrow");
    }

    protected override void HoldPoseInit(){
        M_StartPos = gameObject.transform.localPosition;
        M_StartRot = gameObject.transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(0.75f, -1.2f, 0.22f);
        M_EndRot = new Vector3(2.5f, -8f, 35.0f);
    }

    public override void HoldPoseStart(){
        transform.DOLocalMove(M_EndPos, 0.2f);
        transform.DOLocalRotate(M_EndRot, 0.2f);
        M_EnvCamera.DOFieldOfView(40, 0.2f);
    }

    public override void HoldPoseEnd(){
        transform.DOLocalMove(M_StartPos, 0.2f);
        transform.DOLocalRotate(M_StartRot, 0.2f);

        M_EnvCamera.DOFieldOfView(60, 0.2f);
    }
    protected override void CrosshairInit(){
        //仕様:クロスヘアなし
        M_Crosshair = GameObject.Find("Crosshair").gameObject.transform;
    }
}
