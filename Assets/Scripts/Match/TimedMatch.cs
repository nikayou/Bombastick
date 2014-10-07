using UnityEngine;
using System.Collections;

public class TimedMatch : MatchController {

       public float time = 180f; 
       private float timer;

       void Awake () {
         timer = time;
       }

       void Update () {
         timer -= Time.deltaTime;
	 if (timer <= 0f) {
	    GetComponent<EndMatch>().enabled = true;
	    Destroy(this);
	 }
       }

}
