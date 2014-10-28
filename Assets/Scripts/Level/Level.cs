using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

  private Tileset tileset;
  private Tilemap map;
  public GameObject tilePrefab;

  void Awake () {
    tileset = new Tileset(WWW.EscapeURL("Levels/a.xml"));
    SpriteRenderer sr = (SpriteRenderer)this.renderer;
    sr.sprite = Sprite.Create(tileset.GetTexture(), tileset.GetDimensionRect(), 
    new Vector2(0,0), tileset.GetTileSize() );
    //    this.renderer.material.mainTexture = tileset.GetTexture();
    Debug.Log("loaded : "+sr.sprite);
  }

  void Update () {
    //Draw ();
  }

  void Draw () {
    //DrawFloor (0);
    //DrawFloor (1);
  }

  void DrawFloor (int layer) {
    int i, j;
    int w = map.GetWidth();
    int h = map.GetHeight();
    for (i = 0; i < w; i++) {
      for (j = 0; j < h; j++) {
	int tileIndex = map.Get(layer, i, j);
	Tile tile = tileset.Get(tileIndex);
	GameObject go = Instantiate (tilePrefab) as GameObject;
	go.transform.localPosition = new Vector3(i, j, 0);
      }
    }
  }


}
