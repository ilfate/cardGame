
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Unit : MonoBehaviour {

	private int health;
	private string unitName;
	private int startInitiative;
	public int currentInitiative;
	public int previousInitiative;
	public int x;
	public int y;
	protected UnitManager unitManager;
	protected Vector3[] possibleMovements;


	// Use this for initialization
	void Awake () {
		this.startInitiative = 5;
		this.currentInitiative = this.startInitiative;
		possibleMovements = new Vector3[] {
			new Vector3(1, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(-1, 0, 0),
			new Vector3(0, -1, 0),
		};
	}

	void Start() {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Action() {
		previousInitiative = currentInitiative;
		currentInitiative += 2;
		unitManager.UpdateTimeLine();
	}

	public int CurrentInitiative {
		get {
			return currentInitiative;
		}
	}

	public Vector3[] GetPossibleMovements()
	{
		return possibleMovements.Select (vector => vector + gameObject.transform.position).ToArray();
		//return possibleMovements;
	}
}
