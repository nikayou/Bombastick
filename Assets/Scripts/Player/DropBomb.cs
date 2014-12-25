using UnityEngine;
using System.Collections;

[RequireComponent (typeof(PlayerController))]
[RequireComponent (typeof(AudioSource))]

public class DropBomb : MonoBehaviour
{

  public Transform level;
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
      newBomb.transform.parent = level;
      audio.PlayOneShot(sound);
      newBomb.transform.localPosition = transform.localPosition;
      newBomb.transform.localScale = transform.localScale;
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
