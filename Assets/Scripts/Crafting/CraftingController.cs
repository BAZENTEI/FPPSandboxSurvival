using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingController : MonoBehaviour {

	private Transform m_Transform;
	private Image m_Image;
	private Button m_Craft_Btn;
	private Button m_CraftAll_Btn;
    private Transform bg_Transform;

    private int tempId;
    private string tempSpriteName;

    private GameObject prefab_InventoryItem;
    public GameObject Prefab_InventoryItem { set { prefab_InventoryItem = value; } }

    void Awake () {
		m_Transform = gameObject.transform;
		m_Image = m_Transform.Find("GoodItem/ItemImage").GetComponent<Image>();
        m_Craft_Btn = m_Transform.Find("Craft").GetComponent<Button>();
        m_CraftAll_Btn = m_Transform.Find("CraftAll").GetComponent<Button>();
        bg_Transform = m_Transform.Find("GoodItem").GetComponent<Transform>();

        m_Craft_Btn.onClick.AddListener(CraftingItem);
        m_Image.gameObject.SetActive(false);
        InitButton();
    }
	
	public void Init (int id, string fileName) {
		m_Image.gameObject.SetActive(true);
		m_Image.sprite = Resources.Load<Sprite>("Item/" + fileName);
        tempId = id;
        tempSpriteName = fileName;
    }

    private void InitButton()
    {
        m_Craft_Btn.interactable = false;
        m_Craft_Btn.transform.Find("Text").GetComponent<Text>().color = Color.black;

        m_CraftAll_Btn.interactable = false;
        m_CraftAll_Btn.transform.Find("Text").GetComponent<Text>().color = Color.black;
    }

    public void ActiveButton()
    {
        m_Craft_Btn.interactable = true;
        m_Craft_Btn.transform.Find("Text").GetComponent<Text>().color = Color.white;
    }

    private void CraftingItem()
    {
       
        GameObject item = Instantiate<GameObject>(prefab_InventoryItem, bg_Transform);

        item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 110);
        item.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 110);

        item.GetComponent<InventoryItemController>().InitItem(tempId, tempSpriteName, 1, true);

        InitButton();

        SendMessageUpwards("CraftingOK");
    }


}
