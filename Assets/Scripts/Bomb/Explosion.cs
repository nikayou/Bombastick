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
  private Vector2 rightLine;
  private Vector2 leftLine;
  private Vector2 topLine;
  private Vector2 bottomLine;

  void Start ()
  {
    range += 0.475f;
    mask = 1 << mask;
    Vector2 two = Vector2.one * 2f;
    leftLine = -Vector2.right * scale / 2f + two;
    bottomLine = -Vector2.up * scale / 2f + two;
    rightLine = Vector2.right * scale / 2f - two;
    topLine = Vector2.up * scale / 2f - two;
    audio.PlayOneShot (sounds [Random.Range (0, sounds.Length)]);
    if (GameObject.Find ("MatchController").GetComponent<DeathMatch> () != null) {
      deathMatch = true;
    }
    Destroy (transform.parent.gameObject, explosionTime);
    Raycasting ();
  }

  void Raycasting () {
    RaycastForPlayers (Vector2.up, leftLine);
    RaycastForPlayers (Vector2.up, rightLine);
    RaycastTo (Vector2.up);    
    RaycastForPlayers (-Vector2.up, leftLine);
    RaycastForPlayers (-Vector2.up, rightLine);
    RaycastTo (-Vector2.up);
    RaycastForPlayers (Vector2.right, topLine);
    RaycastForPlayers (Vector2.right, bottomLine);
    RaycastTo (Vector2.right);
    RaycastForPlayers (-Vector2.right, topLine);
    RaycastForPlayers (-Vector2.right, bottomLine);
    RaycastTo (-Vector2.right);
  }

  void RaycastTo (Vector2 direction)
  {
    float distance = range * scale;
    RaycastHit2D [] hits = Physics2D.RaycastAll (transform.position, direction, distance, mask);
    Debug.DrawLine (transform.position, transform.position + ((Vector3)direction * distance), Color.green);
    foreach (RaycastHit2D hit in hits) {
      if (hit.collider.gameObject.tag == "Block") {
        break;
      } else {
        // visual effect of fire
       if (hit.collider.gameObject.tag == "Destructable") {
          DestroyTile (hit.collider.gameObject);
          break;
        } else if (hit.collider.gameObject.tag == "Player") {
          KillPlayer (hit.collider.gameObject);
        } 
      }
    }
  }

  void RaycastForPlayers (Vector2 direction, Vector2 shift)
  {
    float distance = range * scale;
    Vector2 origin = (Vector2)transform.position + shift;
    RaycastHit2D [] hits = Physics2D.RaycastAll (origin, direction, distance, mask);
    Debug.DrawLine (origin, origin + (direction * distance), Color.cyan);
    foreach (RaycastHit2D hit in hits) {
      if (hit.collider.gameObject.tag == "Block") {
        break;
      } else if (hit.collider.gameObject.tag == "Destructable") {
        break;
      } else if (hit.collider.gameObject.tag == "Player") {
          KillPlayer (hit.collider.gameObject);
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

}
