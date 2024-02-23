using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class InventoryItemController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
	private RectTransform m_RectTransform;
	private CanvasGroup m_CanvasGroup;

	private Image m_Image;  //アイコン
	private Text m_Text;    //個数
    private Image m_Bar;    //耐久ゲージ
    private bool bar = false;
    private int id;			//アイテムid
	private bool isDrag = false; //ドラッグ判定
	private bool inInventory = true; //true false
	private int num = 0;             //アイテム数

	private Transform parent;   //ドラッグ途中の親オブジェクト
	private Transform self_parent;	//親オブジェクト


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

	void Awake(){
		Init();
	}
	
	private void Init() {
		m_RectTransform = gameObject.GetComponent<RectTransform>();
		m_CanvasGroup = gameObject.GetComponent<CanvasGroup>();
		m_Image = gameObject.GetComponent<Image>();
		m_Text = m_RectTransform.Find("Number").GetComponent<Text>();
		//
        m_Bar = m_RectTransform.Find("Bar").GetComponent<Image>();
        gameObject.name = "InventoryItem";

		parent = GameObject.Find("Canvas").GetComponent<Transform>();
	}

	void Update(){
		//ドラッグ途中右クリック->ドロップ
        if (Input.GetMouseButtonDown(1) && isDrag){
			BreakMaterials();

		}
    }

	// init item
	public void InitItem(int id, string name, int num, bool bar)
    {
		this.id = id;
		m_Image.sprite = Resources.Load<Sprite>("Item/" + name);
		m_Text.text = num.ToString();
		this.num = num;
        this.bar = bar;
        BarOrNum();
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
		Drag(target);
		//リセット
		m_CanvasGroup.blocksRaycasts = true;
		m_RectTransform.localPosition = Vector3.zero;
		isDrag = false;
	}

	//ドラッグ＆ドロップ処理
	private void Drag(GameObject target){
		if (target != null)
		{
			#region 空きスロット&&それ以外
			//アイテムの空きスロットへ
			if (target.tag == "InventorySlot")
			{
				Debug.Log(target.name);
				m_RectTransform.SetParent(target.transform);
				ResetSpriteSize(m_RectTransform, 85, 85);
				inInventory = true;
			}
			else
			{
				//それ以外:位置リセット
				m_RectTransform.SetParent(self_parent);
			}
			#endregion

			#region 位置入れ替え
			if (target.tag == "InventoryItem"){
				InventoryItemController inventoryItemController = target.GetComponent<InventoryItemController>();
				if (inInventory && inventoryItemController.InInventory){
					if (Id == inventoryItemController.Id){
						MergeMaterials(inventoryItemController);
					} else {
						m_RectTransform.SetParent(target.transform.parent);
						target.transform.SetParent(self_parent);
						target.transform.localPosition = Vector3.zero;
					}
				} else {
					if (Id == inventoryItemController.Id && inventoryItemController.InInventory){
						MergeMaterials(inventoryItemController);
					}
				}
			}
			#endregion

			#region アイテムスロットエリア->クラフトリスト
			if (target.tag == "CraftingSlot"){
				//アイテムが該当のスロットに移動できる
				if (target.GetComponent<CraftingSlotController>().IsOpen){
					//クラフトリストとアイテムが一致の場合
					if (id == target.GetComponent<CraftingSlotController>().Id){
						m_RectTransform.SetParent(target.transform);
						ResetSpriteSize(m_RectTransform, 70, 62);
						inInventory = false;
					} else {
						//戻す
						target.transform.SetParent(self_parent);
					}
				} else {
					target.transform.SetParent(self_parent);
				}
			}
			#endregion
		}
		else {
			//UIエリア外:位置リセット
			m_RectTransform.SetParent(self_parent);
			//m_RectTransform.localPosition = Vector3.zero;
		}
		

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
		Destroy(gameObject);
    }

    public void UpdateUI(float value){
        if (value <= 0){
            gameObject.GetComponent<Transform>().parent.GetComponent<GearBarSlotController>().Normal();
            Destroy(gameObject);
        }
        m_Bar.fillAmount = value;
    }

    private void BarOrNum()
    {
        if (bar == false)
        {
            m_Bar.gameObject.SetActive(false);
        }
        else
        {
            m_Text.gameObject.SetActive(false);
        }
    }
}
