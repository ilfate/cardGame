
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
	protected UnitManager unitManager;
	public ControlsManager controlsManager;
	protected Vector3[] possibleMovements;

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
	}

	void Start() {
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager> ();
		controlsManager = GameObject.Find ("ControlsManager").GetComponent<ControlsManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
