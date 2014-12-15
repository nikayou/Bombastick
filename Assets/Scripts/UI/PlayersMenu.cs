using UnityEngine;
using System.Collections;

public class PlayersMenu : Menu {

  enum playerState {
		ABSENT,
		JOINED,
		CONFIRMED
	};

  private MenuManager myManager;
  private Color [] colors;
  public Color [] defaultColors;
  private playerState[] status;

  void Awake () {
    colors = new Color [4];
	status = new playerState[4];
    myManager = GetComponent<MenuManager>();
  }

  void Start () {
    for (int i = 0; i < 4; i++) {
      colors[i] = defaultColors[i];
			status[i] = playerState.ABSENT;
    }
  }

  void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			UpdatePlayerAfterOK(0);
		} 
		if (Input.GetButtonDown ("Fire2")) {
			UpdatePlayerAfterOK(1);
		}
		if (Input.GetButtonDown ("Fire3")) {
			UpdatePlayerAfterOK(2);
		}
		if (Input.GetButtonDown ("Fire4")) {
			UpdatePlayerAfterOK(3);
		}
  }

  void UpdatePlayerAfterOK (int i) {
		switch (status [i]) {
		case playerState.ABSENT:
			status[i] = playerState.JOINED;
			break;
		case playerState.JOINED:
			status[i] = playerState.CONFIRMED;
			break;
		case playerState.CONFIRMED:
			TryConfirm();
			break;
		}
  }

  void TryConfirm () {
		int nbPlayers = 0;
		foreach (playerState ps in status) {
			if (ps == playerState.JOINED) // player has joined but not choosen yet
				return;
			if (ps == playerState.CONFIRMED)
				nbPlayers++;
		}
		if (nbPlayers >= 2) {
			myManager.ChangeMenuState (MenuState.MATCH);
		}
  }

  void OnGUI () {
    GUI.Label (GUIUtils.CenteredNormal(.5f, .1f, .4f, .1f), "Players settings", skin.label);
    BoxFor (1, .25f, .25f);
    BoxFor (2, .75f, .25f);
    BoxFor (3, .25f, .75f);    
    BoxFor (4, .75f, .75f);    
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .45f, .15f, .08f), "Next", skin.button)) {
      myManager.ChangeMenuState (MenuState.MATCH);
      }
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .55f, .15f, .08f), "Back", skin.button)) {
	myManager.ChangeMenuState (MenuState.MAIN);
      }
      
  }

  void BoxFor (int j, float posX, float posY) {
		int i = j - 1;
		if (status [i] == playerState.ABSENT) {
			// darker color when nothing to do
			GUI.backgroundColor = GUIUtils.Darker(colors [i]);
		} else {
			GUI.backgroundColor = colors [i];
		}
    GUI.Box (GUIUtils.CenteredNormal(posX, posY, .5f, .5f), "Player"+j, skin.box);
  }

}
