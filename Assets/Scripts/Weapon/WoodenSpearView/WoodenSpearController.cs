using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpearController : WeaponControllerBase
{
    private WoodenSpearView m_WoodenSpearView;
    public override void Init(){
        m_WoodenSpearView = (WoodenSpearView)M_WeaponViewBase;
        ReadyToFire(0);

    }

    public override void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow_Release");

    }

    public override void Shoot(){
    }

    public override void PlayFireEffect(){
        //エフェクトなし
    }

    public override void PlayFireAnimation(){
        //アニメーションなし

    }

}
