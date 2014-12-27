using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchPlayerUI : MonoBehaviour
{

  public PlayerController player;
  private Text text;

  // Use this for initialization
  void Start ()
  {
    text = transform.FindChild ("Text").GetComponent<Text> ();
  }
 
  // Update is called once per frame
  void Update ()
  {
    float score100 = (Mathf.Round (player.GetScore () * 100f));
    if (score100 != 0f) {
      text.text = "" + (score100 / 100f);
      if (score100 % 100f == 0f)
        text.text += "00";
      if (score100 % 10f == 0f)
        text.text += "0";
    } else {
      text.text = "0.00";
    }
  }

}
