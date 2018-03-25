using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour {

	protected List<GameObject> buttons;

	public GameObject moveSquarePrefab;

	public const string SORTING_LAYER = "Controlls";

	// Use this for initialization
	void Start () {
		buttons = new List<GameObject> ();
	}

	public void AddMoveControll(int x, int y)
	{
		GameObject button = Instantiate (moveSquarePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
		button.GetComponent<SpriteRenderer>().sortingLayerName = ControlsManager.SORTING_LAYER;
		buttons.Add (button);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
