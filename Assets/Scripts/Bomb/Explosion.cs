using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]
[RequireComponent (typeof(AudioSource))]

public class Explosion : MonoBehaviour
{

  public float explosionTime = 1.0f;
  bool deathMatch = false;
  public AudioClip[] sounds;
  public float scale = 64f;
  public float range = 1f;
  public int mask = 8;

  void Start ()
  {
    mask = 1 << mask;
    audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
    if (GameObject.Find ("MatchController").GetComponent<DeathMatch> () != null) {
      deathMatch = true;
    }
    Destroy (transform.parent.gameObject, explosionTime);
  }

  void Update ()
  {
    //TODO: more rays, those only start from the center, we also need upper/lower
    //TODO: in that case, make the bomb spawn in the middle of the tile
    RaycastTo (Vector2.up);
    RaycastTo (-Vector2.up);
    RaycastTo (Vector2.right);
    RaycastTo (-Vector2.right);
  }

  void RaycastTo (Vector2 direction)
  {
    RaycastHit2D [] hits = Physics2D.RaycastAll (transform.position, direction, range * scale, mask);
    Debug.DrawLine (transform.position, transform.position + ((Vector3)direction * range * scale), Color.green);
    foreach (RaycastHit2D hit in hits) {
      Debug.Log ("hit " + hit.collider.gameObject.name);
      if (hit.collider.gameObject.tag != "Block") {
        // visual effect of fire
        if (hit.collider.gameObject.tag == "Player") {
          // TODO: check if deads can be killed
          KillPlayer (hit.collider.gameObject);
        } else if (hit.collider.gameObject.tag == "Destructable") {
          DestroyTile (hit.collider.gameObject);
        }
      }
    }
  }

  void KillPlayer (GameObject player)
  {
    player.SendMessage ("SetLife", false);
    if (deathMatch) { 
      // TODO: suicide and kill count
      player.SendMessage ("AddScore", -1);
    }
  }

  void DestroyTile (GameObject tile)
  {
    Destroy (tile);
  }

  /*
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
  }*/

}
