using UnityEngine;
using System.Collections;

public class ObjectPick : MonoBehaviour
{

  void OnTriggerEnter2D (Collider2D other)
  {
    if ((transform.parent == null || transform.parent.tag != "Player") && other.transform.tag == "Player") {
      transform.parent = other.transform;
      other.gameObject.SendMessage ("PickObject", transform);
    }

  }

}
