using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBowController : WeaponControllerBase{
    private WoodenBowView m_WoodenBowView;
    public override void Init(){
        m_WoodenBowView = (WoodenBowView)M_WeaponViewBase;
        ReadyToFire(0);
    }

    public override void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow_Release");
    }

    public override void Shoot(){
        GameObject arrow = Instantiate<GameObject>(m_WoodenBowView.M_Arrow, m_WoodenBowView.M_MuzzlePos.transform.position,
            m_WoodenBowView.M_MuzzlePos.rotation);
        arrow.GetComponent<ArrowContorller>().Shoot(m_WoodenBowView.M_MuzzlePos.transform.forward, 1000, 100);
    }

    public override void PlayFireEffect(){
        //エフェクトなし
    }

    public override void PlayFireAnimation(){
        //アニメーションなし
    }



}
