using UnityEngine;
using System.Collections;

public class PlayersMenu : Menu {

  enum playerState {
		ABSENT,
		JOINED,
		CONFIRMED
	};
	
	public Color [] possibleColors;

	private class PlayerCharacter {
		public int colorIndex;
		public playerState status;
		public GameObject avatar;
		public GameObject confirmObject;
		public RandomRotation rotationScript;

		public PlayerCharacter() {}
		public void Colorise (Color c) {
			if (status == playerState.ABSENT)
				c = GUIUtils.Darker (c);
			avatar.renderer.material.color = c;
		}
		// return TRUE if pressed OK while confirmed, to go to next screen
		public bool PressOK () {
			switch (status) {
			case playerState.ABSENT:
				rotationScript.enabled = true;
				status = playerState.JOINED;
				break;
			case playerState.JOINED:
				rotationScript.enabled = false;
				status = playerState.CONFIRMED;
				confirmObject.SetActive(true);
				break;
			case playerState.CONFIRMED:
				return true;
			}
			return false;
		}
		// return FALSE if pressed BACK while absent, to go to previous screen
		public bool PressBack () {
			switch (status) {
			case playerState.ABSENT:
				return true;
			case playerState.JOINED:
				status = playerState.ABSENT;
				rotationScript.enabled = false;
				break;
			case playerState.CONFIRMED:
				status = playerState.JOINED;
				rotationScript.enabled = true;
				confirmObject.SetActive(false);
				break;
			}
			return false;
		}

	};

  private MenuManager myManager;
  private PlayerCharacter [] players;

	void Awake () {
		myManager = GetComponent<MenuManager>();
		players = new PlayerCharacter[4];
		for (int i = 0; i < 4; i++) {
			players [i] = new PlayerCharacter ();
			players [i].colorIndex = i;
			players [i].status = playerState.ABSENT;
			GameObject playerObject = GameObject.Find("Player"+(i+1));
			players [i].avatar = playerObject.transform.Find ("Avatar").gameObject;
			players [i].confirmObject = playerObject.transform.FindChild ("Confirm").gameObject;
			players [i].rotationScript = players [i].avatar.GetComponent<RandomRotation>();
			players [i].Colorise (GUIUtils.Darker(possibleColors[i]));
		}

	}

	void OnEnable () {
		ShowCharacters (true);	
	}
	
	void OnDisable () {
		ShowCharacters (false);
	}
	
	void ShowCharacters (bool show) {
		foreach (PlayerCharacter p in players) {
			p.avatar.SetActive(show);
		}
	}

	void Update () {
		for (int i = 0; i < 4; i++) {
			int j = i + 1;
			if (Input.GetButtonDown ("Fire"+j)) {
				if (players[i].PressOK())
					TryNext ();
				players[i].Colorise (possibleColors[players[i].colorIndex]);
			} 
			if (Input.GetButtonDown ("Cancel"+j)) {
				if (players[i].PressBack())
					TryBack ();
				players[i].Colorise (possibleColors[players[i].colorIndex]);
			} 
			if (players[i].status == playerState.JOINED) {
				float y = Input.GetAxis ("Vertical" + j);
				if (y != 0) {
					Debug.Log ("\nold index: "+players[i].colorIndex);
					if (y < 0) {
						players[i].colorIndex = NextColorIndexFrom(players[i].colorIndex, true);
					} else {
						// y > 0
						players[i].colorIndex = NextColorIndexFrom(players[i].colorIndex, false);
					}
					Debug.Log ("new index: "+players[i].colorIndex);
					players[i].Colorise (possibleColors[players[i].colorIndex]);
				}
			}

		}
	}

	void TryNext () {
		int nbPlayers = 0;
		foreach (PlayerCharacter p in players) {
			playerState ps = p.status;
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
		foreach (PlayerCharacter p in players) {
			playerState ps = p.status;
			if (ps == playerState.JOINED || ps == playerState.CONFIRMED)
				return;
		}
		// here, no player has joined
		myManager.ChangeMenuState (MenuState.MAIN);
		//ShowCharacters (false);
	}

	// pre-condition : need at least 4 possible colors
	int NextColorIndexFrom (int i, bool up) {
		// check the next color that is not already taken
		int origin = i;
		bool taken = true;
		int max = possibleColors.Length-1;
		int [] takenColors = new int[3];
		for (int j = 0; j < 3; j++) {
			if (j == origin && j < 2)
				j++;
			takenColors[j] = players[j].colorIndex;
		}
		do {
			if (up) {
				i += 1;
				if (i >= max)
					i = 0;
			} else {
				i -= 1;
				if (i < 0)
					i = max;
			}
		} while (i == takenColors[0] || i == takenColors[1] || i == takenColors[2]);
		return i;
	}
	
}
