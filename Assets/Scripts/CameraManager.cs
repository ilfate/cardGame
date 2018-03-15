using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

	[Range(1, 4)]
	public int pixelScale = 1;
	private Camera camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (camera == null) {
			camera = GetComponent<Camera> ();
		}
		camera.orthographicSize = Screen.height * (0.5f / 40.0f / pixelScale);
	}
}
