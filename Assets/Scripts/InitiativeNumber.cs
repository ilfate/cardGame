using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiativeNumber : MonoBehaviour {

	public int number;
	public float moveTime = 0.1f;
	private float inverseMoveTime;

	protected InitiativePanel panel;

	void Awake () {
		this.panel = this.transform.parent.gameObject.GetComponent<InitiativePanel> ();
		this.inverseMoveTime = 1f / this.moveTime;
	}

	public bool CalculatePosition(int position, bool isInstantMove = false)
	{
		float start = this.panel.GetRenderStartPosition ();
		float x = start + (position * this.panel.itemWidth);
		if (x > start + this.panel.rect.width || x < start) {
			this.gameObject.SetActive (false);
			return false;
		}
		bool wasActive = this.gameObject.activeSelf;
		this.gameObject.SetActive (true);
		Vector3 end = new Vector3 (x, - this.panel.rect.height / 2, 0);
		if (isInstantMove || !wasActive) {
			this.transform.localPosition = end;
		} else {
			StartCoroutine (this.SmoothMovement (end));
		}
		return true;
	}

	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainDistance = (this.transform.localPosition - end).sqrMagnitude;

		while (sqrRemainDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (this.transform.localPosition, end, 50 * Time.deltaTime);

			this.transform.localPosition = newPosition;
			sqrRemainDistance = (this.transform.localPosition - end).sqrMagnitude;

			yield return null;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
