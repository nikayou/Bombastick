using UnityEngine;
using System.Collections;

public class SettingsMenu : Menu {

  public float speed = .5f;
  private float xPos = 1f;
  private bool introed = false;
  private MenuManager myManager;

  void Awake () {
    myManager = GetComponent<MenuManager>();
  }

  void Update () {
    if (!introed) {
      xPos -= speed * Time.deltaTime;
      if (xPos <= .5f) {
	xPos = .5f;
	introed = true;
      }
    }
  }

  void OnGUI () {
    GUI.Label (GUIUtils.CenteredNormal(.5f, .1f, .4f, .1f), "Settings", skin.label);
 if (GUI.Button(GUIUtils.CenteredNormal(.5f, .8f, .2f, .1f), "Back", skin.button) ) {
    myManager.ChangeMenuState (MenuState.MAIN);
    }
  }

}
