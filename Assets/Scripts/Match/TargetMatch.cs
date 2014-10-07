using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TargetMatch : MatchController {

  public float targetTime = 15f; 
  List<PlayerController> players;

  void Start () {
    players = new List<PlayerController>();
    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
      players.Add(p.GetComponent<PlayerController>());
    }
  }

  void Update () {
    int l = players.Count;
    for (int i = 0; i < l; i++) {
      if (players[i].GetScore() >= targetTime) {
	GetComponent<EndMatch>().enabled = true;
	Destroy(this);
      }
    }
  }

}
