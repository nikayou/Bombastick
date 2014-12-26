using UnityEngine;

public class UnsignedCmp
{

  public static float Max (float f1, float f2) 
  {
    float abs1 = Mathf.Abs (f1);
    float abs2 = Mathf.Abs (f2);
    return (abs1 > abs2)?f1:f2;
  }

  public static float Min (float f1, float f2) 
  {
    float abs1 = Mathf.Abs (f1);
    float abs2 = Mathf.Abs (f2);
    return (abs1 < abs2)?f1:f2;
  }

}

