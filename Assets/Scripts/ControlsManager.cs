using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour {

	protected List<GameObject> buttons;
	protected UnitManager unitManager;

	public GameObject moveSquarePrefab;

	public const string SORTING_LAYER = "Controlls";

	// Use this for initialization
	void Start () {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager> ();
		buttons = new List<GameObject> ();
	}

	public void AddMoveControll(int x, int y, Unit unit)
	{
		GameObject button = Instantiate (moveSquarePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
		button.GetComponent<SpriteRenderer>().sortingLayerName = ControlsManager.SORTING_LAYER;
		button.GetComponent<MoveButton> ().unit = unit;
		//button.
		buttons.Add (button);
	}

	public void DisplayMoveControls(Unit unit)
	{

		Vector3[] movementDirections = unit.GetPossibleMovements ();
		foreach (Vector3 vector in movementDirections) {

			int x = (int) vector.x;
			int y = (int) vector.y;
			if (!unitManager.HasUnit (x, y)) {
				AddMoveControll (x, y, unit);
			}
		}
	}

	public void hideAllControlls()
	{
		foreach (GameObject button in buttons) {
			Destroy (button);
		}
		buttons.Clear ();
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
