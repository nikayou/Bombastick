using UnityEngine;
using System.Collections;

public class CameraFix : MonoBehaviour
{

 // public float size = 240f;

  // Use this for initialization
  void Start ()
  {
    float halfHeight = Screen.height / 2f;
    Camera.main.orthographicSize = halfHeight;
    Camera.main.transform.position = new Vector3 (Screen.width / 2f, halfHeight, 0f);
    Destroy (this);
  }
 
}
