using UnityEngine;
using System.Collections;

public class MatchLauncher : MonoBehaviour {

  public GameObject matchController;
  public GameObject playerPrefab;
  private Vector3 [] spawnPoint;

  void Start () {
	GameObject matchSettingsHolder; 
	matchSettingsHolder = GameObject.FindGameObjectWithTag("GameController");
	if (matchSettingsHolder != null) {
		MatchSettings matchSettings = matchSettingsHolder.GetComponent<MatchSettings>(); 
    	SetMatch (matchSettings.matchType, matchSettings.duration);
    	LoadMap (matchSettings.levelName);
		SetPlayers (matchSettings.playersColors);
	} else {
			SetMatch (MatchType.TIMED, 180f);
			LoadMap ("classic1.xml"); //TODO: load default map
			SetPlayers (new Color[]{new Color(1,0,0,1), new Color(0,1,0,1), new Color(1,0,0,0), new Color(1,0,0,0)});
	}
	//TODO: also set camera, considering border
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
	// computing spawn points
		float halfTileSize = tileset.GetTileSize() / 2f;
		float w = tilemap.GetWidth() * halfTileSize; // w*(s/2) is the same as (w*s)/2
		float h = tilemap.GetHeight() * halfTileSize;
		float upper = h - halfTileSize;
		float right = w - halfTileSize;
		spawnPoint = new Vector3[4];
		spawnPoint[0] = new Vector3(halfTileSize, upper, 0);
		spawnPoint[1] = new Vector3(right, halfTileSize, 0);
		spawnPoint[2] = new Vector3(halfTileSize, halfTileSize, 0);
		spawnPoint[3] = new Vector3(right, upper, 0);
	//
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
				p.transform.position = spawnPoint[i];
			}
		}
  }

	
}
