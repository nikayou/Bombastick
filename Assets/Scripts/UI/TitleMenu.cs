using UnityEngine;
using System.Collections;

public class TitleMenu : Menu {

  public string title = "I am the title";
  private float introTime = 3.0f;
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
      }
    }
  }

  void OnGUI () {
    GUI.Label (GUIUtils.CenteredNormal(.5f, .2f, 1f, .1f), title, skin.GetStyle("Title"));
    if (introed) {
      GUI.Label (GUIUtils.CenteredNormal(.5f, .6f, .3f, .1f), "Press any key", skin.label);
    }
  }

}
