using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour
{

  Vector3[] spawnPoints;
  Transform[] players;

  void Awake ()
  {
    spawnPoints = new Vector3[4];
    players = new Transform[4];
    for (int i = 0; i < 4; i++) {
      players [i] = null;
    }
  }

  public void AddRespawn (Vector3[] points)
  {
    for (int i = 0; i < 4; i++) {
      spawnPoints [i] = points [i];
    }
  }

  public void AddPlayer (Transform player, int index)
  {
    players [index] = player;
  }

  public Vector3 GetSpawnFor (int index)
  {
    int maxSpawn = 0;
    float maxDistance = 0f;
    for (int j = 0; j < 4; j++) {
      // iteration through spawnpoints
      float distance = 0f;
      for (int i = 0; i < 4; i++) {
        // iteration through players
        if (i == index || players[i] == null) {
          continue;
        } else {
          float x = (players [i].localPosition.x - spawnPoints [j].x);
          float y = (players [i].localPosition.y - spawnPoints [j].y);
          distance += Mathf.Sqrt(Mathf.Pow(x, 2f) + Mathf.Pow(y, 2f));
        }
      }
      if (distance > maxDistance) {
        maxDistance = distance;
        maxSpawn = j;
      }
    }
    return spawnPoints[maxSpawn];
  }

}
