using UnityEngine;
using System.Collections;

public class MatchSettings : MonoBehaviour
{

  public Color[] playersColors;
  public MatchType matchType;
  public float duration;
  public string levelName;

  void Awake ()
  {
    Color dummy = new Color (0, 0, 0, 0);
    playersColors = new Color[4];
    for (int i = 0; i < playersColors.Length; i++) {
      // default color: if a c=player has this color, he doesn't play
      playersColors [i] = dummy;
    }
  }

  void Start ()
  {
    DontDestroyOnLoad (gameObject);
  }

  public void SetPlayerColor (int p, Color c)
  {
    playersColors [p] = c;
  }

  public void SetMatchMode (MatchType m)
  {
    matchType = m;
  }

  public void SetMatchDuration (float d)
  {
    duration = d;
  }

  public void SetLevelName (string n)
  {
    levelName = n;
  }

}
