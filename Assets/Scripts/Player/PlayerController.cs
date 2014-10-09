using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

  public int playerID = 1;		      
  public Color color;
  private Respawn respawnScript;
  private GameObject myEyes;
  private float score = 0f;
  private bool isOwner = false;
  private DropBomb dropBombScript;

  // Use this for initialization
  void Awake () {
    renderer.material.color = color;
    myEyes = transform.Find("Eyes").gameObject;
    respawnScript = GetComponent<Respawn>();
  }

  void Start () {
    dropBombScript = GetComponent<DropBomb>();
  }

  public int GetID () {
    return playerID;
  }

  public void SetLife (bool val = true) {
    respawnScript.enabled = !val;
    this.renderer.enabled = val;
    myEyes.renderer.enabled = val;
    this.collider2D.enabled = val;
    if (!val) {
      LoseObject ();
    }
  }

  public void PickObject (Transform what) {
    dropBombScript.enabled = false;
    what.parent = transform;
    what.transform.localPosition = Vector3.zero;
    what.transform.localScale /= 2;
    isOwner = true;
  }

  public void LoseObject () {
    isOwner = false;
    Transform obj = transform.Find("Star");
    if (obj) {
      obj.localScale *= 2;
      obj.parent = null;
    }    
    dropBombScript.enabled = true;
  }

  public bool IsOwner () {
    return isOwner;
  } 

  public float AddScore (float amount) {
    return (score += amount);
  }

  public float GetScore () {
    return score;
  } 

  public float SetScore (float value) {
    return (score = value);
  }

}
