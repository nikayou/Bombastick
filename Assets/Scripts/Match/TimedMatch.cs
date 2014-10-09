using UnityEngine;
using System.Collections;

public class TimedMatch : MatchController {

  public float time = 180f; 
  private float timer;

  void Awake () {
    timer = time;
  }

  void Update () {
    timer -= Time.deltaTime;
    AddScoreToOwner(Time.deltaTime);
    if (timer <= 0f) {
      End();
    }
  }

  void AddScoreToOwner (float amount) {
    foreach (PlayerController pc in players) {
      if (pc.IsOwner () ) {
	pc.AddScore(Time.deltaTime);
	return;
      } 
    }
  }


}
