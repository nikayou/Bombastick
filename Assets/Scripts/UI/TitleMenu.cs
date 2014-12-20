using UnityEngine;
using System.Collections;

// TODO: Delete after refactoring
public class TitleMenu : Menu {

  public float introTime = 3.0f;
  private float timer = 0f;
  private bool introed = false;
  public GameObject anyKeyObject;
  //private MenuManager myManager;

  void Awake () {
    //myManager = GetComponent<MenuManager>();
  }

  void Update () {
    if (!introed) {
      timer += Time.deltaTime;
      if (timer >= introTime) {
	    introed = true;
		anyKeyObject.SetActive(true);
	  }
    } else {
      if (Input.anyKey) {
				//myManager.ChangeMenuState (MenuState.MAIN);
		  anyKeyObject.SetActive(false);
		  Destroy (this);
      }
    }
  }


}
