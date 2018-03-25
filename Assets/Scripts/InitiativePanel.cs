using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;

public class InitiativePanel : MonoBehaviour {

	public int currentInitiative;
	public Dictionary<int, List<GameObject>> list;
	public GameObject listItemPrefab;
	public GameObject numberListPrefab;
	public Rect rect;
	protected Dictionary<int, GameObject> numbersList;
	protected UnitManager unitManager;

	public static float stepDeleay = 0.4f;
	public static float itemWidth;

	void Awake() {
		this.currentInitiative = 0;
		this.list = new Dictionary<int, List<GameObject>> ();
		this.numbersList = new Dictionary<int, GameObject> ();
	}

	// Use this for initialization
	void Start () {
		Canvas.ForceUpdateCanvases ();
		unitManager = GameObject.Find ("UnitManager").GetComponent<UnitManager>();
		this.rect = this.GetComponent<RectTransform> ().rect;
		InitiativePanel.itemWidth = listItemPrefab.GetComponent<RectTransform> ().rect.width;
		for (int i = 0; i < 100; i++) {
			GameObject number = Instantiate (this.numberListPrefab, Vector3.zero, Quaternion.identity, this.transform);
			number.GetComponent<Text> ().text = i.ToString ();
			number.name = "InitiativeNumber_" + i;
			InitiativeNumber numberComponent = number.GetComponent<InitiativeNumber> ();
			numberComponent.number = i;
			int position = i - this.currentInitiative;
			if (numberComponent.IsVisible(position)) {
				Vector3 endPosition = CalculatePosition (position);
				numberComponent.SetPosition (endPosition);
			}
			//this.UpdateNumber (number);
			this.numbersList.Add (i, number);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Step() {
		
		this.numbersList [this.currentInitiative].SetActive (false);
		this.currentInitiative++;
		this.RenderList ();
	}

	public void AddToList(Unit unit) {
		int initiative = unit.CurrentInitiative;
		List<GameObject> slot = new List<GameObject> ();
		if (this.list.ContainsKey (initiative)) {
			slot = this.list [initiative];
			this.list.Remove (initiative);
		}

		GameObject newListItem = Instantiate (this.listItemPrefab, Vector3.zero, Quaternion.identity, this.transform);
		newListItem.name = "Initiative" + unit.name;
		InitiativeUnit item = newListItem.GetComponent<InitiativeUnit> ();
		item.unit = unit;


		slot.Add (newListItem);
		this.list.Add (initiative, slot);
		this.RenderList ();
	}

	public float GetRenderStartPosition()
	{
		return - this.rect.width / 2 + (InitiativePanel.itemWidth / 2);
	}

	public void MakeStepTillNextUnit()
	{
		
		unitManager.currectActiveUnit = null;
		StartCoroutine (MultipleSteps (() => {
			List<GameObject> slot = list [currentInitiative];
			unitManager.ActivateNextUnit(slot.First ().GetComponent<InitiativeUnit>().unit);
		}));

	}

	IEnumerator MultipleSteps(Action callback)
	{
		while (!list.ContainsKey (currentInitiative)) {
			Step ();
			yield return new WaitForSeconds (InitiativePanel.stepDeleay);
		}
		callback ();
	}

	public void UpdateUnits() {
		bool wasUpdated = false;
		Dictionary<int, List<GameObject>> newList = new Dictionary<int, List<GameObject>> ();
		foreach (KeyValuePair<int, List<GameObject>> pair in list) {
			List<GameObject> newSlot = new List<GameObject> ();
			if (newList.ContainsKey (pair.Key)) {
				newSlot = newList [pair.Key];
			}
			
			for (int key = 0; key < pair.Value.Count; key++) {
				GameObject initiativeObj = pair.Value [key];
				Unit unit = initiativeObj.GetComponent<InitiativeUnit> ().unit;
				if (unit.currentInitiative != pair.Key) {
					// this unit initiative has changed. we need to move it
					if (newList.ContainsKey (unit.currentInitiative)) {
						newList [unit.currentInitiative].Add (initiativeObj);
					} else {
						List<GameObject> featureSlot = new List<GameObject> ();
						featureSlot.Add (initiativeObj);
						newList.Add (unit.currentInitiative, featureSlot);
					}
					wasUpdated = true;
				} else {
					newSlot.Add (initiativeObj);
				}
			}
			if (newSlot.Count > 0) {
				newList.Add (pair.Key, newSlot);
			}
		}
		list = newList;
		if (wasUpdated) {
			RenderList ();
		}
	}

	public Vector3 CalculatePosition(int position)
	{
		float start = GetRenderStartPosition ();
		float x = start + (position * InitiativePanel.itemWidth);
		Vector3 end = new Vector3 (x, - rect.height / 2, 0);
		return end;
	}

	public void RenderList() 
	{
		int initiative = this.currentInitiative;
		int position = 0;
		bool noSpaceLeft = false;
		while (!noSpaceLeft) {
			InitiativeNumber numberObj = this.numbersList [initiative].GetComponent<InitiativeNumber>();
			bool wasActive = numberObj.WasActive ();
			noSpaceLeft = !numberObj.IsVisible (position);
			if (noSpaceLeft) {
				break;
			}
			Vector3 numberPosition = CalculatePosition (position);
			if (!wasActive) {
				numberObj.SetPosition (numberObj.FindLisstEndPosition());
			}
			numberObj.Animate (numberPosition);
			if (this.list.ContainsKey (initiative)) {
				// yes we have here items
				List<GameObject> slot = this.list [initiative];
				foreach (GameObject itemObj in slot) {
					InitiativeUnit item = itemObj.GetComponent<InitiativeUnit> ();

					Vector3 itemPosition = CalculatePosition (position);
					item.Animate (itemPosition);

					position ++;
				}
			} else {
				position++;
			}
			initiative++;
		}
		//foreach (KeyValuePair<int, List<GameObject>> pair in this.list) {
			//Debug.Log (pair.Key.ToString() + " - " + pair.Value.ToString());
		//}
	}
}
