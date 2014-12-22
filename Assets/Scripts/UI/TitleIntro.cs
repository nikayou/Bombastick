using UnityEngine;
using System.Collections;

public class TitleIntro : MonoBehaviour {

  public float introTime = 3.0f;
  public MenuManager menuManager;
  public Menu mainMenu;
  private float timer = 0f;
  private bool introed = false;
  public GameObject displayedObject;
	public AudioSource audioSource;
	public AudioClip audioClip;

  void Update () {
    if (!introed) {
      timer += Time.deltaTime;
      if (timer >= introTime) {
		if (timer >= introTime+1) {
			introed = true;
		} else {
		    displayedObject.SetActive(true);
		}
	  }
    } else {
      if (Input.anyKey) {
		  displayedObject.SetActive(false);
		  menuManager.ShowMenu(mainMenu);
		  audioSource.PlayOneShot(audioClip);
		  Destroy (this);
      }
    }
  }


}
