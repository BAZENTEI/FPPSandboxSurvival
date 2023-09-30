using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpearView : WeaponViewBase {
    
    private GameObject preab_Spear;
    public GameObject M_Preab_Spear { get { return preab_Spear; } }


    public override void Init(){
        M_MuzzlePos = transform.Find("Armature/Arm_R/Forearm_R/Wrist_R/Weapon/Pos_Muzzle");
        preab_Spear = Resources.Load<GameObject>("Weapon/Wooden_Spear");
    }

    public override void CrosshairInit(){
        M_Crosshair = GameObject.Find("Crosshair").gameObject.transform;
    }

    public override void HoldPoseInit(){
        M_StartPos = transform.localPosition;
        M_StartRot = transform.localRotation.eulerAngles;
        M_EndPos = new Vector3(0.0f, -1.55f, 0.32f);
        M_EndRot = new Vector3(0f, 3.9f, 0.3f);
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


}
