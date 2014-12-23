using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(EndMatch))]

public class MatchController : MonoBehaviour
{
  
  protected List<PlayerController> players;

  protected virtual void Start ()
  {
    players = new List<PlayerController> ();
    foreach (GameObject p in GameObject.FindGameObjectsWithTag("Player")) {
      players.Add (p.GetComponent<PlayerController> ());
    }    
  }

  /*
   * Add to the player with the given index, the given amount of score
   */
  void AddScore (int index, float amount)
  {
    players [index].AddScore (amount);
  }

  public void ResetScore (float value = 0f)
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
      Debug.Log ("winner: " + winner + "/" + players.Count);
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
