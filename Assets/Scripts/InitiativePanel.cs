using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativePanel : MonoBehaviour {

	public int currentInitiative;
	public Dictionary<int, Unit> list;

	void Awake() {
		this.currentInitiative = 0;
		this.list = new Dictionary<int, Unit> ();
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Step() {
		this.currentInitiative++;
	}

	public void AddToList(Unit unit) {
		int initiative = unit.CurrentInitiative;
		while (this.list.ContainsKey (initiative)) {
			initiative++;
		}
		this.list.Add (initiative, unit);
		this.RenderList ();
	}

	public void RenderList() 
	{
		foreach (KeyValuePair<int, Unit> pair in this.list) {
			Debug.Log (pair.Key.ToString() + " - " + pair.Value.ToString());
		}
	}
}
