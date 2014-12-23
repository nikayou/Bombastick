using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Respawn))]
[RequireComponent (typeof(DropBomb))]

public class PlayerController : MonoBehaviour
{

  public int playerID = 1;
  private Color _color;

  public Color color {
    get { return _color; }
    set {
      _color = value;
      Colorise ();
    }
  }

  private Respawn respawnScript;
  private GameObject myEyes;
  private float score = 0f;
  private bool isOwner = false;
  private DropBomb dropBombScript;

  // Use this for initialization
  void Awake ()
  {
    renderer.material.color = color;
    myEyes = transform.FindChild ("Eyes").gameObject;
  }

  void Start ()
  {
    respawnScript = gameObject.GetComponent<Respawn> ();
    dropBombScript = gameObject.GetComponent<DropBomb> ();
  }

  public int GetID ()
  {
    return playerID;
  }

  public void SetID (int i) {
    playerID = (i<0)?0:i;
  }

  public void SetLife (bool val)
  {
    this.renderer.enabled = val;
    myEyes.renderer.enabled = val;
    this.collider2D.enabled = val;
    dropBombScript.canBomb = val;
    respawnScript.enabled = !val;
    if (!val) {
      LoseObject ();
    }
  }

  public void PickObject (Transform what)
  {
    dropBombScript.enabled = false;
    what.parent = transform;
    what.transform.localPosition = Vector3.zero;
    what.transform.localScale /= 2;
    isOwner = true;
  }

  public void LoseObject ()
  {
    isOwner = false;
    Transform obj = transform.Find ("Star");
    if (obj) {
      obj.localScale *= 2;
      obj.parent = null;
    }    
    dropBombScript.enabled = true;
  }

  public bool IsOwner ()
  {
    return isOwner;
  }

  public float AddScore (float amount)
  {
    return (score += amount);
  }

  public float GetScore ()
  {
    return score;
  }

  public float SetScore (float value)
  {
    return (score = value);
  }

  public void Colorise ()
  {
    renderer.material.color = color;
  }

}
