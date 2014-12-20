using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

}

/**
public class MainMenu : Menu {

  private MenuManager myManager;

  void Awake () {
    myManager = GetComponent<MenuManager>();
  }

  void OnGUI () {
    // TODO : make that static  
    float width = .3f;
    float height = .1f;
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .4f, width, height), "Play", skin.button)) {
      myManager.ChangeMenuState (MenuState.PLAYERS);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .55f, width, height), "Settings", skin.button)) {
      myManager.ChangeMenuState (MenuState.SETTINGS);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .7f, width, height), "Level Editor", skin.button)) {
    Application.LoadLevel(2);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .85f, width, height), "Quit", skin.button)) {
      Application.Quit();
    }
    
  }

}
*/