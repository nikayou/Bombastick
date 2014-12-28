using UnityEngine;
using System.Collections;

public class LastManMatch : MatchController
{

  void Update ()
  {
    base.Update();
    AddScoreToOwner ();
  }

  void AddScoreToOwner ()
  {
    foreach (PlayerController pc in players) {
      if (pc.IsOwner ()) {
        pc.SetScore (1);
      } else {
        pc.SetScore (0);
      }
    }
  }

}

