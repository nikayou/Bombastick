using UnityEngine;
using System.Collections;

public class LastManMatch : MatchController {

  public float time = 180f; 
  private float timer;

  void Awake () {
    timer = time;
  }

  void Update () {
    AddScoreToOwner ();
    timer -= Time.deltaTime;
    if (timer <= 0f) {
      End();
    }
  }

  void AddScoreToOwner () {
    foreach (PlayerController pc in players) {
      if (pc.IsOwner () ) {
	pc.SetScore(1);
      } else {
	pc.SetScore(0);
      }
    }
  }

  public void Reset (float t) {
    time = t;
    timer = t;
    ResetScore(0f);
  }

}

