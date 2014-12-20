using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[RequireComponent (typeof(Animator))]
[RequireComponent (typeof(CanvasGroup))]

public class Menu : MonoBehaviour {

	private Animator animator;
	private CanvasGroup group;
	public Selectable firstSelected;

	public bool IsOpen {
		get { return animator.GetBool("IsOpen"); }
		set { animator.SetBool ("IsOpen", value);
			group.blocksRaycasts = value;
			group.interactable = value;
		}
	}

	public void Awake () {
		animator = GetComponent<Animator>();
		group = GetComponent<CanvasGroup>();
	}

	public void SelectFirst () {
		if (firstSelected != null)
			firstSelected.Select();
	}

}
