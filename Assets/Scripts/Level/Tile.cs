using UnityEngine;
using System.Collections;

public class Tile
{

  private bool blocking;
  private bool destructable;

  public Tile (bool _blocking = false, bool _destructable = false)
  {
    blocking = _blocking;
    destructable = _destructable;
  }

  public void SetSprite (Sprite s)
  {
    //SpriteRenderer sr = (SpriteRenderer)this.renderer;
    //sr.sprite = s;
  }

  public static Tile FromXML (string s)
  {
    // TODO
    return null;
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
