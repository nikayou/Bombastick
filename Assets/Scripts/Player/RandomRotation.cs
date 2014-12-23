using UnityEngine;
using System.Collections;

public class RandomRotation : MonoBehaviour
{

  private float target = 0f;
  public float refresh = 2.0f;
  private float timer = 0f;
  public float smoothness = 1.0f;

  void Start ()
  {
    timer = refresh;
  }

  // Update is called once per frame
  void Update ()
  {
    timer += Time.deltaTime;
    if (timer >= refresh) {
      timer = 0f;
      target = Random.Range (0f, 359f);
    }
    float newAngle = Mathf.LerpAngle (transform.rotation.z, target, smoothness * Time.deltaTime);
    transform.Rotate (Vector3.forward, newAngle);
  }

}
