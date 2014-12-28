using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchPlayerUI : MonoBehaviour
{

  public PlayerController player;
  private Text text;
  public MatchType mode;

  // Use this for initialization
  void Start ()
  {
    text = transform.FindChild ("Text").GetComponent<Text> ();
  }
 
  // Update is called once per frame
  void Update ()
  {
    switch (mode) {
    case MatchType.TIMED:
      TargetOrTimedScore ();
      break;
    case MatchType.TARGET:
      TargetOrTimedScore ();
      break;
    case MatchType.DEATH:
      DeathScore ();
      break;
    case MatchType.LAST_MAN:
      LastManScore();
      break;
    default:
      text.text = "?";
      break;
    }

  }

  void TargetOrTimedScore () {
    float score100 = (Mathf.Round (player.GetScore () * 100f));
    if (score100 != 0f) {
      text.text = "" + (score100 / 100f);
      if (score100 % 100f == 0f)
        text.text += ".00";
      else if (score100 % 10f == 0f)
        text.text += "0";
    } else {
      text.text = "0.00";
    }
  }

  void LastManScore () {
    if (player.GetScore() == 0f) {
      text.text = "O";
    } else {
      text.text = "X";
    }
  }

  void DeathScore () {
    text.text = ""+Mathf.RoundToInt(player.GetScore ());
  }



}
