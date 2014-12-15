public class Power2 {

  public static int Ceiling (int x)
  {
    x--; // comment out to always take the next biggest power of two, even if x is already a power of two
    x |= (x >> 1);
    x |= (x >> 2);
    x |= (x >> 4);
    x |= (x >> 8);
    x |= (x >> 16);
    return (x+1);
  }

}