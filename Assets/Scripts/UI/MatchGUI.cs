using UnityEngine;
using System.Collections;

public class MatchGUI : MonoBehaviour
{

  public GUISkin skin;
  private Vector2 playerHUDDimensions;

  void Start ()
  {
    playerHUDDimensions = new Vector2 (0.15f, 0.1f);
  }

  void OnGUI ()
  {
    TimerHUD ();
    PlayerHUD (1);
    PlayerHUD (2);
    //    PlayerHUD (3);
    //    PlayerHUD (4);
  }

  void TimerHUD ()
  {
    
  }

  void PlayerHUD (int id = 1)
  {
    GameObject player = GameObject.Find ("Player" + id);
    float time = player.GetComponent<PlayerController> ().GetScore ();
    time = (Mathf.Round (time * 10f) / 10f);
    // TODO: do not recalculate everything for each player
    string str = "" + time;
    if ((time * 10f) % 10f == 0f) {
      str += ".0";
    }
    float w = playerHUDDimensions.x;
    float h = playerHUDDimensions.y;
    float x = (id % 2 == 1) ? w / 2f : 1f - (w / 2f);
    float y = (id == 1 || id == 4) ? h / 2f : 1f - (h / 2f);
    GUI.backgroundColor = player.transform.renderer.material.color;
    GUI.Box (GUIUtils.CenteredNormal (x, y, w, h), str, skin.box);
  }

}
