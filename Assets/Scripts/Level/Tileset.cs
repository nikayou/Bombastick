using UnityEngine;
using System.Collections;
using System.IO;

public class Tileset {

  private Texture2D texture; // image read from disk
  private Tile [] tiles;
  private int tileSize;

  public Tileset (string path) {
    LoadFromXML (path);
    LoadTexture (path);
    tileSize = 64;
  }

  public Tile Get (int index) {
    return tiles[index];
  }

  public Texture2D GetTexture () {
    return texture;
  }

  public int GetTileSize () {
    return tileSize;
  }

  public static Tileset LoadFromFile (string path) {
    return (new Tileset(path));
  }

  public void LoadFromXML (string path) {
    // TODO
  }

  public Rect GetDimensionRect () {
    return new Rect (0, 
		     0, 
		     Power2.Ceiling(texture.width), 
		     Power2.Ceiling(texture.height));
  }

  void LoadTexture (string path) {
    // "download" the file from disk
    WWW www = new WWW("file://" + path);                  
    // Wait until its loaded : blocks
    // yield return www;
    Debug.Log("file: "+path+" -> "+www.url);
    // Set the texture
    www.LoadImageIntoTexture(texture);
  }

}
