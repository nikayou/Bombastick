using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class Countdown : MonoBehaviour
{

  public float time = 3.0f;
  public Color baseColor = Color.black;
  public Color targetColor = Color.red;
  private float timer;
  private Vector3 destination;
  public float speed = 1.5f;

// Use this for initialization
  void Start ()
  {
    destination = new Vector3(Mathf.RoundToInt(transform.localPosition.x), Mathf.RoundToInt(transform.localPosition.y), 0);
    timer = 0f;
    renderer.material.color = baseColor;
  }

// Update is called once per frame
  void Update ()
  {
    timer += Time.deltaTime;
    renderer.material.color = Color.Lerp (baseColor, targetColor, timer / time);
    // make the bomb head to the center of its tile
    transform.localPosition = Vector3.Lerp (transform.localPosition, destination, speed * Time.deltaTime);
    if (timer >= time) {
      Explode ();
    }
  }

  void Explode ()
  {
    audio.Stop();
    transform.Find ("Explosion").gameObject.SetActive (true);
  }


}
