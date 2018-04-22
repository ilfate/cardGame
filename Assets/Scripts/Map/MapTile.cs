using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour {

	public bool isVisible = false;

	public void SetVisibility(bool visible)
	{
		isVisible = visible;
		GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, isVisible ? 1f : 0.5f);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
