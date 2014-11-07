using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

  private Tileset tileset;
  private Tilemap map;
  public GameObject tilePrefab;

  void Awake () {
    tileset = new Tileset(WWW.EscapeURL("Levels/Tilesets/a.xml"));
    //map = new Tilemap(WWW.EscapeURL("Levels/level1.xml"));
    map = new Tilemap(16, 16, 1);
    for (int i = 0; i < 16; i++) {
      for (int j = 0; j < 16; j++) {
	int r = Random.Range(0, 4);
	map.Put(r,i,j);
      }   
    }
    Draw();
    //    this.renderer.material.mainTexture = tileset.GetTexture();
  }

  void Update () {
    //    Draw ();
  }

  void Draw () {
    int stop = map.GetNbLayers();
    for (int i = 0; i < stop; i++) {
      DrawFloor (i);
    }
  }

  void DrawFloor (int layer) {
    int i, j;
    int w = map.GetWidth();
    int h = map.GetHeight();
    GameObject layerObject = new GameObject();
    layerObject.transform.parent = transform;
    for (i = 0; i < w; i++) {
      for (j = 0; j < h; j++) {
	int tileIndex = map.Get(i, j, layer);
	Tile tile = tileset.Get(tileIndex);
	GameObject go = Instantiate (tilePrefab) as GameObject;
	go.transform.localPosition = new Vector3(i, j, 0);
	go.transform.parent = layerObject.transform;
	int size = tileset.GetTileSize();
	// TODO: the second parameter could be the top, not the bottom
	Rect subRect = new Rect(size*i, size*j, size, size);
	SpriteRenderer sr = (SpriteRenderer)go.renderer;
	sr.sprite = Sprite.Create(tileset.GetTexture(), subRect, new Vector2(0,0), tileset.GetTileSize() );
	
      }
    }
  }


}
