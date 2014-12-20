using UnityEngine;
using System.Collections;

public class TitleMenu : Menu {

  public float introTime = 3.0f;
  private float timer = 0f;
  private bool introed = false;
  public GameObject anyKeyObject;

  void Update () {
    if (!introed) {
      timer += Time.deltaTime;
      if (timer >= introTime) {
	    introed = true;
		anyKeyObject.SetActive(true);
	  }
    } else {
      if (Input.anyKey) {
		  anyKeyObject.SetActive(false);
		  Destroy (this);
      }
    }
  }


}
