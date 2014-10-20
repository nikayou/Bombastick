using UnityEngine;
using System.Collections;
/*
public class FadeGUI : MonoBehaviour {

  public float fadeSpeed = 1.0f;
  public bool fadeIn = true;
  private bool started = true;
  private float currentAlpha;
  private GUITexture guiTexture;

  void Update () {
    if (!started) {
      started = true;
      StartCoroutine(Process());
    } 
  }

  public void Fade (bool _fadeIn = true) {
    started = false;
    fadeIn = _fadeIn;
  }

  IEnumerator Process () {
    if(fadeIn)
      {
	currentAlpha = Mathf.Lerp(currentAlpha, 1f, fadeSpeed * Time.deltaTime);
	guiTexture.color = new Color(.5f,.5f,.5f,currentAlpha);
	if (currentAlpha > .98f) return null;
      }
    else
      {
	currentAlpha = Mathf.Lerp(currentAlpha, 0f, fadeSpeed * Time.deltaTime);
	guiTexture.color = new Color(.5f,.5f,.5f,currentAlpha);
	if(currentAlpha < .01f) return null;
      } 
    yield return null;
  }

}
*/
