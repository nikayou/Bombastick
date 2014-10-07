using UnityEngine;
using System.Collections;

public class AutoRotation : MonoBehaviour {

public float speed = 90.0f;

	// Update is called once per frame
	void Update () {
	transform.Rotate (0f, 0f, speed * Time.deltaTime);
	}
}