using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingMapItem {
	private int mapId;
	private string[] mapContents;
	private string mapName;

	public int MapId{
		get{ return mapId; }
		set{ mapId = value; }
	}

    public string[] MapContents{
		get { return mapContents; }
		set { mapContents = value; }
	}

	public string MapName{
		get{ return mapName; }
		set{ mapName = value; }
	}

	public CraftingMapItem(int mapId, string[] mapContents, string mapName){
		this.mapId = mapId;
		this.mapContents = mapContents;
		this.mapName = mapName;
	}

	public override string ToString(){
		return string.Format("ID:{0}, map:{1},mapName:{2}",this.mapId, this.mapContents.Length, this.mapName);
	}








}
