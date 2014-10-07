using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

public float time = 3.0f;
public Color baseColor = Color.black;
public Color targetColor = Color.red;
private float timer;

// Use this for initialization
void Start () {
timer = 0f;
renderer.material.color = baseColor;
}

// Update is called once per frame
void Update () {
timer += Time.deltaTime;
renderer.material.color = Color.Lerp(baseColor, targetColor, timer / time);
if (timer >= time) {
Explode();
}
}

void Explode () {
transform.Find("Explosion").gameObject.SetActive(true);
}


}
