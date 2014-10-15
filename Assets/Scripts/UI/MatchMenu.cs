using UnityEngine;
using System.Collections;

public class MatchMenu : Menu {

  private MenuManager myManager;

  void Awake () {
    myManager = GetComponent<MenuManager>();
  }


  void OnGUI () {
    GUI.Label (GUIUtils.CenteredNormal(.5f, .1f, .4f, .1f), "Match options", skin.label);
     if (GUI.Button (GUIUtils.CenteredNormal(.5f, .8f, .15f, .08f), "Play", skin.button)) {
       Application.LoadLevel(1);
      }
     if (GUI.Button (GUIUtils.CenteredNormal(.5f, .9f, .15f, .08f), "Back", skin.button)) {
	myManager.ChangeMenuState (MenuState.PLAYERS);
      }
  }

}
