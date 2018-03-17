using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	public GameObject unitPrefab;
	public GameObject initiativePanel;

	// Use this for initialization
	void Start () {
		this.initiativePanel = GameObject.Find ("InitiativePanel");

		this.CreateUnit ();
	}

	public void CreateUnit()
	{
		GameObject unitObj = Instantiate (unitPrefab, new Vector3 (2, 2, 0), Quaternion.identity) as GameObject;
		unitObj.transform.SetParent (transform);
		this.initiativePanel.GetComponent<InitiativePanel>().AddToList (unitObj.GetComponent<Unit>());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
