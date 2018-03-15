using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dragable : MonoBehaviour{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Fire1"))
		{
			Debug.Log(Input.mousePosition);
		}
		if (Input.GetButtonUp("Fire1"))
		{
			Debug.Log(Input.mousePosition);
		}
	}


}
