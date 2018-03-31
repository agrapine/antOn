using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

  public Transform Target;
  public Transform PartRotate;
  public float Range = 15f;
  public float TurnSpeed = 0.2f;
  public string EnemyTag = "Enemy";

	void Start ()
	{
	  InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

  void UpdateTarget()
  {
    GameObject[] enemies = GameObject.FindGameObjectsWithTag(EnemyTag);

    Target = null;
    foreach (GameObject enemy in enemies)
    {
      float distance = Vector3.Distance(transform.position, enemy.transform.position);
      if (Range >= distance)
      {
        Target = enemy.transform;
        break;
      }
    }
  }
	
	// Update is called once per frame
	void Update ()
	{
	  if (Target == null)
	    return;

	  Vector3 dir = Target.position - transform.position;
	  Quaternion lookRotation = Quaternion.LookRotation(dir);
	  Vector3 rotation = Quaternion.Lerp(PartRotate.rotation,lookRotation, Time.deltaTime * TurnSpeed).eulerAngles;
	  PartRotate.rotation = Quaternion.Euler(0f,rotation.y, 0f);
	}

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, Range);
  }
}
