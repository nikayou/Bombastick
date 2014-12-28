using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchLauncher : MonoBehaviour
{

  public GameObject matchController;
  private MatchController control;
  public GameObject level;
  public GameObject playerPrefab;
  public Respawner respawner;
  public Transform uiCanvas;
  public GameObject star;
  public float playerScale = 0.75f;
  private Vector3[] spawnPoint;
  public MatchType defaultType = MatchType.TIMED;

  void Start ()
  {
    GameObject matchSettingsHolder; 
    matchSettingsHolder = GameObject.FindGameObjectWithTag ("GameController");
    if (matchSettingsHolder != null) {
      MatchSettings matchSettings = matchSettingsHolder.GetComponent<MatchSettings> (); 
      defaultType = matchSettings.matchType;
      SetMatch (matchSettings.matchType, matchSettings.duration);
      LoadMap (matchSettings.levelName);
      SetPlayers (matchSettings.playersColors);
    } else {
      SetMatch (defaultType, 180f);
      LoadMap ("classic1.xml"); //TODO: load default map
      SetPlayers (new Color[] {
        new Color (1, 1, 0, 1),
        new Color (0, 1, 1, 1),
        new Color (1, 0, 1, 1),
        new Color (0.5f, 0.5f, 0.5f, 1f)
      });
    }
    Destroy (matchSettingsHolder);
    Destroy (this);
  }

  void SetMatch (MatchType matchType, float duration)
  {
    GameObject matchPanel = uiCanvas.FindChild("MatchPanel").gameObject;
    Text modeText = matchPanel.transform.FindChild("TypeText").GetComponent<Text>();
    Text timeText = matchPanel.transform.FindChild("TimeText").GetComponent<Text>();
   if (matchType != MatchType.TIMED) {
      Destroy (matchController.GetComponent<TimedMatch> ());
    } else {
      modeText.text = "Best time match";
      control = matchController.GetComponent<TimedMatch> ();
    }
    if (matchType != MatchType.TARGET) {
      Destroy (matchController.GetComponent<TargetMatch> ());
    } else {
      modeText.text = "Target time match";
      control = matchController.GetComponent<TargetMatch> ();
    }
    if (matchType != MatchType.LAST_MAN) {
      Destroy (matchController.GetComponent<LastManMatch> ());
    } else {
      modeText.text = "Last man keeping";
      control = matchController.GetComponent<LastManMatch> ();
    }
    if (matchType != MatchType.DEATH) {
      Destroy (matchController.GetComponent<DeathMatch> ());
    } else {
      modeText.text = "DeathMatch";
      control = matchController.GetComponent<DeathMatch> ();
      Destroy (star);
    }
    control.timeLabel = timeText;
    control.enabled = true;
    control.Reset (duration);
  }

  void LoadMap (string name)
  {
    Tilemap tilemap = new Tilemap (WWW.EscapeURL ("Levels/" + name));
    Tileset tileset = new Tileset (WWW.EscapeURL ("Levels/Tilesets/" + tilemap.GetTileset ()));
    // computing spawn points
    float w = tilemap.GetWidth () - 1f; // w*(s/2) is the same as (w*s)/2
    float h = tilemap.GetHeight ();
    spawnPoint = new Vector3[4];
    spawnPoint [0] = new Vector3 (0f, 1f, 0);
    spawnPoint [1] = new Vector3 (w, h, 0);
    spawnPoint [2] = new Vector3 (0f, h, 0);
    spawnPoint [3] = new Vector3 (w, 1f, 0);
    respawner.AddRespawn(spawnPoint);
    // setting camera
    level.transform.localScale = Vector3.one * tileset.GetTileSize ();
    //Vector3 levelScale = level.transform.localScale;
    float halfTileSize = tileset.GetTileSize () / 2f;
    //float camX = (tilemap.GetWidth () - 1f) * halfTileSize;
    float camY = (tilemap.GetHeight () + 1f) * halfTileSize;
    float camX = camY/2f; // TODO: test this with different resolutions and maps
    Camera.main.transform.position = new Vector3 (camX,
                                                  camY,
                                                  0);
    // orthographic size is the vertical size
    float orthoV = tileset.GetTileSize () * ((2 + tilemap.GetWidth ()) / 2f); // required size to display every row
    float orthoH = tileset.GetTileSize () * ((2 + tilemap.GetHeight ()) / 2f); // required size to display every column
    float fixedOrthoV = (orthoV * Camera.main.aspect < orthoV) ? orthoH * Camera.main.aspect : orthoV;
    Camera.main.orthographicSize = fixedOrthoV;
    // creating level
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
        uiCanvas.FindChild("PlayerPanel"+(i+1)).gameObject.SetActive(false);
        continue;
      } else {
        int j = i+1;
        GameObject p = Instantiate (playerPrefab) as GameObject;
        p.transform.name = "Player" + j;
        p.transform.parent = level.transform;
        PlayerController pc = p.GetComponent<PlayerController> ();
        pc.SetID(j);
        pc.color = colors [i];
        p.transform.localPosition = spawnPoint [i];
        p.transform.localScale = Vector3.one * playerScale;
        switch(i) {
        case 0: p.transform.Rotate(0, 0, 45f);
          break;
        case 1: p.transform.Rotate(0f, 0f, -135f);
          break;
        case 2: p.transform.Rotate(0f, 0f, -45f);
          break;
        case 3: p.transform.Rotate(0f, 0f, 135f);
          break;
        }
        p.GetComponent<Respawn>().respawner = respawner;
        respawner.AddPlayer (p.transform, i);
        p.GetComponent<DropBomb>().level = level.transform;
        GameObject uiPanel = uiCanvas.Find("PlayerPanel"+j).gameObject;
        MatchPlayerUI playerui = uiPanel.GetComponent<MatchPlayerUI>();
        playerui.player = pc;
        playerui.mode = defaultType;
        uiPanel.GetComponent<Image>().color = pc.color;
        control.AddPlayer(pc);
      }
    }
  }

 
}
