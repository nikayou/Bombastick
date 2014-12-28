using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(EndMatch))]

public class MatchController : MonoBehaviour
{
  
  protected List<PlayerController> players;
  public Text timeLabel;
  public float time;
  private float timer;
  public bool started = false;

  protected virtual void Awake ()
  {
    players = new List<PlayerController> ();
    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
      players.Add (p.GetComponent<PlayerController> ());
    }
    if (timeLabel)
      timeLabel.text = "" + Mathf.RoundToInt (timer);
  }

  protected virtual void Update ()
  {
    if (started) {
      timer -= Time.deltaTime;
      if (timeLabel)
        timeLabel.text = "" + Mathf.RoundToInt (timer);
      if (timer <= 0f) {
        End ();
      }
    }
  }

  public void Reset (float t)
  {
    time = t;
    timer = t;
    ResetScore (0f);
  }

  public void AddPlayer (PlayerController pc)
  {
    players.Add (pc);
  }

  /*
   * Add to the player with the given index, the given amount of score
   */
  void AddScore (int index, float amount)
  {
    players [index].AddScore (amount);
  }

  public virtual void ResetScore (float value = 0f)
  {
    foreach (PlayerController pc in players) {
      pc.SetScore (value);
    }
  }

  protected virtual void End ()
  {
    EndMatch endMatch = GetComponent<EndMatch> ();
    endMatch.SetWinner (GetWinner ());
    endMatch.enabled = true;
    Destroy (this);
  }

  int GetWinner ()
  {
    int winner = -1;
    bool draw = true;
    float winnerScore = -1;
    foreach (PlayerController pc in players) {
      if (pc.GetScore () > winnerScore) {
        draw = false;
        winner = pc.GetID ();
        winnerScore = pc.GetScore ();
      } else if (pc.GetScore () == winnerScore) {
        draw = true;
      }
    }
    if (draw) {
      return -1;
    } else {
      return winner;
    }
    
  }

}
