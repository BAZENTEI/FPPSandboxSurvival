using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildPanelView : MonoBehaviour {
	private Transform BG_Transform;
	private Transform player_Transform;
	private Transform models_Parent;

	private GameObject item_Prefab;
	private GameObject material_Prefab;
	private Text itemName_Text;
	private Camera worldCamera = null;

	private List<Sprite> icons = new List<Sprite>();
	private List<Sprite[]> materialIcons = new List<Sprite[]>();
	private List<string[]> materialIconNames = new List<string[]>();
	private List<GameObject[]> materialModels = new List<GameObject[]>();

	public Transform M_BG_Transform { get { return BG_Transform; } }
	public Transform M_Player_Transform { get { return player_Transform; } }
	public Transform M_Models_Parent { get { return models_Parent; } }
	public GameObject M_Item_Prefab { get { return item_Prefab; } }
	public GameObject M_Material_Prefab { get { return material_Prefab; } }
	public Text M_ItemName_Text { get { return itemName_Text; } }
	public Camera WorldCamera { get { return worldCamera; } }
	public List<Sprite> Icons { get { return icons; } }
	public List<Sprite[]> MaterialIcons { get { return materialIcons; } }
	public List<string[]> MaterialIconNames { get { return materialIconNames; } }
	public List<GameObject[]> MaterialModels { get { return materialModels; } }


	void Awake () {
		Init();
		LoadIcons();
		LoadMaterialIcons();
		SetmaterialIconNames();
		LoadMaterialModels();
	}
	
	
	void Update () {
		
	}

	private void Init(){
		BG_Transform = transform.Find("WheelBG");
		player_Transform = GameObject.Find("FPPController").transform;
		models_Parent = GameObject.Find("BuildModelsParent").transform;

		item_Prefab = Resources.Load<GameObject>("Build/Prefab/Item");
		material_Prefab = Resources.Load<GameObject>("Build/Prefab/MaterialBG");
		itemName_Text = transform.Find("WheelBG/ItemName").GetComponent<Text>();

		worldCamera = GameObject.Find("WorldCamera").GetComponent<Camera>();
		
	}

	private void LoadIcons(){
		icons.Add(null);
		icons.Add(Resources.Load<Sprite>("Build/Icon/Question Mark"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Roof_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Stairs_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Window_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Door_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Wall_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Floor_Category"));
		icons.Add(Resources.Load<Sprite>("Build/Icon/Foundation_Category"));
	}

	private void LoadMaterialIcons(){
		materialIcons.Add(null);
		materialIcons.Add(new Sprite[] { LoadIcon("Ceiling Light"), LoadIcon("Pillar_Wood"), LoadIcon("Wooden Ladder") });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Roof_Metal"), null });
		materialIcons.Add(new Sprite[] { LoadIcon("Stairs_Wood"), LoadIcon("L Shaped Stairs_Wood"), null });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Window_Wood"), null });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Wooden Door"), null });
		materialIcons.Add(new Sprite[] { LoadIcon("Wall_Wood"), LoadIcon("Doorway_Wood"), LoadIcon("Window Frame_Wood") });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Floor_Wood"), null });
		materialIcons.Add(new Sprite[] { null, LoadIcon("Platform_Wood"), null });
	}

	private void SetmaterialIconNames(){
		materialIconNames.Add(null);
		materialIconNames.Add(new string[] { "Ceiling Light", "Wood Pillar", "Ladder" });
		materialIconNames.Add(new string[] { "Wood Pillar" });
		materialIconNames.Add(new string[] { "Ceiling Light2", "Wood Pillar" });
		materialIconNames.Add(new string[] { "Wood Pillar" });
		materialIconNames.Add(new string[] { "Wood Pillar" });
		materialIconNames.Add(new string[] { "Ceiling Light5", "Wood Pillar", "Ladder" });
		materialIconNames.Add(new string[] { "Wood Pillar" });
		materialIconNames.Add(new string[] { "Wood Pillar" });
	}

	private void LoadMaterialModels(){
		materialModels.Add(null);
		materialModels.Add(new GameObject[] { LoadBuildModel("Ceiling_Light"), LoadBuildModel("Pillar"), LoadBuildModel("Ladder") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Roof") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Stairs"), LoadBuildModel("L_Shaped_Stairs") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Window") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Door") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Wall"), LoadBuildModel("Doorway"), LoadBuildModel("Window_Frame") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Floor") });
		materialModels.Add(new GameObject[] { LoadBuildModel("Platform") });

	}

	private Sprite LoadIcon(string name){
		return Resources.Load<Sprite>("Build/MaterialIcon/" + name);
	}

	private GameObject LoadBuildModel(string name){
		return Resources.Load<GameObject>("Build/Prefab/Material/" + name);
	}
}
