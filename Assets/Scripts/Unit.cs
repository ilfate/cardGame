
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private int health;
	private string unitName;
	private int startInitiative;
	public int currentInitiative;
	public int previousInitiative;
	protected UnitManager unitManager;


	// Use this for initialization
	void Awake () {
		this.startInitiative = 5;
		this.currentInitiative = this.startInitiative;
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
}
