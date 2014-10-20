using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathMatch : MatchController {

  public float time = 180f; 
  private float timer;

  void Awake () {
    timer = time;
  }

  void Update () {
    timer -= Time.deltaTime;
    if (timer <= 0f) {
      End ();
    } 
  }

  public void Reset (float t) {
   time = t;
   timer = t;
   ResetScore(0f);
  }

}

