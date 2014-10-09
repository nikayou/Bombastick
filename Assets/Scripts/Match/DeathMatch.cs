using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathMatch : MatchController {

  public float time = 15f; 
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

}

