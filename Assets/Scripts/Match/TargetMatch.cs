using UnityEngine;
using System.Collections;

public class TargetMatch : MatchController
{

  public float targetTime = 15f;

  protected override void Start ()
  {
    base.Start ();
    foreach (PlayerController pc in players) {
      pc.AddScore (targetTime);
    }    
    if (timeLabel)
      timeLabel.text = ""+targetTime;
  }

  void Update ()
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

  public void Reset (float t)
  {
    targetTime = t;
    if (timeLabel)
      timeLabel.text = ""+targetTime;
    ResetScore (t);
  }

}
