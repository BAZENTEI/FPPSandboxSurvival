using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private RectTransform m_RectTransform;
	private CanvasGroup m_CanvasGroup;
	private Image m_Image;  //アイコン
	private Text m_Text;	//個数
	private int id;			
	private bool isDrag = false; //ドラッグ判定
	private bool inInventory = true; //
	private int num = 0;
	public int Num {
		get { return num; }
		set { num = value;
			  m_Text.text = num.ToString();	
		}
	}
	public int Id { get { return id; } set { id = value; } }
	public bool InInventory { get { return inInventory; }
		set {
			  inInventory = value;
			  m_RectTransform.localPosition = Vector3.zero;
			  ResetSpriteSize(m_RectTransform, 85, 85);
		} 
	}

	private Transform parent;
	private Transform self_parent;

	void Awake(){
		m_RectTransform = gameObject.GetComponent<RectTransform>();
		m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
		m_Image = gameObject.GetComponent<Image>();
		m_Text = m_RectTransform.Find("Number").GetComponent<Text>();
		gameObject.name = "InventoryItem";

		parent = GameObject.Find("InventoryPanel").GetComponent<Transform>();
	}
	
	void Update(){
        if (Input.GetMouseButtonDown(1) && isDrag){
			BreakMaterials();

		}
    }

	// init item
	public void InitItem(int id, string name, int num){
		this.id = id;
		m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
		m_Text.text = num.ToString();
		this.num = num;
	}

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData){
		self_parent = m_RectTransform.parent;
		m_RectTransform.SetParent(parent);
		m_CanvasGroup.blocksRaycasts = false;
		isDrag = true;
	}

    void IDragHandler.OnDrag(PointerEventData eventData){
		Vector3 pos;
		RectTransformUtility.ScreenPointToWorldPointInRectangle(m_RectTransform, eventData.position, eventData.enterEventCamera, out pos);
		m_RectTransform.position = pos;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData){
		GameObject target = eventData.pointerEnter;

		if(target != null){
			//アイテムの空きスロットへ
			if (target.tag == "InventorySlot"){
				Debug.Log(target.name);
				m_RectTransform.SetParent(target.transform);
				ResetSpriteSize(m_RectTransform, 85, 85);
				inInventory = true;
			} else{
				//それ以外:位置リセット
				m_RectTransform.SetParent(self_parent);
			}
			
			//位置入れ替え
			if (target.tag == "InventoryItem"){
                if (inInventory && target.GetComponent<InventoryItemController>().InInventory){
					if(Id == target.GetComponent<InventoryItemController>().Id){
						MergeMaterials(target.GetComponent<InventoryItemController>());		
                    } 
                    else{
						m_RectTransform.SetParent(target.transform.parent);
						target.transform.SetParent(self_parent);
						target.transform.localPosition = Vector3.zero;
					}
					
                }else{
					if (Id == target.GetComponent<InventoryItemController>().Id && target.GetComponent<InventoryItemController>().InInventory)
					{
						MergeMaterials(target.GetComponent<InventoryItemController>());
					}
				}

			}
			//アイテムスロットエリア->クラフトリスト
			if (target.tag == "CraftingSlot"){
				//アイテムが該当のスロットに移動できる
				if(target.GetComponent<CraftingSlotController>().IsOpen){
					//クラフトリストとアイテムが一致の場合
                    if (id == target.GetComponent<CraftingSlotController>().Id) {
						m_RectTransform.SetParent(target.transform);
						ResetSpriteSize(m_RectTransform, 70, 62);
						inInventory = false;
					}else {
						//戻す
						target.transform.SetParent(self_parent);
					}
                } else{	
					target.transform.SetParent(self_parent);
				}
			}
		}
        else{	
			//UIエリア外:位置リセット
			m_RectTransform.SetParent(self_parent);
			//m_RectTransform.localPosition = Vector3.zero;
		}

		//リセット
		m_CanvasGroup.blocksRaycasts = true;
		m_RectTransform.localPosition = Vector3.zero;
		isDrag = false;
	}

	//アイテムテクスチャーのサイズをリセット
	private void ResetSpriteSize(RectTransform rectTransform, float width,float height)  {
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

	}

	/// <summary>
	/// 
	/// </summary>
	private void BreakMaterials(){
		Debug.Log("BreakMaterials");
		GameObject temp = GameObject.Instantiate<GameObject>(gameObject);
		RectTransform tempTransform = temp.GetComponent<RectTransform>();
		//リセット
		tempTransform.SetParent(self_parent);
		tempTransform.localPosition = Vector3.zero;
		tempTransform.localScale = Vector3.one;

		//
		int tempSum = num;  //合計
		int tempNum = tempSum / 2;
		int tempNumA = tempSum - tempNum;
		//
		temp.GetComponent<InventoryItemController>().Num = tempNum;
		Num = tempNumA;
		//
		temp.GetComponent<CanvasGroup>().blocksRaycasts = true;
		//
		temp.GetComponent<InventoryItemController>().Id = Id;
	}

	//
	private void MergeMaterials(InventoryItemController target) {
		target.Num = target.Num + Num; //
		GameObject.Destroy(gameObject);
    }


}
