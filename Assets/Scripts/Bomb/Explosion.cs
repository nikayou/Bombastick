using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(AudioSource))]

public class Explosion : MonoBehaviour
{

  public float explosionTime = 1.0f;
  bool deathMatch = false;
  public AudioClip[] sounds;

  void Start ()
  {
    audio.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
    if (GameObject.Find ("MatchController").GetComponent<DeathMatch> () != null) {
      deathMatch = true;
    }
    Destroy (transform.parent.gameObject, explosionTime);
  }

  void OnTriggerEnter2D (Collider2D other)
  {
    if (other.transform.tag == "Destructable") {
      Destroy (other.gameObject);
    } else if (other.transform.tag == "Player") {
      //      Destroy(other.gameObject);
      //      Camera.main.GetComponent<EndMatch>().End();
      other.gameObject.SendMessage ("SetLife", false);
      if (deathMatch) { 
        // TODO: suicide and kill count
        other.gameObject.SendMessage ("AddScore", -1);
      }
 
    }
  }

}
