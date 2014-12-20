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
  

  void LaunchMatch () {
    GameObject.FindGameObjectWithTag("GameController").GetComponent<MatchOptions>().mapPath = maps.ElementAt(index);
    Application.LoadLevel(1);
  }

}

