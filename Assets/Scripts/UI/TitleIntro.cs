using UnityEngine;
using System.Collections;

public class TitleIntro : MonoBehaviour {

  public float introTime = 3.0f;
  private float timer = 0f;
  private bool introed = false;
  public GameObject displayedObject;
	
  void Update () {
    if (!introed) {
      timer += Time.deltaTime;
      if (timer >= introTime) {
		if (timer >= introTime+1) {
			introed = true;
		} else {
		    displayedObject.SetActive(true);
		}
	  }
    } else {
      if (Input.anyKey) {
		  displayedObject.SetActive(false);
		  Destroy (this);
      }
    }
  }


}
