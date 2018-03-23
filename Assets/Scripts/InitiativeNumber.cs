using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using DigitalRuby.Tween;

public class InitiativeNumber : MonoBehaviour {

	public int number;
	public float moveTime = 0.3f;

	protected InitiativePanel panel;

	void Awake () {
		this.panel = this.transform.parent.gameObject.GetComponent<InitiativePanel> ();
	}

	public bool CalculatePosition(int position, bool isAnimated = true)
	{
		float start = this.panel.GetRenderStartPosition ();
		float x = start + (position * InitiativePanel.itemWidth);
		if (x > start + this.panel.rect.width + InitiativePanel.itemWidth || x < start - InitiativePanel.itemWidth) {
			this.gameObject.SetActive (false);
			return false;
		}
		bool wasActive = this.gameObject.activeSelf;
		this.gameObject.SetActive (true);
		Vector3 end = new Vector3 (x, - this.panel.rect.height / 2, 0);
		if (!isAnimated || !wasActive) {
			this.transform.localPosition = end;
		} else {
			//StartCoroutine (this.SmoothMovement (end));
			this.gameObject.Tween("MoveNumber" + this.number, this.transform.localPosition, end, moveTime, TweenScaleFunctions.CubicEaseIn, (t) =>
				{
					this.transform.localPosition = t.CurrentValue;
				}, (t2) => { });
		}
		return true;
	}

	protected IEnumerator SmoothMovement(Vector3 end)
	{
		float sqrRemainDistance = (this.transform.localPosition - end).sqrMagnitude;
		float fullDistance = (this.transform.localPosition - end).magnitude; 

		while (sqrRemainDistance > float.Epsilon) {
			Vector3 newPosition = Vector3.MoveTowards (this.transform.localPosition, end, fullDistance * Time.deltaTime / moveTime);

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
