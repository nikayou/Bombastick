using UnityEngine;
using System.Collections;

public class MenuLinesControl : MonoBehaviour {

	Transform horizontal;
	Transform vertical;
	public float smoothness = 1.0f;
	private float upperLimit;
	private float rightLimit;

	void Start () {
		horizontal = transform.FindChild("Horizontal");
		vertical = transform.FindChild("Vertical");
		upperLimit = Screen.height * 0.9f;
		rightLimit = Screen.width * 0.9f;
	}

	public void SetTo (float x, float y) {
		Debug.Log ("setting to "+x+","+y);
		StartCoroutine(TransitionTo(x,y));
	}

	public void RandomPosition () {
		SetTo (Random.Range(32.0f, rightLimit), Random.Range(32.0f, upperLimit));
	}

	IEnumerator TransitionTo (float x, float y) {
		Vector3 hTarget = new Vector3(320, y, 0);
		Vector3 vTarget = new Vector3(x, 240, 0);
		while (horizontal.localPosition.y != y && vertical.localPosition.x != x) {
			float fixedSmoothness = smoothness * Time.deltaTime;
			if (horizontal.localPosition.y != y) {
				horizontal.localPosition = Vector3.Lerp(horizontal.localPosition, hTarget, fixedSmoothness);
			}
			if (vertical.localPosition.x != x) {
				vertical.localPosition = Vector3.Lerp(vertical.localPosition, vTarget, fixedSmoothness);
			}
			yield return null;
		}
	}

}
