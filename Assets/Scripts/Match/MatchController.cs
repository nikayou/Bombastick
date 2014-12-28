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

  protected virtual void Awake ()
  {
    timer = time;
    players = new List<PlayerController> ();
    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
      players.Add (p.GetComponent<PlayerController> ());
    }    
  }

  protected virtual void Update () 
  {
    timer -= Time.deltaTime;
    if (timeLabel)
      timeLabel.text = "" + Mathf.RoundToInt (timer);
    if (timer <= 0f) {
      End ();
    } 
  }

  public void Reset (float t)
  {
    time = t;
    timer = t;
    ResetScore (0f);
  }

  public void AddPlayer (PlayerController pc) {
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
    endMatch.enabled = true;
    endMatch.SetWinner (GetWinner ());
    Destroy (this);
  }

  int GetWinner ()
  {
    int winner = 1;
    bool draw = true;
    bool madeOne = false;
    foreach (PlayerController pc in players) {
      float winnerScore = players [winner - 1].GetScore ();
      if (pc.GetScore () > winnerScore) {
        draw = false;
        winner = pc.GetID ();
      } else if (madeOne && pc.GetScore () == winnerScore) {
        draw = true;
      }
      if (!madeOne) {
        madeOne = true;
      }
    }
    if (draw) {
      return 0;
    } else {
      return winner;
    }
    
  }

}
