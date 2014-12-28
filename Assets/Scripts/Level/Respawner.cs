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

  public Vector3 GetSpawnFor (int _index)
  {
    int index = _index - 1;
    int maxSpawn = 0;
    float maxDistance = 0f;
    float spawnCount = spawnPoints.Length;
    float playersCount = players.Length;
    for (int j = 0; j < spawnCount; j++) {
      // iteration through spawnpoints
      float distance = 0f;
      for (int i = 0; i < playersCount; i++) {
        // iteration through players
        if (i == index || players[i] == null) {
          continue;
        } else {
          float x = (players [i].localPosition.x - spawnPoints[j].x);
          float y = (players [i].localPosition.y - spawnPoints[j].y);
          // pythagore computes the distance
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
