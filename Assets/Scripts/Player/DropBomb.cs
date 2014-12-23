using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
[RequireComponent (typeof(AudioSource))]

public class DropBomb : MonoBehaviour
{

  public GameObject bombPrefab;
  public float cooldown = 4.0f;
  public bool canBomb = true;
  private PlayerController myController;
  public AudioClip sound;

  void Awake ()
  {
    myController = GetComponent<PlayerController> ();
  }

  // Update is called once per frame
  void Update ()
  {
    if (canBomb && Input.GetButtonDown ("Fire" + myController.GetID ())) {
      GameObject newBomb = Instantiate (bombPrefab) as GameObject;
      audio.PlayOneShot(sound);
      newBomb.transform.position = transform.position;
      //newBomb.transform.parent = transform;
      newBomb.transform.localScale = transform.lossyScale;
      StartCoroutine (SetCooldown ());
    }
  }

  IEnumerator SetCooldown ()
  {
    canBomb = false;
    yield return new WaitForSeconds (cooldown);
    canBomb = true;
  }

}
