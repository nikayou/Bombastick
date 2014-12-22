using UnityEngine;
using System.Collections;

public class CameraFix : MonoBehaviour {

	// Use this for initialization
	void Start () {
		float halfH = Screen.height / 2f;
		Camera.main.orthographicSize = halfH;
		Camera.main.transform.position = new Vector3(Screen.width/2f, halfH, 0f);
	}
	
}
