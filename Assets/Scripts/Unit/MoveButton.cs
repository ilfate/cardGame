using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveButton : MonoBehaviour {
	
	public Unit unit;

	void OnMouseDown() {
		unit.Move ((int) this.transform.position.x, (int) this.transform.position.y);
		//unit.Rotate(1);
	}
}
