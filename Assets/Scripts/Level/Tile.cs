using UnityEngine;
using System.Collections;

public class Tile {

  private int index;
  private bool blocking;
  private bool destructable;

  public Tile (int _index, bool _blocking = false, bool _destructable = false) {
    index = _index;
    blocking = _blocking;
    destructable = _destructable;
    Debug.Log("created tile : "+this.ToString());
  }

  public int GetIndex () {
    return index;
  }

  public static Tile FromXML (string s) {
    // TODO
    return null;
  }

  public static string ToXML (Tile t) {
    string output = "<tile ";
    output += ("destructable=\"" + t.destructable +"\" ");
    output += ("blocking=\"" + t.blocking + "\" ");
    output += "> \n";
    return output;
  }

  public string ToString () {
    string output = ("Tile "+index+" : ");
    if (!destructable) {
      output += "non-";
    }
    output += "destructable / ";
    if (!blocking) {
      output += "non-";
    }
    output += "blocking.";
    return output;
  }

}
