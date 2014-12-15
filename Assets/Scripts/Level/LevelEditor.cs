using UnityEngine;
using System.Collections;

public class LevelEditor : MonoBehaviour
{

		private int currentTile = 0;
		public GUISkin skin;

		void OnGUI ()
		{
				GUI.Box (GUIUtils.CenteredNormal (.085f, .1f, .15f, .2f), "Current", skin.box);
    
		}

}
