using UnityEngine;
using System.Collections;

public class MatchLauncher : MonoBehaviour
{

  public GameObject matchController;
  public GameObject level;
  public GameObject playerPrefab;
  public float playerScale = 0.75f;
  private Vector3[] spawnPoint;

  void Start ()
  {
    GameObject matchSettingsHolder; 
    matchSettingsHolder = GameObject.FindGameObjectWithTag ("GameController");
    if (matchSettingsHolder != null) {
      MatchSettings matchSettings = matchSettingsHolder.GetComponent<MatchSettings> (); 
      SetMatch (matchSettings.matchType, matchSettings.duration);
      LoadMap (matchSettings.levelName);
      SetPlayers (matchSettings.playersColors);
    } else {
      SetMatch (MatchType.TIMED, 180f);
      LoadMap ("classic1.xml"); //TODO: load default map
      SetPlayers (new Color[] {
        new Color (1, 0, 0, 1),
        new Color (0, 1, 0, 1),
        new Color (1, 0, 0, 0),
        new Color (1, 0, 0, 0)
      });
    }
    //TODO: also set camera, considering border
    Destroy (matchSettingsHolder);
    Destroy (this);
  }

  void SetMatch (MatchType matchType, float duration)
  {
    if (matchType != MatchType.TIMED) {
      Destroy (matchController.GetComponent<TimedMatch> ());
    } else {
      TimedMatch tm = matchController.GetComponent<TimedMatch> ();
      tm.Reset (duration);
    }
    if (matchType != MatchType.TARGET) {
      Destroy (matchController.GetComponent<TargetMatch> ());
    } else {
      matchController.GetComponent<TargetMatch> ().Reset (duration);
    }
    if (matchType != MatchType.LAST_MAN) {
      Destroy (matchController.GetComponent<LastManMatch> ());
    } else {
      matchController.GetComponent<LastManMatch> ().Reset (duration);
    }
    if (matchType != MatchType.DEATH) {
      Destroy (matchController.GetComponent<DeathMatch> ());
    } else {
      matchController.GetComponent<DeathMatch> ().Reset (duration);
    }
  }

  void LoadMap (string name)
  {
    Tilemap tilemap = new Tilemap (WWW.EscapeURL ("Levels/" + name));
    Tileset tileset = new Tileset (WWW.EscapeURL ("Levels/Tilesets/" + tilemap.GetTileset ()));
    // computing spawn points
    float w = tilemap.GetWidth () - 0.5f; // w*(s/2) is the same as (w*s)/2
    float h = tilemap.GetHeight () - 0.5f;
    spawnPoint = new Vector3[4];
    spawnPoint [0] = new Vector3 (0.5f, h, 0);
    spawnPoint [1] = new Vector3 (w, 0.5f, 0);
    spawnPoint [2] = new Vector3 (0.5f, 0.5f, 0);
    spawnPoint [3] = new Vector3 (w, h, 0);
    // setting camera
    level.transform.localScale = Vector3.one * tileset.GetTileSize ();
    Vector3 levelScale = level.transform.localScale;
    float halfTileSize = tileset.GetTileSize () / 2f;
    float camX = (tilemap.GetWidth () - 1f) * halfTileSize;
    float camY = (tilemap.GetHeight () + 1f) * halfTileSize;
    Camera.main.transform.position = new Vector3 (camX,
                                                  camY,
                                                  0);
    // orthographic size is the vertical size
    float orthoV = tileset.GetTileSize () * ((2 + tilemap.GetWidth ()) / 2f); // required size to display every row
    float orthoH = tileset.GetTileSize () * ((2 + tilemap.GetHeight ()) / 2f); // required size to display every column
    float fixedOrthoV = (orthoV * Camera.main.aspect < orthoV) ? orthoH * Camera.main.aspect : orthoV;
    //Camera.main.orthographicSize = Mathf.Max(orthoV, fixedOrthoV);
    Camera.main.orthographicSize = fixedOrthoV;
    // creating level
    //level.transform.position = new Vector3 (levelScale.x, -levelScale.y, 0f);
    Level lvl = level.GetComponent<Level> ();
    lvl.borderIndex = tileset.GetBorderIndex ();
    lvl.tileset = tileset;
    lvl.map = tilemap;
    lvl.Create ();
    Destroy (lvl);
  }

  void SetPlayers (Color[] colors)
  {
    for (int i = 0; i < colors.Length; i++) {
      if (colors [i].a == 0f) {
        // no color for this player = no player
        continue;
      } else {
        GameObject p = Instantiate (playerPrefab) as GameObject;
        PlayerController pc = p.GetComponent<PlayerController> ();
        pc.color = colors [i];
        p.transform.position = spawnPoint [i];
        p.transform.parent = level.transform;
        p.transform.localScale = Vector3.one * playerScale;
      }
    }
  }

 
}
