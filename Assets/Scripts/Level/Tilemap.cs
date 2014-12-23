using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;

public class Tilemap
{

  private int[] layers;
  private int nbLayers;
  private int width;
  private int height; //TODO: check if required to keep it
  private int nbPerLayer;
  private string tilesetPath;

  public Tilemap (int _width, int _height, int _nbLayers = 2)
  {
    width = _width;
    height = _height;
    nbPerLayer = width * height;
    nbLayers = _nbLayers;
    layers = new int [nbLayers * nbPerLayer];
  }

  public Tilemap (string path)
  {
    LoadFromXML (path);
  }

  public string GetTileset ()
  {
    return tilesetPath;
  }

  void LoadFromXML (string path)
  {
    XmlTextReader xtr = new XmlTextReader (path);
    int layerIndex = 0;
    bool mapFound = false;
    while (xtr.Read()) {
      if (!mapFound && xtr.Name == "MAP") {
        string widthString = string.Empty;
        if ((widthString = xtr.GetAttribute ("width")) != null) {
          width = int.Parse (widthString);
        }
        string heightString = string.Empty;
        if ((heightString = xtr.GetAttribute ("height")) != null) {
          height = int.Parse (heightString);
        }
        string layersString = string.Empty;
        if ((layersString = xtr.GetAttribute ("nbLayers")) != null) {
          nbLayers = int.Parse (layersString);
        }
        nbPerLayer = width * height;
        string tilesetString = string.Empty;
        if ((tilesetString = xtr.GetAttribute ("tileset")) != null) {
          tilesetPath = tilesetString;
        }
        layers = new int [nbLayers * nbPerLayer];
        mapFound = true;
      } 
      if (mapFound && xtr.Name == "LAYER") {
        xtr.Read ();
        string layerStr = xtr.ReadContentAsString ().Replace (System.Environment.NewLine, "").Trim ();
        string [] tilesStr = layerStr.Split ('-');
        int size = tilesStr.Length;
        for (int i = 0; i < size-1; i++) {
          layers [(layerIndex * nbPerLayer) + i] = int.Parse (tilesStr [i]);
        }
        layerIndex++;
      }
    }

  }

  public void Put (int id, int x, int y, int layerIndex = 0)
  {
    // Assert (x < width);
    // Assert (y < height);
    // Assert (layerIndex < nbLayers);
    layers [(layerIndex * nbPerLayer) + CoordinatesToIndex (x, y)] = id;
  }

  public int Get (int x, int y, int layerIndex = 0)
  {
    // Assert (x < width);
    // Assert (y < height);
    // Assert (layerIndex < nbLayers);
    int id = (layerIndex * nbPerLayer) + CoordinatesToIndex (x, y);
    int res = layers [id];
    return layers [(layerIndex * nbPerLayer) + CoordinatesToIndex (x, y)];
  }

  public int GetWidth ()
  {
    return width;
  }

  public int GetHeight ()
  {
    return height;
  }

  public int GetNbLayers ()
  {
    return nbLayers;
  }

  int CoordinatesToIndex (int x, int y)
  {
    // Assert (x < width);
    // Assert (y < height);
    return (y * width + x);
  }

}
