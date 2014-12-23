using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MatchMenu : MonoBehaviour
{

  private MatchType matchType = MatchType.TIMED;
  public MenuManager menuManager;
  public Menu backMenu;
  public Menu nextMenu;
  public float duration;
  public Text durationSelect;
  public Text modeSelect;
  public MatchSettings matchSettings;
  public AudioSource audioSource;
  public AudioClip cancelClip;
  public MenuLinesControl linesControl;

  void Start ()
  {
    UpdateMode ();
  }

  public void NextMode ()
  {
    int newMode = (int)(matchType) + 1;
    if (newMode >= (int)(MatchType.NB))
      newMode = 0;
    matchType = (MatchType)(newMode);
    UpdateMode ();
  }

  public void Activate (GameObject o)
  {
    o.SetActive (true);
  }

  public void NextMenu ()
  {
    menuManager.ShowMenu (nextMenu);
    nextMenu.gameObject.GetComponent<LevelMenu> ().enabled = true;
    this.enabled = false;
  }

  public void BackMenu ()
  {
    audioSource.PlayOneShot (cancelClip);
    menuManager.ShowMenu (backMenu);
    backMenu.gameObject.GetComponent<PlayersMenu> ().enabled = true;
    linesControl.RandomPosition ();
    this.enabled = false;
  }

  void Update ()
  {
    if (Input.GetButton ("Cancel1") || Input.GetButton ("Cancel2") || Input.GetButton ("Cancel3") || Input.GetButton ("Cancel4")) {
      BackMenu ();
    }
  }

  public void UpdateDuration (float d)
  {
    duration = d;
    durationSelect.text = "" + d;
  }

  public void UpdateMode ()
  {
    switch (matchType) {
    case MatchType.TIMED:
      modeSelect.text = "Best time match";
      break;
    case MatchType.TARGET:
      modeSelect.text = "Target time match";
      break;
    case MatchType.LAST_MAN:
      modeSelect.text = "Last man keeping";
      break;
    case MatchType.DEATH:
      modeSelect.text = "Deathmatch";
      break;
    default:
      modeSelect.text = "Error";
      break;
    }
  }

  public void SyncDataToController ()
  {
    matchSettings.SetMatchDuration (duration);
    matchSettings.SetMatchMode (matchType);
  }

}