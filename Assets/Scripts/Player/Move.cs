using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(PlayerController))]

public class Move : MonoBehaviour
{

  public float speed = 3.0f;
  public float movementSmooth = 1.5f;
  public float rotationSmooth = 5f;
  private Vector2 destination;
  private PlayerController myController;

  void Awake ()
  {
    myController = GetComponent<PlayerController> ();
  }

  // Update is called once per frame
  void Update ()
  {
    float x = Input.GetAxis ("Horizontal" + myController.GetID ());
    float y = Input.GetAxis ("Vertical" + myController.GetID ());
    destination = (new Vector2 (x, y).normalized) * speed;
  }

  void FixedUpdate ()
  {
    if (destination != Vector2.zero) {
      Rotate (destination);
      Vector2 newPos = Vector2.Lerp (rigidbody2D.position, rigidbody2D.position + destination, movementSmooth * Time.fixedDeltaTime);
      rigidbody2D.MovePosition (newPos);
    }
  }

  void Rotate (Vector2 direction)
  {
    float angle = Vector2.Angle (Vector2.right, direction);
    if (direction.y < 0f) {
      angle *= -1f;
    }
    rigidbody2D.MoveRotation (angle);
  }

}
