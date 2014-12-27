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
    text = transform.FindChild ("Text").GetComponent<Text>();
  }
 
  // Update is called once per frame
  void Update ()
  {
    text.text = ""+player.GetScore();
  }

}
