using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenSpearController : WeaponControllerBase{
    private WoodenSpearView m_WoodenSpearView;
    protected override void Init(){
        m_WoodenSpearView = (WoodenSpearView)M_WeaponViewBase;
        ReadyToFire(0);

    }

    protected override void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Arrow_Release");

    }

    protected override void Shoot(){
        GameObject go = Instantiate<GameObject>(m_WoodenSpearView.M_Preab_Spear, m_WoodenSpearView.M_MuzzlePos.position, m_WoodenSpearView.M_MuzzlePos.rotation);
        go.GetComponent<ArrowContorller>().Shoot(m_WoodenSpearView.M_MuzzlePos.forward, 2000, Damage);
    }


}
