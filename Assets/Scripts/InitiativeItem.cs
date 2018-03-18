using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeItem : MonoBehaviour {

	public Unit unit;
	public Rect parentRect;
	protected float itemWidth;

	// Use this for initialization
	void Awake () {
		Canvas.ForceUpdateCanvases ();
		this.parentRect = this.transform.parent.gameObject.GetComponent<InitiativePanel> ().rect;
		this.itemWidth = this.GetComponent<RectTransform> ().rect.width;
	}

	public void CalculatePosition(int positionInList) {
		float y = - this.parentRect.height / 2;
		float start = - this.parentRect.width / 2 + (this.itemWidth / 2);
		float x = start + (positionInList * this.itemWidth); 
		this.transform.localPosition = new Vector3 (x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
