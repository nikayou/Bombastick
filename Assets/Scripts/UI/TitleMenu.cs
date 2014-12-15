using UnityEngine;
using System.Collections;

public class TitleMenu : Menu {

  public float introTime = 3.0f;
  private float timer = 0f;
  private bool introed = false;
  private MenuManager myManager;

  void Awake () {
    myManager = GetComponent<MenuManager>();
  }

  void Update () {
    if (!introed) {
      timer += Time.deltaTime;
      if (timer >= introTime)
	introed = true;
    } else {
      if (Input.anyKey) {
	      myManager.ChangeMenuState (MenuState.MAIN);
		  Destroy (this);
      }
    }
  }

  void OnGUI () {
    if (introed) {
      GUI.Label (GUIUtils.CenteredNormal(.5f, .6f, .3f, .1f), "Press any key", skin.label);
    }
  }

}
