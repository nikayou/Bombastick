using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public class Tileset
{

  private Texture2D texture; // image read from disk
  private Dictionary <int, Tile> tiles;
  private int tileSize;
  private int tilesPerLine = 4;
  private int borderIndex = -1; // index of the tile used for bounds

  public Tileset (string path)
  {
    tiles = new Dictionary<int, Tile> ();
    tileSize = 64;
    texture = new Texture2D (1, 1);
    LoadFromXML (path);
    tilesPerLine = texture.width / tileSize;
  }

  public Tile Get (int index)
  {
    Tile res = new Tile ();
    if (! tiles.TryGetValue (index, out res)) {
      res = new Tile (true, false);
      tiles.Add (index, res);
    }
    return res;
  }

  public Texture2D GetTexture ()
  {
    return texture;
  }

  public int GetTileSize ()
  {
    return tileSize;
  }

  public int GetTilesPerLine ()
  {
    return tilesPerLine;
  }

  public int GetBorderIndex () {
    return borderIndex;
  }

  public static Tileset LoadFromFile (string path)
  {
    return (new Tileset (path));
  }

  public void LoadFromXML (string path)
  {
    // TODO: check errors with asserts
    Debug.Log ("loading tileset " + path);
    XmlTextReader xtr = new XmlTextReader (path);    
    while (xtr.Read()) { 
      if (xtr.Name == "TILESET") {
        string tileSizeStr = string.Empty;
        if ((tileSizeStr = xtr.GetAttribute ("tilesize")) != null) {
          tileSize = int.Parse (tileSizeStr);
        }
        string imgPath = string.Empty;
        if ((imgPath = xtr.GetAttribute ("image")) != null) {
          LoadTexture (imgPath);
        }
        string borderStr = string.Empty;
        if ((borderStr = xtr.GetAttribute ("borderIndex")) != null) {
          borderIndex = int.Parse (borderStr);
        }
      } else if (xtr.Name == "TILE") { 
        string _destructable, _blocking;
        bool destructable = false, blocking = false;
        string tileIndex = string.Empty;
        if ((tileIndex = xtr.GetAttribute ("index")) == null) {
          // TODO: extraction error
          continue;
        }
        if ((_destructable = xtr.GetAttribute ("destructable")) != null) {
          destructable = bool.Parse (_destructable);
        }
        if ((_blocking = xtr.GetAttribute ("blocking")) != null) {
          blocking = bool.Parse (_blocking);
        }
        Tile tile = new Tile (blocking, destructable);
        tiles.Add (int.Parse (tileIndex), tile);
      }
    }
    xtr.Close ();
  }

  public Rect GetDimensionRect ()
  {
    return new Rect (0, 
       0, 
       Power2.Ceiling (texture.width), 
       Power2.Ceiling (texture.height));
  }

  void LoadTexture (string path)
  {
    // "download" the file from disk
    WWW www = new WWW (("file://" + path));
    // Wait until its loaded : blockings
    // yield return www;
    // Set the texture
    www.LoadImageIntoTexture (texture);
    texture.wrapMode = TextureWrapMode.Clamp;
    texture.filterMode = FilterMode.Point;
  }

  public Tile GetTile (int index)
  {
    return tiles [index];
  }

}
