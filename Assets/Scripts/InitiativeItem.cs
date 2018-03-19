using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeItem : MonoBehaviour {

	public Unit unit;
	public InitiativePanel panel;

	// Use this for initialization
	void Awake () {
		Canvas.ForceUpdateCanvases ();
		this.panel = this.transform.parent.gameObject.GetComponent<InitiativePanel> ();
	}

	public void CalculatePosition(int positionInList) {
		float y = - this.panel.rect.height / 2;
		float start = - this.panel.rect.width / 2 + (this.panel.itemWidth / 2);
		float x = start + (positionInList * this.panel.itemWidth); 
		this.transform.localPosition = new Vector3 (x, y, 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
