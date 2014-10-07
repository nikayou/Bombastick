using UnityEngine;
using System.Collections;

public class LastManMatch : MatchController {

       public float time = 180f; 
       public bool keepLast = true;
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

