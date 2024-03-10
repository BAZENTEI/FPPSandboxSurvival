using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenBowController : RangedWeaponControllerBase{
    private WoodenBowView m_WoodenBowView;
    protected override void Init(){
        m_WoodenBowView = (WoodenBowView)M_WeaponViewBase;
        ReadyToFire(0);
    }

    protected override void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow_Release");
    }

    protected override void Shoot(){
      
       
      
        GameObject arrow = Instantiate<GameObject>(m_WoodenBowView.M_Arrow, m_WoodenBowView.M_MuzzlePos.transform.position,
            m_WoodenBowView.M_MuzzlePos.rotation);
        Debug.Log(arrow);
        arrow.GetComponent<ArrowController>().Shoot(m_WoodenBowView.M_MuzzlePos.transform.forward, 1000, Damage, Hit);
        Durable--;
    }




}
