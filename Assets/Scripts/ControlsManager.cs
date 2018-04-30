using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour {

	protected List<GameObject> buttons;
	protected UnitManager unitManager;

	protected GameObject rotateLeftButton;
	protected GameObject rotateRightButton;
	protected GameObject rotationControlsPanel;

	public GameObject moveSquarePrefab;

	public const string SORTING_LAYER = "Controlls";

	// Use this for initialization
	void Start () {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager> ();
		buttons = new List<GameObject> ();
		rotateLeftButton = GameObject.Find("UnitRotateLeft");
		rotateRightButton = GameObject.Find("UnitRotateRight");
		rotationControlsPanel = GameObject.Find ("RotationControlls");
		hideRotateButtons ();
	}

	public void AddMoveControll(int x, int y, Unit unit)
	{
		GameObject button = Instantiate (moveSquarePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
		SpriteRenderer renderer = button.GetComponent<SpriteRenderer> ();
		renderer.color = new Color (renderer.color.r, renderer.color.g, renderer.color.b, 0.5f);
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
		showRotateButtons (unit);
	}

	public void hideAllControlls()
	{
		foreach (GameObject button in buttons) {
			Destroy (button);
		}
		buttons.Clear ();
		hideRotateButtons ();
	}

	public void hideRotateButtons()
	{
		CanvasGroup group = rotationControlsPanel.GetComponent<CanvasGroup> ();
		group.alpha = 0f;
		group.blocksRaycasts = false;
	}

	public void showRotateButtons(Unit unit)
	{
		CanvasGroup group = rotationControlsPanel.GetComponent<CanvasGroup> ();
		group.alpha = 1f;
		group.blocksRaycasts = true;
		Button btn1 = rotateLeftButton.GetComponent<Button> ();
		btn1.onClick.RemoveAllListeners ();
		btn1.onClick.AddListener (() => unit.Rotate (-1));

		Button btn2 = rotateRightButton.GetComponent<Button> ();
		btn2.onClick.RemoveAllListeners ();
		btn2.onClick.AddListener (() => unit.Rotate (1));
	}


	
	// Update is called once per frame
	void Update () {
		
	}
}
