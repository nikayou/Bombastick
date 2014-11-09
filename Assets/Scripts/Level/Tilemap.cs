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
				//Debug.Log ("map("+x+","+y+","+layerIndex+") = "+id+" -> "+res);
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
