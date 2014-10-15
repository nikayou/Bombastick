using UnityEngine;
using System.Collections;

public class MainMenu : Menu {

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
    // TODO : make that static  
    float width = .3f;
    float height = .1f;
    if (GUI.Button (GUIUtils.CenteredNormal(xPos, .4f, width, height), "Play", skin.button)) {
      myManager.ChangeMenuState (MenuState.PLAYERS);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(xPos, .55f, width, height), "Settings", skin.button)) {
      myManager.ChangeMenuState (MenuState.SETTINGS);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(xPos, .7f, width, height), "Level Editor", skin.button)) {
    Application.LoadLevel(2);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(xPos, .85f, width, height), "Quit", skin.button)) {
      Application.Quit();
    }
    
  }

}
