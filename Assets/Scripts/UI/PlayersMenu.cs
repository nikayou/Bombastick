using UnityEngine;
using System.Collections;
using XboxCtrlrInput;

public class PlayersMenu : MonoBehaviour
{

  enum playerState
  {
    ABSENT,
    JOINED,
    CONFIRMED
 }
  ;
 
  public Color[] possibleColors;

  private class PlayerCharacter
  {
    public int colorIndex;
    public playerState status;
    public GameObject selfObject;
    public GameObject avatar;
    public GameObject confirmObject;
    public RandomRotation rotationScript;

    public PlayerCharacter ()
    {
      status = playerState.ABSENT;
    }

    public void SetObject (GameObject o)
    {
      selfObject = o;
      avatar = o.transform.Find ("Avatar").gameObject;
      confirmObject = o.transform.FindChild ("Confirm").gameObject;
      rotationScript = avatar.GetComponent<RandomRotation> ();
    }

    public void Colorise (Color c)
    {
      if (status == playerState.ABSENT)
        c = GUIUtils.Darker (c);
      avatar.renderer.material.color = c;
    }
    // return TRUE if pressed OK while confirmed, to go to next screen
    public bool PressOK ()
    {
      switch (status) {
      case playerState.ABSENT:
        rotationScript.enabled = true;
        status = playerState.JOINED;
        break;
      case playerState.JOINED:
        rotationScript.enabled = false;
        status = playerState.CONFIRMED;
        confirmObject.SetActive (true);
        break;
      case playerState.CONFIRMED:
        return true;
      }
      return false;
    }
    // return FALSE if pressed BACK while absent, to go to previous screen
    public bool PressBack ()
    {
      switch (status) {
      case playerState.ABSENT:
        return true;
      case playerState.JOINED:
        status = playerState.ABSENT;
        rotationScript.enabled = false;
        break;
      case playerState.CONFIRMED:
        status = playerState.JOINED;
        rotationScript.enabled = true;
        confirmObject.SetActive (false);
        break;
      }
      return false;
    }

  };

  public MenuManager menuManager;
  public Menu nextMenu;
  public Menu backMenu;
  private PlayerCharacter[] players;
  private bool[] up; // players that pressed UP the previous frame
  private bool[] down; // players that pressed DOWN the previous frame
  public MatchSettings matchSettings;
  public AudioSource audioSource;
  public AudioClip chooseClip;
  public AudioClip nextClip;
  public AudioClip cancelClip;
  public MenuLinesControl linesControl;

  void Awake ()
  {
    players = new PlayerCharacter[4];
    up = new bool[4];
    down = new bool[4];
    GameObject playersObject = transform.FindChild ("Canvas/Players").gameObject;
    for (int i = 0; i < 4; i++) {
      up [i] = false;
      down [i] = false;
      players [i] = new PlayerCharacter ();
      players [i].colorIndex = i;
      players [i].SetObject (playersObject.transform.FindChild ("Player" + (i + 1)).gameObject);
      players [i].Colorise (GUIUtils.Darker (possibleColors [i]));
    }

  }

  void OnEnable ()
  {
    ShowCharacters (true); 
  }
 
  void OnDisable ()
  {
    ShowCharacters (false);
  }
 
  void ShowCharacters (bool show)
  {
    foreach (PlayerCharacter p in players) {
      p.selfObject.SetActive (show);
    }
  }

  void Update ()
  {
    for (int i = 0; i < 4; i++) {
      int j = i + 1;
      if (Input.GetButtonDown ("Fire" + j) || XCI.GetButtonDown(XboxButton.A, j)) {
        if (players [i].PressOK ())
          TryNext ();
        players [i].Colorise (possibleColors [players [i].colorIndex]);
      } 
      if (Input.GetButtonDown ("Cancel" + j) || XCI.GetButtonDown(XboxButton.B, j)|| XCI.GetButtonDown(XboxButton.X, j)) {
        if (players [i].PressBack ())
          TryBack ();
        players [i].Colorise (possibleColors [players [i].colorIndex]);
      } 
      if (players [i].status == playerState.JOINED) {
        float y = UnsignedCmp.Max(Input.GetAxis ("Vertical" + j), XCI.GetAxis(XboxAxis.LeftStickY, j));
        if (Mathf.Abs (y) < 0.5f) {
          up [i] = down [i] = false;
        } else if (y <= -0.5f) {
          audioSource.PlayOneShot (chooseClip);
          if (!up [i]) {
            // player was not pressing UP before
            players [i].colorIndex = NextColorIndexFrom (players [i].colorIndex, true);
            players [i].Colorise (possibleColors [players [i].colorIndex]);
          }
          up [i] = true;
          down [i] = false;
        } else if (y >= 0.5f) {
          audioSource.PlayOneShot (chooseClip);
          if (!down [i]) {
            // player was not pressing DOWN before
            players [i].colorIndex = NextColorIndexFrom (players [i].colorIndex, false);
            players [i].Colorise (possibleColors [players [i].colorIndex]);
          }
          up [i] = false;
          down [i] = true;
        }
   
      }

    }
  }

  void TryNext ()
  {
    int nbPlayers = 0;
    foreach (PlayerCharacter p in players) {
      playerState ps = p.status;
      if (ps == playerState.JOINED) // player has joined but not choosen yet
        return;
      if (ps == playerState.CONFIRMED) {
        nbPlayers++;
      }
    }
    if (nbPlayers >= 2) {
      NextMenu ();
      audioSource.PlayOneShot (nextClip);
      //ShowCharacters (false);
    }
  }
 
  void TryBack ()
  {
    foreach (PlayerCharacter p in players) {
      playerState ps = p.status;
      if (ps == playerState.JOINED || ps == playerState.CONFIRMED)
        return;
    }
    // here, no player has joined
    audioSource.PlayOneShot (cancelClip);
    BackMenu ();
    //ShowCharacters (false);
  }

  // pre-condition : need at least 4 possible colors
  int NextColorIndexFrom (int i, bool up)
  {
    // check the next color that is not already taken
    int origin = i;
    int max = possibleColors.Length - 1;
    int [] takenColors = new int[4];
    for (int j = 0; j < 4; j++) {
      if (players [j].colorIndex == origin) {
        // we are comparing to "self" player
        takenColors [j] = -1;
        continue;
      }
      takenColors [j] = players [j].colorIndex;
    }
    do {
      if (up) {
        i += 1;
        if (i > max)
          i = 0;
      } else {
        i -= 1;
        if (i < 0)
          i = max;
      }
    } while (i == takenColors[0] || i == takenColors[1] || i == takenColors[2] || i == takenColors[3]);
    return i;
  }

  void NextMenu ()
  {
    menuManager.ShowMenu (nextMenu);
    SyncDataWithController ();
    nextMenu.gameObject.GetComponent<MatchMenu> ().enabled = true;
    linesControl.RandomPosition ();
    this.enabled = false;
  }

  void BackMenu ()
  {
    menuManager.ShowMenu (backMenu);
    linesControl.RandomPosition ();
    this.enabled = false;
  }

  public void SyncDataWithController ()
  {
    for (int i = 0; i < players.Length; i++) {
      if (players [i].status == playerState.CONFIRMED) {
        matchSettings.SetPlayerColor (i, possibleColors [players [i].colorIndex]);
      }
    }
  }

}
