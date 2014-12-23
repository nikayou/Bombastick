using UnityEngine;
using System.Collections;

public class ObjectPick : MonoBehaviour
{

  PlayerController ownerController;

  void OnTriggerEnter2D (Collider2D other)
  {
    if (transform.parent == null && other.transform.tag == "Player") {
      transform.parent = other.transform;
      other.gameObject.SendMessage ("PickObject", transform);
    }

  }

}
