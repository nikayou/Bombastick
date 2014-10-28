using UnityEngine;
using System.Collections;

public class Tilemap {

  private int [] layers;
  private int width;
  private int height; //TODO: check if required to keep it
  private int nbPerLayer;

  public Tilemap (int _width, int _height, int nbLayers = 2) {
    width = _width;
    height = _height;
    nbPerLayer = width * height;
    layers = new int [nbLayers * nbPerLayer];
  }

  public int Get (int x, int y, int layerIndex = 0) {
    return layers[(layerIndex * nbPerLayer) + CoordinatesToIndex(x,y)];
  }

  public int GetWidth () {
    return width;
  }

  public int GetHeight () {
    return height;
  }

  int CoordinatesToIndex (int x, int y) {
    return (y * width + x);
  }


  /*
  void CreateBounds () {
    for (int i = 0; i < dimension; i++) {
      map[0, i] = Tile.BLOCK;
      map[limit, i] = Tile.BLOCK;
      map[i, 0] = Tile.BLOCK;
      map[i, limit] = Tile.BLOCK;
    }
    }
  
  void CreateRandom () {
    int nb = Random.Range(minBlocks, maxBlocks+1);
    for (int i = 0; i < nb; i++) {
      int x = Random.Range(1, limit);
      int y = Random.Range(1, limit);
      if (!IsSpawn(x, y)) {
	map[x,y] = (Tile)(Random.Range(1, (int)Tile.NB));
	//	map[x,y] = Tile.DESTRUCT;
      }
    }
  }

  void PlaceTiles () {
    for (int i = 0; i < dimension; i++) {
      for (int j = 0; j < dimension; j++) {
	if (map[i,j] == Tile.DESTRUCT) {
	  PlaceBlock(i, j);
	} else if (map[i,j] == Tile.BLOCK) {
	  PlaceBorder(i, j);
	}
      }
    }
  }

  void PlaceBlock (int x, int y) {
    GameObject tile = Instantiate(blockPrefab) as GameObject;
    tile.transform.position = new Vector3(x, y, 0);
    tile.transform.parent = blocksContainer;
    tile.transform.tag = "Destructable";
  }

  void PlaceBorder (int x, int y) {
    GameObject tile = Instantiate(borderPrefab) as GameObject;
    // TODO    
    //    tile.transform.isStatic = true;
    tile.transform.position = new Vector3(x, y, 0);
    tile.transform.parent = borderContainer;
  }

  bool IsSpawn (int x, int y) {
    return ( (x <= 3 && y <= 3) 
	     || (x <= 3 && y >= dimension-4)
	     || (x >= dimension-4 && y <= 3)
	     || (x >= dimension-4 && y >= dimension-4) 
	     // last is for the object
	     || (x == dimension/2 && y == dimension/2)
	     );
  }
  */
}
