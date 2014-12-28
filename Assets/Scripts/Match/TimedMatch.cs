using UnityEngine;
using System.Collections;

public class TimedMatch : MatchController
{

  protected override void Update () {
    base.Update();
    AddScoreToOwner (Time.deltaTime);
  }

  void AddScoreToOwner (float amount)
  {
    foreach (PlayerController pc in players) {
      if (pc.IsOwner ()) {
        pc.AddScore (amount);
        return;
      } 
    }
  }


}
