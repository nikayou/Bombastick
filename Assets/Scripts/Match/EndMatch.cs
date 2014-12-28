using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EndMatch : MonoBehaviour
{

  public float resultTime = 3.0f;
  private float timer = 0f;
  public Text winnerText;
  public GameObject resultPanel;

  void Start ()
  {
    resultPanel.SetActive(true);
    timer = resultTime;
  }

  void Update ()
  {
    if (timer <= 0f) {
      if (Input.anyKey) {
        Application.LoadLevel (0);
      } 
    } else {
      timer -= Time.deltaTime;
    }
  }

  public void SetWinner (int index)
  {
    if (index != -1) {
      winnerText.text = "Player "+(index)+" wins!";
    }
  }

}
