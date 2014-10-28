using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class Tileset {

  private Texture2D texture; // image read from disk
  private Tile [] tiles;
  private int tileSize;

  public Tileset (string path) {
    tileSize = 64;
    texture = new Texture2D(1,1);
    LoadFromXML (path);
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
    XmlTextReader xtr = new XmlTextReader (path);    
    while (xtr.Read()) { 
      if (xtr.Name == "TILESET") {
	string tileSizeStr = string.Empty;
	if ((tileSizeStr = xtr.GetAttribute("tilesize")) != null) {
	  tileSize = int.Parse(tileSizeStr);
	}
	string imgPath = string.Empty;
	if ((imgPath = xtr.GetAttribute("image")) != null) {
	  LoadTexture (imgPath);
	}
      } else if (xtr.Name == "TILE") { 
	//TODO: extract tile
      }
    }
    xtr.Close();
  }

  public Rect GetDimensionRect () {
    return new Rect (0, 
		     0, 
		     Power2.Ceiling(texture.width), 
		     Power2.Ceiling(texture.height));
  }

  void LoadTexture (string path) {
    // "download" the file from disk
    Debug.Log("loading texture "+path);
    WWW www = new WWW(("file://" + path));
    // Wait until its loaded : blocks
    // yield return www;
    Debug.Log("file: "+path+" -> "+www.url);
    // Set the texture
    www.LoadImageIntoTexture(texture);
  }

}
