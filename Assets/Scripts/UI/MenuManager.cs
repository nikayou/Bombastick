using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

  public MenuState currentState = MenuState.TITLE;
  private Menu [] states;

  void Awake () {
    states = new Menu [(int)MenuState.NB];
    states[(int)MenuState.TITLE] = GetComponent<TitleMenu>();
    states[(int)MenuState.MAIN] = GetComponent<MainMenu>();
    states[(int)MenuState.PLAYERS] = GetComponent<PlayersMenu>();
    states[(int)MenuState.MATCH] = GetComponent<MatchMenu>();
    states[(int)MenuState.SETTINGS] = GetComponent<SettingsMenu>();
    states[(int)MenuState.LEVEL] = GetComponent<LevelMenu>();
  }

  void Start () {
    ChangeMenuState (currentState);
  }

  void DisableAll () {
    foreach (Menu ms in states) {
      // TODO: delete the test
      if (ms != null)
	ms.enabled = false;
    }
  }

  public void ChangeMenuState (MenuState ms) {
    currentState = ms;
    DisableAll ();
    states[(int)currentState].enabled = true;
  }

}
