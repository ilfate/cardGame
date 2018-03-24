using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitManager : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject initiativePanel;

	public Unit currectActiveUnit;

	// Use this for initialization
	void Start () {
		this.initiativePanel = GameObject.Find ("InitiativePanel");
	}

	public void CreateUnit()
	{
		GameObject unitObj = Instantiate (unitPrefab, new Vector3 (2, 2, 0), Quaternion.identity) as GameObject;
		unitObj.transform.SetParent (transform);
		this.initiativePanel.GetComponent<InitiativePanel>().AddToList (unitObj.GetComponent<Unit>());
	}
	
	public void FindNextUnit()
	{
		initiativePanel.GetComponent<InitiativePanel> ().MakeStepTillNextUnit ();
	}

	public void UnitAction()
	{
		if (!currectActiveUnit)
			return;
		currectActiveUnit.Action ();
	}

	public void UpdateTimeLine() {
		initiativePanel.GetComponent<InitiativePanel> ().UpdateUnits ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
