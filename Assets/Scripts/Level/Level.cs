using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour
{

  public Tileset tileset;
  public Tilemap map;
  public GameObject tilePrefab;
  static public Vector2 halfVec = Vector2.one / 2;
  private int size;
  public int zoom = 4;

  public void Create ()
  {
    size = tileset.GetTileSize ();
    int stop = map.GetNbLayers ();
    for (int i = 0; i < stop; i++) {
      CreateFloor (i);
    }
  }

  public void Destroy ()
  {
    Destroy (this.gameObject);
  }

  void CreateFloor (int layer)
  {
    int i, j; // iterators for the map
    int w = map.GetWidth ();
    int h = map.GetHeight ();
    // TODO: don't compute this everytime
    Transform tilesObject = transform.Find ("Tiles");
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
    sr.sprite = (Sprite.Create (tileset.GetTexture (), subRect, halfVec, size));
    // TODO: colliders and destruction
    return go;
  }

}
