using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : GunControllerBase{
    private ShotgunView m_ShotgunView;
    private const int pelletCapactiy = 6;   //散弾の粒数
    protected override void Init(){
        m_ShotgunView = (ShotgunView)M_WeaponViewBase;
    }

    protected override void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Shotgun_Fire");
        Effect = Resources.Load<GameObject>("Effect/Weapon/Shotgun_Muzzle_Effect");
    }

    protected override void PlayFireAnimation(){
        GameObject tempShell = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Shell, m_ShotgunView.M_EjectionPos.position, Quaternion.identity);
        tempShell.GetComponent<Rigidbody>().AddForce(m_ShotgunView.M_EjectionPos.up * 70.0f);
        StartCoroutine(FireEffectDestory(tempShell, 6.0f));

    }

    protected override void PlayFireEffect(){
        GameObject temp = GameObject.Instantiate<GameObject>(Effect, M_WeaponViewBase.M_MuzzlePos.position, Quaternion.identity);
        temp.GetComponent<ParticleSystem>().Play();
        StartCoroutine(FireEffectDestory(temp, 3.0f));
    }

    protected override void Shoot(){
        if (Hit.collider.GetComponent<BulletHole>() != null){
            Hit.collider.GetComponent<BulletHole>().CreateBulletHole(Hit);
        }else{
            //的に生成
            Instantiate(m_ShotgunView.Prefab_Bullet, Hit.point, Quaternion.identity);
        }
        StartCoroutine("CreateBullet");
    }

    private IEnumerator CreateBullet() {
        for(int i = 0;i < pelletCapactiy; i++){
            Vector3 offset = new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f), 0);

            GameObject tempBullet = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Bullet, m_ShotgunView.M_MuzzlePos.position, Quaternion.identity);
            tempBullet.GetComponent<ShotgunBullet>().Shoot(m_ShotgunView.M_MuzzlePos.forward + offset, 2000, 10);
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator FireEffectDestory(GameObject go,float time){
        yield return new WaitForSeconds(time);
        GameObject.Destroy(go);
    }

    private void PlayPumpAudio(int state){
        AudioSource.PlayClipAtPoint(m_ShotgunView.M_EffectAudio, m_ShotgunView.M_EjectionPos.position);
    }
}
