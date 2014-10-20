using UnityEngine;
using System.Collections;

public class Border : MonoBehaviour {

  public int dimension = 16;
  public GameObject borderPrefab;
  private Transform borderContainer;
  private Tile [,] map;
  private int limit;

  void Awake () {
    map = new Tile [dimension, dimension];
    limit = dimension - 1;
    borderContainer = transform.Find("Border");
  }

  void Start () {
    CreateBounds();
  }

  void CreateBounds () {
    for (int i = 0; i < dimension; i++) {
      map[0, i] = Tile.BLOCK;
      map[limit, i] = Tile.BLOCK;
      map[i, 0] = Tile.BLOCK;
      map[i, limit] = Tile.BLOCK;
      PlaceBorder (0, i);
      PlaceBorder (limit, i);
      PlaceBorder (i, 0);
      PlaceBorder (i, limit);
    }
  }

  void PlaceBorder (int x, int y) {
    GameObject tile = Instantiate(borderPrefab) as GameObject;
    tile.isStatic = true;
    tile.transform.position = new Vector3(x, y, 0);
    tile.transform.parent = borderContainer;
  }

}
