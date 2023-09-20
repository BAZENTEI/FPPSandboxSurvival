using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunController : WeaponControllerBase
{
    private ShotgunView m_ShotgunView;
    private const int pelletCapactiy = 6;   //散弾の粒数
    public override void Init(){
        m_ShotgunView = (ShotgunView)M_WeaponViewBase;
    }

    public override void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Shotgun_Fire");
        Effect = Resources.Load<GameObject>("Effect/Weapon/Shotgun_Muzzle_Effect");
    }

    public override void PlayFireAnimation(){
        GameObject tempShell = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Shell, m_ShotgunView.M_EjectionPos.position, Quaternion.identity);
        tempShell.GetComponent<Rigidbody>().AddForce(m_ShotgunView.M_EjectionPos.up * 70.0f);
        StartCoroutine(FireEffectDestory(tempShell, 6.0f));

    }

    public override void PlayFireEffect(){
        GameObject temp = GameObject.Instantiate<GameObject>(Effect, M_WeaponViewBase.M_MuzzlePos.position, Quaternion.identity);
        temp.GetComponent<ParticleSystem>().Play();
        StartCoroutine(FireEffectDestory(temp, 3.0f));
    }

    public override void Shoot(){
        StartCoroutine("CreateBullet");
    }

    private IEnumerator CreateBullet() {
        for(int i = 0;i < pelletCapactiy; i++){
            Vector3 offset = new Vector3(Random.Range(-0.03f, 0.03f), Random.Range(-0.03f, 0.03f), 0);

            GameObject tempBullet = GameObject.Instantiate<GameObject>(m_ShotgunView.Prefab_Bullet, m_ShotgunView.M_MuzzlePos.position, Quaternion.identity);
            tempBullet.GetComponent<ShotgunBullet>().Shoot(2000, m_ShotgunView.M_MuzzlePos.forward + offset );
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
