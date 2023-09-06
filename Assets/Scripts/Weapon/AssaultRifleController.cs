using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AssaultRifleController : WeaponControllerBase{
	private AssaultRifleView m_AssaultRifleView;
	private ObjectPool []objectPools;	//オブジェクトプール
   
	override public void Init(){
        m_AssaultRifleView = (AssaultRifleView)M_WeaponViewBase;

        objectPools = gameObject.GetComponents<ObjectPool>();

	}

    override public void LoadAsset(){
        AudioClip = Resources.Load<AudioClip>("Audio/Weapon/Drum_gun");
        Effect = Resources.Load<GameObject>("Effect/Muzzle/AssaultRifle_GunPoint_Effect");
    }

    override public void Shoot(){
        if (Hit.point != Vector3.zero){
            Debug.Log("玉あり");
            //的に生成
            Instantiate(m_AssaultRifleView.Prefab_Bullet, Hit.point, Quaternion.identity);
        }
        else{
            Debug.Log("玉なし");
        }
        Durable--;
    }

	//発射のエフェクト
	//発射炎
	override public void PlayFireEffect(){
		GameObject fire = null;

		if(objectPools[0].isEmpty()){
			//非アクティブなオブジェクトがない場合新規生成
			fire = GameObject.Instantiate<GameObject>(Effect, m_AssaultRifleView.MuzzlePos.position, Quaternion.identity,m_AssaultRifleView.M_FireParent);
			fire.name = "Fire";
		}else{
			//オブジェクト再利用
			fire = objectPools[0].GetObject();
			fire.transform.position = m_AssaultRifleView.MuzzlePos.position; 
		}
	
		fire.GetComponent<ParticleSystem>().Play();
		StartCoroutine(Delay(objectPools[0], fire, 1.0f));
		
	}

	//発射のアニメーション
	override public void PlayFireAnimation()
	{
		GameObject shell = null;
		if(objectPools[1].isEmpty()){
			//非アクティブなオブジェクトがない場合新規生成
			shell =  GameObject.Instantiate<GameObject>(m_AssaultRifleView.Prefab_Shell, m_AssaultRifleView.M_EjectionPos.position, Quaternion.identity, m_AssaultRifleView.M_ShellParent);
			shell.name = "Shell";
		}else{
			//オブジェクト再利用
			shell = objectPools[1].GetObject();
			shell.GetComponent<Rigidbody>().isKinematic = true;
			shell.transform.position = m_AssaultRifleView.M_EjectionPos.position; 
			shell.GetComponent<Rigidbody>().isKinematic = false;
		}

		Rigidbody shellRigidbody = shell.GetComponent<Rigidbody>();
		//エジェクションポートから空薬莢が弾き出される
		shellRigidbody.AddForce(m_AssaultRifleView.M_EjectionPos.up * Random.Range(60, 70));
		StartCoroutine(Delay(objectPools[1], shell, 3.0f));
	}


    void Update()
    {
        //Debug.DrawLine(m_AssaultRifleView.M_EjectionPos.position, m_AssaultRifleView.M_EjectionPos.up * 1000, Color.cyan);

    }

}
