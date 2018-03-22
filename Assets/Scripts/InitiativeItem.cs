using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeItem : MonoBehaviour {

	public Unit unit;
	protected Rect parentRect;

	// Use this for initialization
	void Awake () {
		Canvas.ForceUpdateCanvases ();
		this.parentRect = this.transform.parent.gameObject.GetComponent<RectTransform> ().rect;
	}

	public void CalculatePosition(int positionInList) {
		//Debug.Log(this.transform.parent.GetComponent<RectTransform> ().rect.height);
		float y = 0f;
		float start = 0f;
		float x = start + (positionInList * InitiativePanel.itemWidth); 
		this.transform.localPosition = new Vector3 (x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
