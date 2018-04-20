
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
		controlsManager = GameObject.Find ("ControlsManager").GetComponent<ControlsManager>();
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

	public void Move(int x, int y)
	{
		this.x = x;
		this.y = y;
		Vector3 end = new Vector3 (x, y, 0);
		//this.gameObject.Tween("MoveUnit" + this.unitName, this.transform.position, end, 0.3f, TweenScaleFunctions.CubicEaseIn, 
		//	(t) => {
		//		this.transform.position = t.CurrentValue;
		//	}, null);
		transform.DOMove(new Vector3(x, y, 0), 0.3f).OnComplete(AfterMove);
		controlsManager.hideAllControlls ();
	}

	public void AfterMove()
	{
		controlsManager.DisplayMoveControls (this);
	}

	public Vector3[] GetPossibleMovements()
	{
		return possibleMovements.Select (vector => vector + gameObject.transform.position).ToArray();
		//return possibleMovements;
	}
}
