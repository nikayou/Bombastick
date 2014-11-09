using UnityEngine;
using System.Collections;

/// <summary>
/// Track makes a Transform follow an other Transform's position.
/// </summary>
public class Track : MonoBehaviour
{

		/// <summary>
		/// Transform to follow.
		/// </summary>
		public Transform target;
		/// <summary>
		/// Movement smoothness. 
		/// </summary>
		public float smoothness = 1.5f;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
				Vector2 oldPos = transform.position;
				if (target != null) {
						Vector2 targetPos = target.position;
						transform.position = Vector2.Lerp (oldPos, targetPos, smoothness * Time.deltaTime);
				}
		}
}
