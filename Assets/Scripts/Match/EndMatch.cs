using UnityEngine;
using System.Collections;

public class EndMatch : MatchController {

  float w = Screen.width / 2f;
  float h = Screen.height / 2f;
  void OnGUI () {
      GUI.Box (GUIUtils.CenteredNormal(0, 0, w, h), "Game Over");
  }

}
