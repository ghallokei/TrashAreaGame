using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRotation : PlasticEnemy
{
    void Update()
    {
        Vector3 viewDirection = target.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(viewDirection);
        transform.rotation = rotation;
    }
}
