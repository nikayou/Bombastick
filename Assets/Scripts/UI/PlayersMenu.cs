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
  public GameObject [] playersAvatars;

  void Awake () {
    colors = new Color [4];
	status = new playerState[4];
    myManager = GetComponent<MenuManager>();
  }

  void Start () {
    for (int i = 0; i < 4; i++) {
      colors[i] = defaultColors[i];
	  status[i] = playerState.ABSENT;
	  ActivateCharacter(i, false);
	  playersAvatars[i].SetActive(true);
    }
  }

  void OnEnable () {
		Debug.Log ("nable");
		ShowCharacters (true);	
  }

  void OnDisable () {
		ShowCharacters (false);
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
		if (Input.GetButtonDown ("Cancel1")) {
			UpdatePlayerAfterBack(0);
		} 
		if (Input.GetButtonDown ("Cancel2")) {
			UpdatePlayerAfterBack(1);
		} 
		if (Input.GetButtonDown ("Cancel3")) {
			UpdatePlayerAfterBack(2);
		} 
		if (Input.GetButtonDown ("Cancel4")) {
			UpdatePlayerAfterBack(3);
		} 
  }

  void UpdatePlayerAfterOK (int i) {
		switch (status [i]) {
		case playerState.ABSENT:
			status[i] = playerState.JOINED;
			ActivateCharacter(i, true);
			break;
		case playerState.JOINED:
			status[i] = playerState.CONFIRMED;
			break;
		case playerState.CONFIRMED:
			TryConfirm();
			break;
		}
  }

void UpdatePlayerAfterBack (int i) {
	switch (status [i]) {
		case playerState.ABSENT:
			TryBack();
			break;
		case playerState.JOINED:
			status[i] = playerState.ABSENT;
			ActivateCharacter(i, false);
			break;
		case playerState.CONFIRMED:
			status[i] = playerState.JOINED;
			break;
	}
  }

 /*
  * when a player has already validated his choice, pressing "Fire" will try to jump to the 
  * next menu. It has to be done only if there isn't another player trying to choose. 
  */
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
			//ShowCharacters (false);
		}
  }

  void TryBack () {
		foreach (playerState ps in status) {
			if (ps == playerState.JOINED || ps == playerState.CONFIRMED)
				return;
		}
		// here, no player has joined
		myManager.ChangeMenuState (MenuState.MAIN);
		//ShowCharacters (false);
  }

  void ActivateCharacter (int i, bool active = true) {
		playersAvatars [i].GetComponent<RandomRotation>().enabled = active;
		Color c = colors [i];
		if (!active) {
			c = GUIUtils.Darker (c);
		}
		playersAvatars [i].renderer.material.color = c;
  }

  void ShowCharacters (bool show) {
		foreach (GameObject p in playersAvatars) {
			p.SetActive(show);
		}
  }

}
