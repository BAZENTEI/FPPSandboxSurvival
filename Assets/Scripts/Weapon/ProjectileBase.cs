using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///親クラス 
/// </summary>
public abstract class ProjectileBase : MonoBehaviour {
    protected Rigidbody m_Rigidbody;
    public Rigidbody M_Rigidbody { get { return m_Rigidbody;} }

    void Awake(){
        m_Rigidbody = GetComponent<Rigidbody>();
        Init();
    }

    void OnCollisionEnter(Collision coll){
        CollisionEnter(coll);
    }

    public abstract void Init();
    public abstract void Shoot(Vector3 dir, int force, int damage);
    public abstract void CollisionEnter(Collision coll);

   


}
