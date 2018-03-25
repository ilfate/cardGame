using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeNumber : InitiativeItem {

	public int number;


	/*protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainDistance = (this.transform.localPosition - end).sqrMagnitude;
		float fullDistance = (this.transform.localPosition - end).magnitude; 

		while (sqrRemainDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (this.transform.localPosition, end, fullDistance * Time.deltaTime / moveTime);

			this.transform.localPosition = newPosition;
			sqrRemainDistance = (this.transform.localPosition - end).sqrMagnitude;

			yield return null;
		}
	}*/

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
