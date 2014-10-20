using UnityEngine;
using System.Collections;

public class MatchMenu : Menu {

  private MenuManager myManager;
  private MatchType matchType = MatchType.TIMED;
  private float duration = 90.0f;
  private float subTitleY = .5f;
  private MatchOptions matchOptions;

  void Awake () {
    myManager = GetComponent<MenuManager>();
  }

  void Start () {
    GameObject optionsHolder = GameObject.FindGameObjectWithTag("GameController");
    DontDestroyOnLoad(optionsHolder);
    matchOptions = optionsHolder.GetComponent<MatchOptions>();
  }

  void OnGUI () {
    GUI.Label (GUIUtils.CenteredNormal(.5f, .1f, .4f, .1f), "Match options", skin.label);
    ModeGUI ();
    // change mode
    if (GUI.Button (GUIUtils.CenteredNormal(.3f, subTitleY, .1f, .1f), "<", skin.button) ) {
      int newMode = (int)(matchType)-1;
      if (newMode < 0)
	newMode = ((int)MatchType.NB)-1;
      matchType = (MatchType)(newMode);
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.7f, subTitleY, .1f, .1f), ">", skin.button) ) {
      int newMode = (int)(matchType)+1;
      if (newMode >= (int)(MatchType.NB))
	newMode = 0;
      matchType = (MatchType)(newMode);
    }
    // next & previous
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .8f, .15f, .08f), "Play", skin.button)) {
      LaunchMatch ();
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .9f, .15f, .08f), "Back", skin.button)) {
      myManager.ChangeMenuState (MenuState.PLAYERS);
    }
  }

  void ModeGUI () {
    switch (matchType) {
    case MatchType.TIMED: 
      TimedGUI();
      break;
    case MatchType.TARGET: 
      TargetGUI();
      break;
    case MatchType.LAST_MAN: 
      LastManGUI();
      break;
    case MatchType.DEATH: 
      DeathGUI();
      break;
    default:
      Debug.Log ("Error: incorrect match type");
      break;
    }
  }

  void TimedGUI () {
    SubTitle ("Timed match");
    DurationGUI ();
   
  }

  void TargetGUI () {
    SubTitle ("Target match");
    DurationGUI ();
  }

  void LastManGUI () {
    SubTitle ("Last man match");
    DurationGUI ();
  }

  void DeathGUI () {
    SubTitle ("Death match");
    DurationGUI ();
  }

  void SubTitle (string subtitle) {
    GUI.Label (GUIUtils.CenteredNormal(.5f, subTitleY, .3f, .1f), subtitle, skin.label);
  }

  void DurationGUI () {
    float roundedDuration = Mathf.Round(duration);
    string msg = "Duration : "+roundedDuration;
    GUI.Label (GUIUtils.CenteredNormal(.5f, .65f, .3f, .1f), msg, skin.label);
  }

  void LaunchMatch () {
    matchOptions.duration = duration;
    matchOptions.mode = matchType;
    Application.LoadLevel(1);
  }

}
