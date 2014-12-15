using UnityEngine;
using System.Collections;

public class GUIUtils {
	
  public static Vector2 conversionRatio = (new Vector2 (Screen.width / 400f, Screen.height / 300f));

  public static Rect NormalizeRect (Rect source) {
    Rect r = new Rect (source);
    r.x /= Screen.width;
    r.width /= Screen.width;
    r.y /= Screen.height;
    r.height /= Screen.height;
    return r;
  }

  public static Rect CreateRect(float x, float y, float w, float h) {
    Rect r = new Rect();
    r.x = x;
    r.y = y;
    r.width = w;
    r.height = h;
    return r;
  }

  public static Rect CreateNormalRect (float x, float y, float w, float h) {
    Rect r = new Rect (x, y, w, h);
    r.x *= Screen.width;
    r.width *= Screen.width;
    r.y *= Screen.height;
    r.height *= Screen.height;
    return r;
  }

  public static Rect Center (Rect source) {
    Rect r = new Rect (source);
    r.x -= r.width / 2f;
    r.y -= r.height / 2f;
    return r;
  }

  public static Rect CenteredNormal (float x, float y, float w, float h) {
    Rect r = new Rect (x, y, w, h);
    r.width *= Screen.width;
    r.height *= Screen.height;
    r.x = r.x * Screen.width - r.width / 2f;
    r.y = r.y * Screen.height - r.height / 2f;
    return r;
  }

  /// <summary>
  /// Converts a string like "255,23,43" into an RGB color. If an alpha channel is given, it is also applied.
  /// </summary>
  /// <returns>Color normalized.</returns>
  /// <param name="s">String containing the fields, separated by a comma</param>
  public static Color StringRGBToColor (string s)
  {
    Color c = new Color (0, 0, 0, 0);
    string[] sep = s.Split (',');
    // checking invalid strings
    if (sep.Length < 3)
      return c;
    c.r = float.Parse (sep [0]) / 255f;
    c.g = float.Parse (sep [1]) / 255f;
    c.b = float.Parse (sep [2]) / 255f;
    if (sep.Length == 4) {
      // gave an alpha channel
      c.a = float.Parse (sep [3]) / 255f;
    } else {
      // when no alpha channel given, setting to opaque
      c.a = 1.0f;
    }
    return c;
  }

  /// <summary>
  /// Resize the fonts so they always look quite the same size (relative to screen resolution).
  /// </summary>
  public static void ScaleFonts (GUISkin skin, float ratio)
  {
    //int newFontSize = Mathf.FloorToInt (Screen.height * ratio); // new size of the font size '16'
    foreach (GUIStyle s in skin) {
      s.fontSize = Mathf.FloorToInt(s.fontSize * ratio);
    }
  }

  /// <summary>
  /// Makes the tex.
  /// </summary>
  /// <returns>The tex.</returns>
  /// <param name="width">Width.</param>
  /// <param name="height">Height.</param>
  /// <param name="col">Col.</param>
  public static Texture2D MakeTex( int width, int height, Color col )
  {
    Color[] pix = new Color[width * height];
    for( int i = 0; i < pix.Length; ++i )
      {
	pix[ i ] = col;
      }
    Texture2D result = new Texture2D( width, height );
    result.SetPixels( pix );
    result.Apply();
    return result;
  }

  public static Color reser (Color c) 
	{
		return MultColor (c, 1.25f, true);
	}

	public static Color Darker (Color c) 
	{
		return MultColor (c, 0.75f, true);
	}

	public static Color MultColor (Color c, float factor, bool keepAlpha = true) {
		Color res = Color.black;
		res.r = Mathf.Min (1, c.r * factor);
		res.g = Mathf.Min (1, c.g * factor);
		res.b = Mathf.Min (1, c.b * factor);
		if (keepAlpha)
			res.a = c.a;
		else
			res.a = Mathf.Min (1, c.a * factor);
		return res;
	}

}
