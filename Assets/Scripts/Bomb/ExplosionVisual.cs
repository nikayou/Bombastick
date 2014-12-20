using UnityEngine;
using System.Collections;

public class ExplosionVisual : MonoBehaviour {

  public float explosionTime = 1.0f;
 
  void Start () {
    Destroy(transform.parent.gameObject, explosionTime);
  }

}
