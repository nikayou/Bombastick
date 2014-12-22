using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour
{
	public Menu current;
	public UnityEngine.EventSystems.EventSystem eventSystem;

	public void Start () 
	{
		ShowMenu(current);
	}

	public void ShowMenu (Menu m) {
		if (current != null) {
			current.IsOpen = false;
			current.enabled = false;
		}
		current = m;
		current.enabled = true;
		current.IsOpen = true;
		current.SelectFirst();
	}
}