using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]

public class ExplosionVisual : MonoBehaviour
{

  public float explosionTime = 1.0f;
  public AudioClip sound;

  void Start ()
  {
    audio.PlayOneShot (sound);
    Destroy (transform.parent.gameObject, explosionTime);
  }

}
