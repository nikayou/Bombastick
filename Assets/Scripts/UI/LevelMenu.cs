using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;

public class LevelMenu : MonoBehaviour {
	
	private Queue<string> maps;
	private int index;
	public Text mapName;
	public MatchSettings matchSettings;
	public MenuManager menuManager;
	public Menu backMenu;

	void Awake () {
		maps = LoadMapsFromDirectory ("Levels/");
		if (maps.Count <= 0) {
			// TODO: no level can be found: loading default map
			Debug.Log ("No level to load");
		}
		index = 0;
	}

	void OnEnable () {
		DisplayMap ();
	}

	void Update () {
		if (Input.GetButton("Cancel1") || Input.GetButton("Cancel2") || Input.GetButton("Cancel3") || Input.GetButton("Cancel4")) {
			BackMenu ();
        }
	}

	public void BackMenu () {
		backMenu.gameObject.GetComponent<MatchMenu>().enabled = true;
		menuManager.ShowMenu(backMenu);
		this.enabled = false;
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

	public void PreviousMap () {
		index--;
		if (index < 0)
			if (maps.Count > 0)
				index = maps.Count-1;
			else
				index = 0;
		DisplayMap ();
	}

	public void NextMap () {
		index++;
		if (index >= maps.Count)
			index = 0;
		DisplayMap ();
	}

	public void DisplayMap () {
		//TODO: also display a schema minimap:
		// blocks are black, destructable are grey, through are white
		mapName.text = maps.ElementAt(index);
	}
  

  public void LaunchMatch () {
		Debug.Log ("launching match...");
        Application.LoadLevel(1);
		Debug.Log ("launched match");
  }

  public void SyncDataWithController () {
		matchSettings.SetLevelName(maps.ElementAt(index));
  }

}

