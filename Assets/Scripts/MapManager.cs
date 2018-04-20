using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour 
{
	public const string SORTING_LAYER = "Ground";
	public int mapWidth = 8;
	public int mapHeight = 8;

	public GameObject[] cellTiles;

	protected UnitManager unitManager;
	protected ControlsManager controlsManager;


	// Use this for initialization
	void Start () {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager>();
		controlsManager = GameObject.Find ("ControlsManager").GetComponent<ControlsManager>();
		MapSetup ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MapSetup()
	{
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				GameObject tilePrefab = cellTiles [Random.Range (0, cellTiles.Length)];
				GameObject tile = Instantiate (tilePrefab, new Vector3 (x, y, 0), Quaternion.identity, transform) as GameObject;

				tile.GetComponent<SpriteRenderer>().sortingLayerName = MapManager.SORTING_LAYER;
				//SpriteRenderer sprite = tile.GetComponent<SpriteRenderer>();
				//sprite.sortingLayerName = "Ground";
			}
		}
	}


}
