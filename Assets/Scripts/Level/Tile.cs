using UnityEngine;
using System.Collections;

public class Tile {

  private Sprite sprite;
  private bool block;
  private bool destruct;

  public Tile (Sprite _sprite, bool _block = false, bool _destruct = false) {
    sprite = _sprite;
    block = _block;
    destruct = _destruct;
  }

  public Sprite GetSprite () {
    return sprite;
  }

  public static Tile FromXML (string s) {
    // TODO
    return null;
  }

  public static string ToXML (Tile t) {
    string output = "<tile ";
    output += ("destruct=\"" + t.destruct +"\" ");
    output += ("block=\"" + t.block + "\" ");
    output += "> \n";
    return output;
  }

}
