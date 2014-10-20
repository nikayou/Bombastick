using UnityEngine;
using System.Collections;

public class MatchLauncher : MonoBehaviour {

  private GameObject optionsHolder;
  private MatchOptions matchOptions;
  private GameObject gameController; 

  void Start () {
    foreach (GameObject go in GameObject.FindGameObjectsWithTag("GameController")) {
      if (go.name == "GameController") {
	gameController = go;
	Debug.Log("found gamecontroller");
      } else {
	optionsHolder = go;
	Debug.Log("found options holder");
      }
	
    }
    DontDestroyOnLoad(optionsHolder);
    matchOptions = optionsHolder.GetComponent<MatchOptions>();
    SetMatchType (matchOptions.mode);
    Destroy(matchOptions);
    Destroy(optionsHolder);
    Destroy(this);
  }

  void SetMatchType (MatchType matchType) {
    if (matchType != MatchType.TIMED) {
      Destroy(gameController.GetComponent<TimedMatch>());
    } else {
      TimedMatch tm = gameController.GetComponent<TimedMatch>();
      tm.Reset(matchOptions.duration);
    }
    if (matchType != MatchType.TARGET) {
      Destroy(gameController.GetComponent<TargetMatch>());
    } else {
      gameController.GetComponent<TargetMatch>().Reset(matchOptions.duration);
    }
    if (matchType != MatchType.LAST_MAN) {
      Destroy(gameController.GetComponent<LastManMatch>());
    } else {
      gameController.GetComponent<LastManMatch>().Reset(matchOptions.duration);
    }
    if (matchType != MatchType.DEATH) {
      Destroy(gameController.GetComponent<DeathMatch>());
    } else {
      gameController.GetComponent<DeathMatch>().Reset(matchOptions.duration);
    }
     
  }	
	
}
