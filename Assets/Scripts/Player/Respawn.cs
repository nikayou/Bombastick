using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]

public class Respawn : MonoBehaviour
{

  public float respawnTime = 4.0f;
  public Respawner respawner;
  private PlayerController myController;
  private bool launched = false;

  void Awake ()
  {
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
    RespawnAt (respawner.GetSpawnFor(myController.GetID()));
  }

  void RespawnAt (Vector2 location)
  {
    transform.localPosition = location;
    myController.SetLife (true);
    launched = false;
    this.enabled = false;
  }

}
