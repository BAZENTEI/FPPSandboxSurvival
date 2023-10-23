using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : ProjectileBase{
    private RaycastHit hit;
   
    private float time = 5.0f;

    public override void Init(){
        //何も当たったものがないなら
        Invoke("DestroySelf", time);
    }

    public override void Shoot(Vector3 dir, int force, int damage){
        m_Rigidbody.AddForce(dir * force);

        this.Damage = damage;
    }

    public override void CollisionEnter(Collision coll){
        m_Rigidbody.Sleep();

        //銃痕 生成
        if (coll.collider.GetComponent<BulletHole>() != null){
            coll.collider.GetComponent<BulletHole>().CreateBulletHole(hit);
            coll.collider.GetComponent<BulletHole>().Hp -= this.Damage;
        }
        //当たった瞬間 弾をなくす
        Destroy(gameObject);
    }

    private void DestroySelf(){
        Destroy(gameObject);
    }
}
