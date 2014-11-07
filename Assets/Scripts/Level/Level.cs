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
	int r = Random.Range(0, 5);
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
    int i, j; // iterators for the map
    int w = map.GetWidth();
    int h = map.GetHeight();
    int size = tileset.GetTileSize();
    // TODO: don't compute this everytime
    Transform tilesObject = transform.Find("Tiles");
    GameObject layerObject = new GameObject();
    layerObject.transform.parent = tilesObject;
    layerObject.name = "Layer"+layer;
    /* for all case of the layer of the map, we should create a gameobject whose 
     renderer is a portion of the tileset. */
    for (i = 0; i < w; i++) {
      for (j = 0; j < h; j++) {
	int tileIndex = map.Get(i, j, layer);
	Tile tile = tileset.Get(tileIndex);
	// TODO: don't hard-code this (the 4)
	int x = tileIndex % 4;
	// TODO: the second parameter could be the top, not the bottom
	int y = tileIndex / 4;
	Rect subRect = new Rect(x*size, y*size, size, size);
	//TODO : several tiles share the same sprite, no need to create it everytime
	Debug.Log("creating tile at "+i+","+j+"("+tileIndex+"-> "+x+","+y+" - "+size+")");

	GameObject go = Instantiate (tilePrefab) as GameObject;
	go.transform.localPosition = new Vector3(i, j, 0);
	go.name = "Tile-"+i+"_"+j+"("+tileIndex+")";
	go.transform.parent = layerObject.transform;
        SpriteRenderer sr = (SpriteRenderer)go.renderer;
	sr.sprite = (Sprite.Create(tileset.GetTexture(), subRect, new Vector2(0,0), tileset.GetTileSize() ));

	// TODO: colliders and destruction
	
      }
    }
  }


}
