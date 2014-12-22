using UnityEngine;
using System.Collections;

public class MatchLauncher : MonoBehaviour {

  public GameObject matchController;
  public GameObject playerPrefab;
  private Vector3 [] positionForPlayer;

  void Start () {
	GameObject matchSettingsHolder; 
	matchSettingsHolder = GameObject.FindGameObjectWithTag("GameController");
	MatchSettings matchSettings = matchSettingsHolder.GetComponent<MatchSettings>(); 
    SetMatch (matchSettings.matchType, matchSettings.duration);
    LoadMap (matchSettings.levelName);
	SetPlayers (matchSettings.playersColors);
	Destroy (matchSettingsHolder);
    Destroy(this);
  }

  void SetMatch (MatchType matchType, float duration) {
    if (matchType != MatchType.TIMED) {
      Destroy(matchController.GetComponent<TimedMatch>());
    } else {
      TimedMatch tm = matchController.GetComponent<TimedMatch>();
      tm.Reset(duration);
    }
    if (matchType != MatchType.TARGET) {
      Destroy(matchController.GetComponent<TargetMatch>());
    } else {
      matchController.GetComponent<TargetMatch>().Reset(duration);
    }
    if (matchType != MatchType.LAST_MAN) {
      Destroy(matchController.GetComponent<LastManMatch>());
    } else {
      matchController.GetComponent<LastManMatch>().Reset(duration);
    }
    if (matchType != MatchType.DEATH) {
      Destroy(matchController.GetComponent<DeathMatch>());
    } else {
      matchController.GetComponent<DeathMatch>().Reset(duration);
    }
  }	

  void LoadMap (string name) {
    Tilemap tilemap = new Tilemap (WWW.EscapeURL ("Levels/"+name));
    Tileset tileset = new Tileset (WWW.EscapeURL ("Levels/Tilesets/"+tilemap.GetTileset()));
    Level lvl = GameObject.FindGameObjectWithTag("Level").GetComponent<Level>();
    lvl.tileset = tileset;
    lvl.map = tilemap;
    lvl.Create();
    Destroy(lvl);
  }

  void SetPlayers (Color [] colors) {
		for (int i = 0; i < colors.Length; i++) {
			if (colors[i].a == 0) {
				// no color for this player = no color
				continue;
			} else {
				GameObject p = Instantiate(playerPrefab) as GameObject;
				PlayerController pc = p.GetComponent<PlayerController>();
				pc.color = colors[i];
				p.transform.position = positionForPlayer[i];
			}
		}
  }

	
}
