using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeNumber : MonoBehaviour {

	public int number;

	protected InitiativePanel panel;
	public float itemWidth;

	void Awake () {
		this.panel = this.transform.parent.gameObject.GetComponent<InitiativePanel> ();
	}

	public bool CalculatePosition(int position)
	{
		float start = this.panel.GetRenderStartPosition ();
		float x = start + (position * this.itemWidth);
		if (x > start + this.panel.rect.width || x < start) {
			this.gameObject.SetActive (false);
			return false;
		}
		this.gameObject.SetActive (true);
		this.transform.localPosition = new Vector3 (x, - this.panel.rect.height / 2, 0);
		return true;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
