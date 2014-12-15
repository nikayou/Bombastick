using UnityEngine;
using System.Collections;

public class PlayersMenu : Menu {

  enum playerState {
		ABSENT,
		JOINED,
		SELECTED,
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
		if (status [i] == playerState.ABSENT || status [i] == playerState.CONFIRMED) {
			// darker color when nothing to do
			GUI.backgroundColor = GUIUtils.Darker(colors [i]);
		} else {
			GUI.backgroundColor = colors [i];
		}
    GUI.Box (GUIUtils.CenteredNormal(posX, posY, .5f, .5f), "Player"+j, skin.box);
  }

}
