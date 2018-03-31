using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MindlessMovement : MonoBehaviour
{
  public float Speed = 10f;
  private Transform target;
  private int targetIndex = 0;

  void Start()
  {
    MoveNext();
  }

  void Update()
  {
    if(target == null)
      return;

    var dir = target.position - transform.position;
    var step = Speed * Time.deltaTime;


    var rotate = Vector3.RotateTowards(transform.forward, dir, step, 0.0f);
    transform.rotation = Quaternion.LookRotation(rotate);

    transform.Translate(dir.normalized * step, Space.World);

    if(Vector3.Distance(transform.position, target.position) <= 0.2f)
      MoveNext();
  }

  void MoveNext()
  {
    if (++targetIndex == Waypoints.Points.Length)
    {
      Finish();
    }
    else
    {
      target = Waypoints.Points[targetIndex];
    }
  }

  private void Finish()
  {
    Destroy(this);
  }
}
