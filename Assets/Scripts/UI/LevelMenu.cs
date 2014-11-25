using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class LevelMenu : Menu {

  private MenuManager myManager;
  Queue<string> maps;
  int index;
  private float subTitleY = .35f;

  void Awake () {
    myManager = GetComponent<MenuManager>();
    maps = LoadMapsFromDirectory ("Levels/");
    index = 0;
  }

  void OnGUI () {
    GUI.Label (GUIUtils.CenteredNormal(.5f, .1f, .4f, .1f), "Level select", skin.label);
    // change map
    if (GUI.Button (GUIUtils.CenteredNormal(.3f, subTitleY, .1f, .1f), "<", skin.button) ) {
      index--;
      if (index < 0)
	index = maps.Count -1;
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.7f, subTitleY, .1f, .1f), ">", skin.button) ) {
      index++;
      if (index >= maps.Count)
	index = 0;
    }
    // display map name
    GUI.Label (GUIUtils.CenteredNormal(.5f, subTitleY, .2f, .1f), ""+maps.ElementAt(index), skin.label);
    // next & previous
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .8f, .15f, .08f), "Next", skin.button)) {
      LaunchMatch ();
    }
    if (GUI.Button (GUIUtils.CenteredNormal(.5f, .9f, .15f, .08f), "Back", skin.button)) {
      myManager.ChangeMenuState (MenuState.MATCH);
    }
  }
    

  void LaunchMatch () {
    GameObject.FindGameObjectWithTag("GameController").GetComponent<MatchOptions>().mapPath = maps.ElementAt(index);
    Application.LoadLevel(1);
  }

  Queue<string> LoadMapsFromDirectory(string path) 
    {
      Queue<string> res = new Queue<string>();
      DirectoryInfo info = new DirectoryInfo(path);
      FileInfo[] fileInfo = info.GetFiles("*.xml");
      foreach (FileInfo file in fileInfo) 
	{
	  res.Enqueue(file.Name);
	}
      return res;
    }

}
