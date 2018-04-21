using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.Tween;

public class InitiativeItem : MonoBehaviour {

	public float moveTime = 0.3f;
	protected Rect parentRect;

	protected InitiativePanel panel;

	void Awake () {
		this.panel = this.transform.parent.gameObject.GetComponent<InitiativePanel> ();
	}

	public bool WasActive()
	{
		return gameObject.activeSelf;
	}

	public bool IsVisible(int position) {
		float start = this.panel.GetRenderStartPosition ();
		float x = start + (position * InitiativePanel.itemWidth);
		if (x > start + this.panel.rect.width + InitiativePanel.itemWidth || x < start - InitiativePanel.itemWidth) {
			this.gameObject.SetActive (false);
			return false;
		}
		this.gameObject.SetActive (true);
		return true;
	}



	public Vector3 FindLisstEndPosition()
	{
		float start = this.panel.GetRenderStartPosition ();
		float x = start + panel.rect.width + InitiativePanel.itemWidth;
		return new Vector3 (x, - this.panel.rect.height / 2, 0);
	}

	public void SetPosition(Vector3 position)
	{
		this.transform.localPosition = position;
	}

	public void Animate(Vector3 end, System.Action<ITween<Vector3>> callback = null) {
		//StartCoroutine (this.SmoothMovement (end));
		this.gameObject.Tween("MoveNumber" + this.name, this.transform.localPosition, end, moveTime, TweenScaleFunctions.CubicEaseIn, 
			(t) => {
				this.transform.localPosition = t.CurrentValue;
			}, callback);
	}
}
