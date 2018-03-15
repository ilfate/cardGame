using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour {

	public GameObject unitPrefab;

	// Use this for initialization
	void Start () {
		GameObject unitObj = Instantiate (unitPrefab, new Vector3 (2, 2, 0), Quaternion.identity) as GameObject;
		unitObj.transform.SetParent (transform);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
