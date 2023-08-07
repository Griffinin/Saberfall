using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform spawnProj;
    private float timer;
    // Start is called before the first frame update

    // Update is called once per frame
 /*   private void Update()
    {
        timer += Time.deltaTime;
        if(timer > 2)
        {
            timer = 0;
            FireProjectile();
        }
    }*/

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
