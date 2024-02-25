using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneHatchet : MonoBehaviour {

    private Animator m_Animator;
    [SerializeField] private int id;
    [SerializeField] private WeaponType weaponType;  //銃の種類
    [SerializeField] private int damage;     //基礎攻撃力
    [SerializeField] private int durable;    //最大耐久値
    //
    private float durable_2;
    private GameObject toolBarIcon;

    public int Id { get { return id; } set { id = value; } }
    public WeaponType WeaponType { get { return weaponType; } set { weaponType = value; } }
    public int Damage { get { return damage; } set { damage = value; } }
    public int Durable
    {
        get { return durable; }
        set
        {
            durable = value;
            if (durable < 0)
            {
               // Destroy(gameObject);
            }
        }
    }
    public GameObject ToolBarIcon { get { return toolBarIcon; } set { toolBarIcon = value; } }

    private Ray ray;
    private RaycastHit hit;

    void Start () {
        durable_2 = Durable;
        m_Animator = gameObject.GetComponent<Animator>();
    }
	
	
	void Update () {
        AttackPre();
        if (Input.GetMouseButtonDown(0)){
            Attack();
        }
    }

    public void Holster(){
        m_Animator.SetTrigger("Holster");
    }

    private void UpdateUI(){
        
        //toolBarIcon.GetComponent<InventoryItemController>().UpdateUI(Durable / durable_2);
    }

    private void Attack(){
        m_Animator.SetTrigger("Hit");
        Durable--;
        UpdateUI();
    }

    private void AttackPre(){
        ray = new Ray(transform.Find("Point").position, transform.Find("Point").forward);
        if (Physics.Raycast(ray, out hit, 2)){
            

        }
    }

    //アニメーション呼び出し
    private void AttackStone(){
        if (hit.collider != null && hit.collider.tag == "Stone"){
            hit.collider.GetComponent<BulletHole>().HatchetHit(hit, Damage);
        }
    }
}
