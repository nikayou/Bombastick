using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]

public class Respawn : MonoBehaviour
{

  public float respawnTime = 4.0f;
  private Vector2 spawnLocation;
  private PlayerController myController;
  private bool launched = false;

  void Awake ()
  {
//TODO: remove those 2 and compute nicely
    spawnLocation = transform.position;
    this.enabled = false;
  }

// Use this for initialization
  void Start ()
  {
    myController = GetComponent<PlayerController> ();
  }

// Update is called once per frame
  void Update ()
  {
    if (!launched) {
      StartCoroutine (Launch ());
    }
  }

  IEnumerator Launch ()
  {
    launched = true;
    yield return new WaitForSeconds (respawnTime);
    RespawnAt (spawnLocation);
  }

  void RespawnAt (Vector2 location)
  {
    transform.position = location;
    myController.SetLife (true);
    launched = false;
    this.enabled = false;
  }

}
