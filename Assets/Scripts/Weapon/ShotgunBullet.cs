using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : ProjectileBase{

    public override void Init(){
        
    }

    public override void Shoot(Vector3 dir, int force, int damage){
        m_Rigidbody.AddForce(dir * force);
    }

    public override void CollisionEnter(Collision coll){
        m_Rigidbody.Sleep();
    }
}
