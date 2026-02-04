using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
  // makes referencing corners easier
  public enum Corners {
    LT,
    LB,
    RT,
    RB
  }

  // corners we may want to remove
  [SerializeField]
  GameObject CornerLT;
  [SerializeField]
  GameObject CornerLB;
  [SerializeField]
  GameObject CornerRT;
  [SerializeField]
  GameObject CornerRB;

  Dictionary<Corners, GameObject> corners = new Dictionary<Corners, GameObject>();

  public void updateCorners(){

  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    corners[Corners.LT] = CornerLT; 
    corners[Corners.LB] = CornerLB; 
    corners[Corners.RT] = CornerRT; 
    corners[Corners.RB] = CornerRB; 
  }
}
