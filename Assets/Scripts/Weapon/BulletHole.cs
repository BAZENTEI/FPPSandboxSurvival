using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 銃痕
/// </summary>
[RequireComponent(typeof(ObjectPool))]
public class BulletHole : MonoBehaviour {
	private Texture2D m_BulletHole;           //銃痕のテクスチャー
	private Texture2D m_BulletStrikeTexture;  //当たったもののテクスチャー
	private Texture2D m_BulletStrikeBackup_1;   //元のテクスチャー
    private Texture2D m_BulletStrikeBackup_2;
    [SerializeField] private MaterialType materialType; //当たったもののマテリアルタイプ
	private Queue<Vector2> hitUVQueue = null;			//当たった場所のUV座標キュー

	private GameObject prefab_hitEffect; //銃痕のエフェクト
	private Transform effectParent;	     //エフェクトを一括管理のオブジェクト
	private ObjectPool effectObejctPool; //銃痕エフェクトのオブジェクトプール

    [SerializeField] private int hp;        //テスト      
    public int Hp{
        get { return hp; }
        set{
            hp = value;
            if (hp <= 0){
				Invoke("DestorySelf", 0.5f);
            }
        }
    }

	private void DestroySelf(){
        Destroy(gameObject);

    }

    void Start () {
		switch (materialType){
			case MaterialType.Wood:
				ResourcesLoad("Bullet Decal_Wood", "Bullet Impact FX_Wood", "Effect_Parent");
				break;
			case MaterialType.Marble:
			case MaterialType.Stone:
				ResourcesLoad("Bullet Decal_Stone", "Bullet Impact FX_Stone", "Effect_Parent");
				break;
			case MaterialType.Metal:
				prefab_hitEffect = Resources.Load<GameObject>("Weapon/Effects/Bullet Impact FX_Metal");
				ResourcesLoad("Bullet Decal_Metal", "Bullet Impact FX_Metal", "Effect_Parent");
				break;
			default:
				ResourcesLoad("Bullet Decal_Wood", "Bullet Impact FX_Wood", "Effect_Parent");
				break;
		}
		if(GetComponent<ObjectPool>() == null){
			effectObejctPool = gameObject.AddComponent<ObjectPool>();
        }else{
			effectObejctPool = GetComponent<ObjectPool>();
		}

		m_BulletStrikeTexture = (Texture2D)gameObject.GetComponent<MeshRenderer>().material.mainTexture;
		m_BulletStrikeBackup_1 = Instantiate<Texture2D>(m_BulletStrikeTexture);
        m_BulletStrikeBackup_2 = Instantiate<Texture2D>(m_BulletStrikeTexture);
        gameObject.GetComponent<MeshRenderer>().material.mainTexture = m_BulletStrikeBackup_1;

        hitUVQueue = new Queue<Vector2>();
	}

	private void ResourcesLoad(string bulletMark, string effect, string parent){
		m_BulletHole = Resources.Load<Texture2D>("Weapon/BulletHoles/" + bulletMark);
		prefab_hitEffect = Resources.Load<GameObject>("Weapon/BulletHoles/Effects/" + effect);
		effectParent = GameObject.Find("ObjectPool/" + parent).transform;
	}

	/// <summary>
	/// 銃痕の生成
	/// </summary>
	public void CreateBulletHole(RaycastHit hit){
		Vector2 uv = hit.textureCoord;	//撃たれた場所のUV座標
		hitUVQueue.Enqueue(uv);

		//横:x軸 縦:y軸
		for (int i = 0; i < m_BulletHole.width; i++){
            for (int j = 0; j < m_BulletHole.height; j++){
				float x = uv.x * m_BulletStrikeTexture.width - m_BulletHole.width / 2 + i;
				float y = uv.y * m_BulletStrikeTexture.height - m_BulletHole.height / 2 + j;
				//銃痕のカラー
				Color color = m_BulletHole.GetPixel(i, j);
				//カラーの融合
				if (color.a >= 0.3f){
                    m_BulletStrikeBackup_1.SetPixel((int)x, (int)y, color);

				}
			}
		}
        m_BulletStrikeBackup_1.Apply();
		PlayEffect(hit);
		//銃痕を取り除く
		Invoke("RemoveBulletHole", 3.0f);

	}

	/// <summary>
	/// 銃痕を取り除く
	/// </summary>
	private void RemoveBulletHole(){
		if(hitUVQueue.Count > 0){
			Vector2 uv = hitUVQueue.Dequeue();
			for (int i = 0; i < m_BulletHole.width; i++){
				for (int j = 0; j < m_BulletHole.height; j++){
					float x = uv.x * m_BulletStrikeTexture.width - m_BulletHole.width / 2 + i;
					float y = uv.y * m_BulletStrikeTexture.height - m_BulletHole.height / 2 + j;

					Color color = m_BulletStrikeBackup_2.GetPixel((int)x, (int)y);
                    m_BulletStrikeBackup_1.SetPixel((int)x, (int)y, color);
					
				}
				m_BulletStrikeTexture.Apply();
		
			}
		}

	}

	/// <summary>
	/// 銃痕のエフェクト
	/// </summary>
	private void PlayEffect(RaycastHit hit){
		GameObject effect = null;

		if (!effectObejctPool.isEmpty()){
			//既存を再利用
			effect = effectObejctPool.GetObject();
			effect.transform.position = hit.point;
			effect.transform.rotation = Quaternion.LookRotation(hit.normal);

		}
		else{
            //新規
            Debug.Log("PlayEffect: " + prefab_hitEffect + ", " + effectParent);
		
			effect = Instantiate<GameObject>(prefab_hitEffect, hit.point, Quaternion.LookRotation(hit.normal), effectParent);
			effect.name = "Effect_" + materialType;

		}

		StartCoroutine(Delay(effect, 1.0f));
	}

	private IEnumerator Delay(GameObject gameObject, float time){
		yield return new WaitForSeconds(time);
		effectObejctPool.AddObject(gameObject);
    }

    private void PlayAudios(RaycastHit hit){
       
    }

    public void HatchetHit(RaycastHit hit, int hpValue){
        PlayAudios(hit);
        PlayEffect(hit);
        Hp -= hpValue;
    }
}
