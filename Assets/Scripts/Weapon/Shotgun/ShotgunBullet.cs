using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : ProjectileBase{
    private Ray ray;
    private RaycastHit hit;
   
    private float time = 5.0f;
    private Ray m_Ray;
    override public void Init(){
        //何も当たったものがないなら
        Invoke("DestroySelf", time);
    }

    override public void Shoot(Vector3 dir, int force, int damage, RaycastHit hit){
        m_Rigidbody.AddForce(dir * force);
        //this.hit = hit;
        this.Damage = damage;

        ray = new Ray(transform.position, dir);
        //Debug.Log("障碍物:" + hit.collider.name);

        
    }

    override public void CollisionEnter(Collision coll){
        m_Rigidbody.Sleep();

        //銃痕 生成
        if (coll.collider.GetComponent<BulletHole>() != null){
            float maxDistance = 500.0f;
            //Layer "Env": 9   射线 此处写注释
            if (Physics.Raycast(ray, out hit, maxDistance, 1 << 9)){}
            coll.collider.GetComponent<BulletHole>().CreateBulletHole(hit);
            coll.collider.GetComponent<BulletHole>().Hp -= this.Damage;
        }
        

        if (coll.collider.GetComponent<EnemyEntityController>() != null){
            float maxDistance = 500.0f;
            //Layer "Env": 9   射线 此处写注释
            if (Physics.Raycast(ray, out hit, maxDistance, 1 << 10)) { }
            coll.collider.GetComponent<EnemyEntityController>().Life -= this.Damage;
            //エフェクトの再生
            coll.collider.GetComponent<EnemyEntityController>().PlayFleshEffect(hit);
        }

        //当たった瞬間 弾をなくす
        Destroy(gameObject);
    }

    
}
