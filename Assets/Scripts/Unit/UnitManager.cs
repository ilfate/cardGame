using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : MonoBehaviour {

	public const string SORTING_LAYER = "Units";

	public GameObject unitPrefab;
	public GameObject initiativePanel;
	public MapManager mapManager;
	public ControlsManager controlsManager;

	public Unit currectActiveUnit;

	protected Dictionary<string, GameObject> units;

	protected int lastUnitId = 0;

	void Awake() {
		units = new Dictionary<string, GameObject> ();
	}

	// Use this for initialization
	void Start () {
		this.initiativePanel = GameObject.Find ("InitiativePanel");
		mapManager = GameObject.Find ("MapManager").GetComponent<MapManager>();
		controlsManager = GameObject.Find ("ControlsManager").GetComponent<ControlsManager>();

		CreateUnit (3, 3);
		//CreateUnit (1, 1);
	}

	public void CreateUnit(int x, int y)
	{
		lastUnitId++;
		GameObject unitObj = Instantiate (unitPrefab, new Vector3 (x, y, 0), Quaternion.identity);
		unitObj.name = "Unit_" + lastUnitId;
		unitObj.transform.SetParent (transform);
		unitObj.GetComponent<SpriteRenderer>().sortingLayerName = UnitManager.SORTING_LAYER;
		Unit unit = unitObj.GetComponent<Unit> ();
		unit.x = x;
		unit.y = y;
		AddUnit (unitObj);
		this.initiativePanel.GetComponent<InitiativePanel>().AddToList (unit);
	}

	public void AddUnit(GameObject unitObj)
	{
		Unit unit = unitObj.GetComponent<Unit> ();
		units.Add (unit.x.ToString () + "_" + unit.y.ToString (), unitObj);
		UpdateVisibility ();
	}

	public void UpdateVisibility()
	{
		mapManager.updateVisible (GetVisibleTiles ());
	}

	public void MoveUnit(Unit unit, Vector3 from)
	{
		this.RemoveUnit ((int)from.x, (int)from.y);
		this.AddUnit (unit.gameObject);

	}

	public void RemoveUnit(int x, int y)
	{
		units.Remove (GetKey(x, y));
	}

	public bool HasUnit(int x, int y) 
	{
		return units.ContainsKey (GetKey(x, y));
	}

	protected string GetKey(int x, int y)
	{
		return x.ToString () + "_" + y.ToString ();
	}
		
	protected string GetKey(float x, float y)
	{
		return x.ToString () + "_" + y.ToString ();
	}

	public GameObject GetUnit(int x, int y)
	{
		return units [GetKey(x, y)];
	}
	
	public void FindNextUnit()
	{
		initiativePanel.GetComponent<InitiativePanel> ().MakeStepTillNextUnit ();
	}

	public void ActivateNextUnit(Unit unit)
	{
		currectActiveUnit = unit;
		controlsManager.DisplayMoveControls (unit);
	}


	public void UnitAction()
	{
		if (!currectActiveUnit)
			return;
		currectActiveUnit.Action ();
	}

	public void UpdateTimeLine() {
		StartCoroutine (TimeLineAndSelectNext ());
	}

	IEnumerator TimeLineAndSelectNext()
	{
		initiativePanel.GetComponent<InitiativePanel> ().UpdateUnits ();
		yield return new WaitForSeconds (InitiativePanel.stepDeleay);
		FindNextUnit ();
	}
	
	public List<Vector3> GetVisibleTiles()
	{
		List<string> keyMap = new List<string> ();
		List<Vector3> visible = new List<Vector3>();
		foreach (KeyValuePair<string, GameObject> pair in units) {
			Unit unit = pair.Value.GetComponent<Unit> ();
			Vector3[] pattern = unit.GetVisibleTiles ();
			foreach (Vector3 vector in pattern) {
				if (!keyMap.Contains(GetKey(vector.x, vector.y))) {
					visible.Add (vector + new Vector3(unit.x, unit.y, 0));
					keyMap.Add (GetKey (vector.x, vector.y));
					//Debug.Log (vector + new Vector3(unit.x, unit.y, 0));
				}
			}
		}

		return visible;
	}
}
