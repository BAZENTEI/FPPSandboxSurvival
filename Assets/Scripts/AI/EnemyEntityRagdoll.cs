using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEntityRagdoll : MonoBehaviour {
    private BoxCollider m_Collider_Up;
    private BoxCollider m_Collider_Down;

    void Start (){
        m_Collider_Up = transform.Find("Armature").GetComponent<BoxCollider>();
        m_Collider_Down = transform.Find("Armature/Hips/Middle_Spine").GetComponent<BoxCollider>();

    }

    public void StartRagdoll(){
        m_Collider_Up.enabled = false;
        m_Collider_Down.enabled = false;
    }

}
