using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
  public static Transform[] Points;

  void Awake()
  {
    Points = Enumerable
      .Range(0, transform.childCount)
      .Select(i => transform.GetChild(i))
      .ToArray();
  }
}
