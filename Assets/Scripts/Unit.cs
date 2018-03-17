using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	private int health;
	private string unitName;
	private int startInitiative;
	private int currentInitiative;


	// Use this for initialization
	void Start () {
		this.startInitiative = 5;
		this.currentInitiative = this.startInitiative;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int CurrentInitiative {
		get {
			return currentInitiative;
		}
	}
}
