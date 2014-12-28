using UnityEngine;
using System.Collections;

public class TargetMatch : MatchController
{

  public float targetTime = 15f;

  void Start ()
  {
    foreach (PlayerController pc in players) {
      pc.AddScore (targetTime);
    }    
    if (timeLabel)
      timeLabel.text = ""+targetTime;
  }

  override public void ResetScore (float value)
  {
    foreach (PlayerController pc in players) {
      pc.SetScore (targetTime);
    }
  }

  protected override void Update ()
  {
    int l = players.Count;
    for (int i = 0; i < l; i++) {
      if (players [i].IsOwner ()) {
        if (players [i].AddScore (-Time.deltaTime) <= 0f) {
          End ();
        }
      }
    }
  }

}
