using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private RectTransform m_RectTransform;
	private CanvasGroup m_CanvasGroup;
	private Image m_Image;
	private Text m_Text;
	private int id;

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
	
	// init item
	public void InitItem(int id, string name, int num){
		this.id = id;
		m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
		m_Text.text = num.ToString();
	}

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData){
		self_parent = m_RectTransform.parent;
		m_RectTransform.SetParent(parent);
		m_CanvasGroup.blocksRaycasts = false;
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
			} else{
				//それ以外:位置リセット
				m_RectTransform.SetParent(self_parent);
			}
			
			//位置入れ替え
			if (target.tag == "InventoryItem"){
				m_RectTransform.SetParent(target.transform.parent);
				target.transform.SetParent(self_parent);
				target.transform.localPosition = Vector3.zero;
			}
			//アイテムスロットエリア->クラフトリスト
			if (target.tag == "CraftingSlot"){
				//アイテムが該当のスロットに移動できる
				if(target.GetComponent<CraftingSlotController>().IsOpen){
					//クラフトリストとアイテムが一致の場合
                    if (id == target.GetComponent<CraftingSlotController>().Id) {
						m_RectTransform.SetParent(target.transform);
						ResetSpriteSize(m_RectTransform, 70, 62);
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
	}

	//アイテムテクスチャーのサイズをリセット
	private void ResetSpriteSize(RectTransform rectTransform, float width,float height)  {
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
		rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);

	}
}
