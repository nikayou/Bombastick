using UnityEngine;
using System.Collections;

public class Tile
{

  public bool blocking;
  public bool destructable;

  public Tile (bool _blocking = false, bool _destructable = false)
  {
    blocking = _blocking;
    destructable = _destructable;
  }

  public static string ToXML (Tile t)
  {
    string output = "<tile ";
    output += ("destructable=\"" + t.destructable + "\" ");
    output += ("blocking=\"" + t.blocking + "\" ");
    output += "> \n";
    return output;
  }

  override public string ToString ()
  {
    string output = ("Tile : ");
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
