using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

  private Tileset tileset;
  private Tilemap map;
  public GameObject tilePrefab;
  static public Vector2 halfVec = Vector2.one / 2;
  private int size;

  void Awake () {
    tileset = new Tileset(WWW.EscapeURL("Levels/Tilesets/a.xml"));
    size = tileset.GetTileSize();
    //map = new Tilemap(WWW.EscapeURL("Levels/level1.xml"));
    map = new Tilemap(16, 16, 1);
    for (int i = 0; i < 16; i++) {
      for (int j = 0; j < 16; j++) {
	int r = Random.Range(0, 5);
	map.Put(r,i,j);
      }   
    }
    Create ();
  }

  void Create () {
    int stop = map.GetNbLayers();
    for (int i = 0; i < stop; i++) {
      CreateFloor (i);
    }
  }

  void CreateFloor (int layer) {
    int i, j; // iterators for the map
    int w = map.GetWidth();
    int h = map.GetHeight();
    // TODO: don't compute this everytime
    Transform tilesObject = transform.Find("Tiles");
    GameObject layerObject = new GameObject();
    layerObject.transform.parent = tilesObject;
    layerObject.transform.position = halfVec;
    layerObject.name = "Layer"+layer;
    for (i = 0; i < w; i++) {
      for (j = 0; j < h; j++) {
	int tileIndex = map.Get(i,j,layer);
	GameObject go = CreateTile(tileIndex);
	go.transform.parent = layerObject.transform;
	go.transform.localPosition = new Vector3(i, j, 0);
	go.name = "Tile-"+i+"_"+j+"("+tileIndex+")";	
      }
    }
  }

  GameObject CreateTile (int tileIndex) {
    // retrieving the tile corresponding to the index
    Tile tile = tileset.Get(tileIndex);
    // computing subRectangle
    int w = tileset.GetWidth();
    int h = tileset.GetHeight();
    // TODO: don't hard-code this (the 4)
    int x = (tileIndex % w) * size;
    // TODO: the second parameter could be the top, or the bottom
    int y = (h*size)-((tileIndex-1) / h) * size;
    Rect subRect = new Rect(x, y, size, size);
    // creating the object
    GameObject go = Instantiate (tilePrefab) as GameObject;
    // assigning sub-sprite
    //TODO : several tiles share the same sprite, no need to create it everytime
    SpriteRenderer sr = (SpriteRenderer)go.renderer;
    sr.sprite = (Sprite.Create(tileset.GetTexture(), subRect, halfVec, size));
    // TODO: colliders and destruction
    return go;
  }

}
