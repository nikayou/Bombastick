using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DeathMatch : MatchController {

  public float time = 15f; 
  private float timer;
  List<PlayerController> players;

  void Awake () {
    timer = time;
  }

  void Start () {
    players = new List<PlayerController>();
    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
      players.Add(p.GetComponent<PlayerController>());
    }
  }

  void Update () {
    timer -= Time.deltaTime;
    if (timer <= 0f) {
      End ();
    } 
  }


  void End () {
    int l = players.Count;
    int maxId = 0;
    for (int i = 1; i < l; i++) {
      if (players[i].GetScore() >= players[maxId].GetScore()) {
	maxId = i;
	// TODO: draws
	GetComponent<EndMatch>().enabled = true;
	Destroy(this);
      }
    }
  }

}

