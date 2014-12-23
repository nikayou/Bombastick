using UnityEngine;
using System.Collections;

public class EndMatch : MatchController
{

  float w = Screen.width / 2f;
  float h = Screen.height / 2f;
  int winner = 0;

  void OnGUI ()
  {
    string msg;
    if (winner == 0) {
      msg = "Draw";
    } else {
      msg = "Player " + winner + " wins!";
    }
    GUI.Box (GUIUtils.CenteredNormal (0, 0, w, h), msg);
  }

  public void SetWinner (int index)
  {
    winner = index;
  }

}
