using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnProj;
    private float timer;

    //used by the gunner to fire a projectile
    public void FireProjectile()
    {
        GameObject proj = Instantiate(projectilePrefab, spawnProj.position, projectilePrefab.transform.rotation);
        Vector3 scale = proj.transform.localScale;
        proj.transform.localScale = new Vector3(
            scale.x * transform.localScale.x > 0 ? -1 : 1,
            scale.y,
            scale.z);
    }
}
