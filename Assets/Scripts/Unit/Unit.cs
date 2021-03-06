﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class Unit : MonoBehaviour {

	private int health;
	private string unitName;
	private int startInitiative;
	public int currentInitiative;
	public int previousInitiative;
	public int x;
	public int y;
	public int d = 0;
	protected UnitManager unitManager;
	public ControlsManager controlsManager;

	protected Vector3[] possibleMovements;
	protected Vector3[] visionPattern;

	protected float maxMovementsPerTurn;
	protected float movementsDoneThisTurn = 0f;
	protected float movementCost = 1f;
	protected float rotationCost = 0.5f;

	protected float maxActionsPerTurn = 1f;
	protected float actionsDoneThisTurn = 0f;

	protected float actionTime;


	// Use this for initialization
	void Awake () {
		this.startInitiative = 5;
		this.currentInitiative = this.startInitiative;
		maxMovementsPerTurn = 3f;
		actionTime = 4f;
		possibleMovements = new Vector3[] {
			new Vector3(1, 0, 0),
			new Vector3(0, 1, 0),
			new Vector3(-1, 0, 0),
			new Vector3(0, -1, 0),
		};

		visionPattern = new Vector3[] {
			new Vector3(-1, 0, 0),
			new Vector3(-2, 0, 0),
			new Vector3(1, 0, 0),
			new Vector3(2, 0, 0),
			new Vector3(0, -1, 0),
			new Vector3(-1, -1, 0),
			new Vector3(-2, -1, 0),
			new Vector3(-3, -1, 0),
			new Vector3(-4, -1, 0),
			new Vector3(1, -1, 0),
			new Vector3(2, -1, 0),
			new Vector3(3, -1, 0),
			new Vector3(4, -1, 0),
			new Vector3(0, -2, 0),
			new Vector3(-1, -2, 0),
			new Vector3(-2, -2, 0),
			new Vector3(-3, -2, 0),
			new Vector3(1, -2, 0),
			new Vector3(2, -2, 0),
			new Vector3(3, -2, 0),
			new Vector3(0, -3, 0),
			new Vector3(-1, -3, 0),
			new Vector3(-2, -3, 0),
			new Vector3(1, -3, 0),
			new Vector3(2, -3, 0),
			new Vector3(0, -4, 0),
			new Vector3(-1, -4, 0),
			new Vector3(1, -4, 0),
			new Vector3(0, -5, 0),
		};
	}

	void Start() {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager> ();
		controlsManager = GameObject.Find ("ControlsManager").GetComponent<ControlsManager>();
	}

	public void Action() {
		AddInitiative (2);
	}

	public void AddInitiative(int additionalInitiative)
	{
		previousInitiative = currentInitiative;
		currentInitiative += additionalInitiative;
		unitManager.UpdateTimeLine();
	}

	public int CurrentInitiative {
		get {
			return currentInitiative;
		}
	}

	public void Move(int x, int y)
	{
		Vector3 from = new Vector3 (this.x, this.y, 0);
		this.x = x;
		this.y = y;
		Vector3 end = new Vector3 (x, y, 0);
		//this.gameObject.Tween("MoveUnit" + this.unitName, this.transform.position, end, 0.3f, TweenScaleFunctions.CubicEaseIn, 
		//	(t) => {
		//		this.transform.position = t.CurrentValue;
		//	}, null);
		unitManager.MoveUnit(this, from);
		transform.DOMove(new Vector3(x, y, 0), 0.3f).OnComplete(AfterMove);
		movementsDoneThisTurn += movementCost;
		controlsManager.hideAllControlls ();
	}

	public void Rotate(int direction)
	{
		this.d += direction;
		if (d > 3)
			d = 0;
		if (d < 0)
			d = 3;
		unitManager.UpdateVisibility ();
	}

	public void AfterMove()
	{
		if (movementsDoneThisTurn >= maxMovementsPerTurn) {
			// no more movements
			actionsDoneThisTurn++;
			if (actionsDoneThisTurn >= maxActionsPerTurn) {
				// there is no actions left as well
				// we have to end turn and move initiative
				finishTurn();
				AddInitiative((int)actionTime);
			}
		} else {
			controlsManager.DisplayMoveControls (this);
		}
	}

	public void finishTurn()
	{
		actionsDoneThisTurn = 0;
		movementsDoneThisTurn = 0;
	}

	public Vector3[] GetPossibleMovements()
	{
		return possibleMovements.Select (vector => vector + gameObject.transform.position).ToArray();
		//return possibleMovements;
	}

	public Vector3[] GetVisibleTiles()
	{
		float rotate = Mathf.PI * 2;
		switch (d) {
			case 0:
				rotate = 180;
				break;
			case 1:
				rotate = 90;
				break;
			case 2:
				rotate = 0;
				break;
			case 3:
				rotate = 270;
				break;
		}
		Vector3[] returnArray = new Vector3[visionPattern.Length];
		for (int i = 0; i < returnArray.Length; i++) {
			returnArray [i] = Quaternion.Euler (0, 0, rotate) * (visionPattern [i] ); // + new Vector3(0.25f, 0.25f, 0)
			//returnArray [i] = visionPattern [i]; // + new Vector3(0.25f, 0.25f, 0)
			//Debug.Log(returnArray [i]);
		}
		return returnArray;
	}
}
