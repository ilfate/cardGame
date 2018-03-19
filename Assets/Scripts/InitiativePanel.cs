using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class InitiativePanel : MonoBehaviour {

	public int currentInitiative;
	public Dictionary<int, List<GameObject>> list;
	public GameObject listItemPrefab;
	public GameObject numberListPrefab;
	public Rect rect;
	protected Dictionary<int, GameObject> numbersList;

	public float itemWidth;

	void Awake() {
		this.currentInitiative = 0;
		this.list = new Dictionary<int, List<GameObject>> ();
		this.numbersList = new Dictionary<int, GameObject> ();
	}

	// Use this for initialization
	void Start () {
		Canvas.ForceUpdateCanvases ();
		this.rect = this.GetComponent<RectTransform> ().rect;
		this.itemWidth = listItemPrefab.GetComponent<RectTransform> ().rect.width;
		for (int i = 0; i < 100; i++) {
			GameObject number = Instantiate (this.numberListPrefab, Vector3.zero, Quaternion.identity, this.transform);
			number.GetComponent<Text> ().text = i.ToString ();
			InitiativeNumber numberComponent = number.GetComponent<InitiativeNumber> ();
			numberComponent.number = i;
			numberComponent.CalculatePosition (i - this.currentInitiative, true);
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
		InitiativeItem item = newListItem.GetComponent<InitiativeItem> ();
		item.unit = unit;


		slot.Add (newListItem);
		this.list.Add (initiative, slot);
		this.RenderList ();
	}

	public float GetRenderStartPosition()
	{
		return - this.rect.width / 2 + (this.itemWidth / 2);
	}

	public void RenderList() 
	{
		int initiative = this.currentInitiative;
		int position = 0;
		bool noSpaceLeft = false;
		while (!noSpaceLeft) {
			InitiativeNumber numberObj = this.numbersList [initiative].GetComponent<InitiativeNumber>();
			noSpaceLeft = !numberObj.CalculatePosition (position, false);
			if (noSpaceLeft)
				break;
			if (this.list.ContainsKey (initiative)) {
				// yes we have here items
				List<GameObject> slot = this.list [initiative];
				foreach (GameObject itemObj in slot) {
					InitiativeItem item = itemObj.GetComponent<InitiativeItem> ();
					item.CalculatePosition (position);
					position++;
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
