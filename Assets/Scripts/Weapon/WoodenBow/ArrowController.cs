using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : ProjectileBase{
	private BoxCollider m_BoxCollider;

    private Transform m_Pivot;
    private RaycastHit hit;

    public override void Init(){
        m_BoxCollider = GetComponent<BoxCollider>();

        m_Pivot = transform.Find("Pivot");
    }

    public override void Shoot(Vector3 dir, int force, int damage, RaycastHit hit){
        m_Rigidbody.AddForce(dir * force);
        this.hit = hit;
        this.Damage = damage;
        this.Damage = damage;


    }

    public override void CollisionEnter(Collision coll){
        Debug.Log(name + ":あたり");
     
        if (coll.collider.gameObject.layer == LayerMask.NameToLayer("Env")){
            Destroy(m_Rigidbody);
            Destroy(m_BoxCollider);

            //矢が刺さったものに付く
            transform.SetParent(coll.collider.gameObject.transform);
            //ダメージ計算
            Debug.Log(coll.collider.GetComponent<BulletHole>().Hp + ":"+ Damage);
            coll.collider.GetComponent<BulletHole>().Hp -= Damage;
            StartCoroutine("VibrationAnimation");
        }else if (coll.collider.gameObject.layer == LayerMask.NameToLayer("EnemyEntity")){
            Destroy(m_Rigidbody);
            Destroy(m_BoxCollider);
            //矢が刺さったものに付く
            transform.SetParent(coll.collider.gameObject.transform);
            //ダメージ計算
            coll.collider.GetComponentInParent<EnemyEntityController>().Life -= Damage;
            //エフェクト
            coll.collider.GetComponentInParent<EnemyEntityController>().PlayFleshEffect(hit);
            StartCoroutine("VibrationAnimation");
        }
        else{
            Destroy(m_Rigidbody);
            Destroy(m_BoxCollider);
        }

    }

    /// <summary>
    /// 矢の振れ
    /// </summary>
    /// <returns></returns>
    private IEnumerator VibrationAnimation(){
        float stopTime = Time.time + 0.8f;  //総時間
        float range = 1.0f;
        float vel = 0;
        Quaternion startRot = Quaternion.Euler(new Vector3(Random.Range(-8.0f, 8.0f), Random.Range(-8.0f, 8.0f), 0.0f));
        while (Time.time < stopTime){
            //減衰振動
            m_Pivot.localRotation = Quaternion.Euler(new Vector3(Random.Range(-range, range), Random.Range(-range, range), 0.0f)) * startRot;
            range = Mathf.SmoothDamp(range, 0.0f, ref vel, stopTime - Time.time);
            yield return null;
        }

    }
}
