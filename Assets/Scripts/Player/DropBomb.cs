using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

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
    int id = myController.GetID ();
    if (canBomb && (Input.GetButtonDown ("Fire" + id) || XCI.GetButtonDown(XboxButton.A, id))) {
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
