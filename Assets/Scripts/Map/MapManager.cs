using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour 
{
	public const string SORTING_LAYER = "Ground";
	public int mapWidth = 10;
	public int mapHeight = 8;

	public GameObject[] cellTiles;

	protected MapTile[,] map;

	protected UnitManager unitManager;
	protected ControlsManager controlsManager;


	// Use this for initialization
	void Start () {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager>();
		controlsManager = GameObject.Find ("ControlsManager").GetComponent<ControlsManager>();
		map = new MapTile[mapWidth, mapHeight];
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
				map [x, y] = tile.GetComponent<MapTile> ();
				//SpriteRenderer sprite = tile.GetComponent<SpriteRenderer>();
				//sprite.sortingLayerName = "Ground";
			}
		}
	}

	public void updateVisible(List<Vector3> visible)
	{
		for (int x = 0; x < mapWidth; x++) {
			for (int y = 0; y < mapHeight; y++) {
				map [x, y].SetVisibility (false);
			}
		}
		foreach (Vector3 vector in visible) {
			int x = (int) Mathf.Round(vector.x);
			int y = (int) Mathf.Round(vector.y);
			if (x >= 0 && x < mapWidth && y >= 0 && y < mapHeight) {
				map [x, y].SetVisibility (true);
			}
		}
	}


}
