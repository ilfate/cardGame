using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapManager : MonoBehaviour {

	private Transform mapContainer;

	public int mapWidth = 8;
	public int mapHeight = 8;

	public GameObject[] cellTiles;


	// Use this for initialization
	void Start () {
		MapSetup ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void MapSetup()
	{
		mapContainer = new GameObject ("MapContainer").transform;
		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				GameObject tilePrefab = cellTiles [Random.Range (0, cellTiles.Length)];

				GameObject tile = Instantiate (tilePrefab, new Vector3 (x, y, 0), Quaternion.identity) as GameObject;
				tile.transform.SetParent (mapContainer);
				//SpriteRenderer sprite = tile.GetComponent<SpriteRenderer>();
				//sprite.sortingLayerName = "Ground";
			}
		}
	}
}
