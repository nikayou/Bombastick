using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Level : MonoBehaviour
{

  public Tileset tileset;
  public Tilemap map;
  public GameObject tilePrefab;
  public GameObject borderPrefab;
  static public Vector2 halfVec = Vector2.one / 2;
  private int size;
  private Dictionary <int, Sprite> sprites;

  public void Create ()
  {
    size = tileset.GetTileSize ();
    sprites = new Dictionary<int, Sprite> ();
    int stop = map.GetNbLayers ();
    Transform tilesObject = transform.Find ("Tiles");
    for (int i = 0; i < stop; i++) {
      CreateFloor (i, tilesObject);
    }
    CreateBorder (map.GetWidth (), map.GetHeight ());
  }

  public void Destroy ()
  {
    sprites.Clear ();
    Destroy (this.gameObject);
  }

  void CreateFloor (int layer, Transform tilesObject)
  {
    int i, j; // iterators for the map
    int w = map.GetWidth ();
    int h = map.GetHeight ();
    // TODO: don't compute this everytime
    GameObject layerObject = new GameObject ();
    layerObject.transform.parent = tilesObject;
    layerObject.transform.localScale = Vector3.one;
    //layerObject.transform.position = halfVec * zoom;
    layerObject.name = "Layer" + layer;
    for (j = 0; j < h; j++) {
      for (i = 0; i < w; i++) {
        int tileIndex = map.Get (i, j, layer);
        GameObject go = CreateTile (tileIndex);
        if (go != null) {
          go.transform.parent = layerObject.transform;
          go.transform.localPosition = (new Vector3 (i, h - j, 0));
          go.transform.localScale = Vector3.one;
          go.name = "Tile-" + i + "_" + j + "(" + tileIndex + ")";
          go.renderer.sortingOrder = layer;
        }
      }
    }
  }

  GameObject CreateTile (int tileIndex)
  {
    // retrieving the tile corresponding to the index
    Tile tile = tileset.Get (tileIndex);
    // if in the map, the index is 0, we do not create
    if (tileIndex == 0) {
      return null;
    } else {
      // otherwise, we substract 1 (more convenient for operations)
      tileIndex--;
    }
    // computing subRectangle
    Texture2D texture = tileset.GetTexture ();
    int w = tileset.GetTilesPerLine ();
    int x = (tileIndex % w) * size;
    int y = texture.height - (tileIndex / w) * size - size;
    // creating the object
    Rect subRect = new Rect (x, y, size, size);
    GameObject go = Instantiate (tilePrefab) as GameObject;
    // assigning sub-sprite
    //TODO : several tiles share the same sprite, no need to create it everytime
    SpriteRenderer sr = (SpriteRenderer)go.renderer;
    Sprite sp;
    if (! sprites.TryGetValue (tileIndex, out sp)) {
      // if index has no associated sprite, creating and adding it
      sp = Sprite.Create (tileset.GetTexture (), subRect, halfVec, size);
      sprites.Add (tileIndex, sp);
    }
    sr.sprite = sp;
    // TODO: colliders and destruction
    return go;
  }

  void CreateBorder (int w, int h)
  {
    Transform borderContainer = transform.FindChild ("Border");
    for (int x = -1; x <= w; x++) {
      PlaceBorder (x, 0, borderContainer);
      PlaceBorder (x, h + 1, borderContainer);
      int x1 = x + 1;
      PlaceBorder (-1, x1, borderContainer);
      PlaceBorder (w, x1, borderContainer);
    }
  }

  void PlaceBorder (float x, float y, Transform parent)
  {
    GameObject tile = Instantiate (borderPrefab) as GameObject;
    tile.transform.parent = parent;
    // TODO    
    //    tile.transform.isStatic = true;
    tile.transform.localScale = Vector3.one;
    tile.transform.localPosition = new Vector3 (x, y, 0);
  }

}
