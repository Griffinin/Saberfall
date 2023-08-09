using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject[] projectilePrefab;
    private int index = 0;
    public Transform spawnProj;
    // public GameObject projectilePrefab;
    void Update()
    {
        // Cycle through the available prefabs when buttons 1 and 2 are pressed
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            index = 0;   //knife
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            index = 1;    //sword
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            index = 2;    //sword2
        }
    }
        public void FireProjectile()
    {
        switch (index)
        {
            case 0:
                MenuController.removeItem(GameObject.Find("ItemList/Knife"));
                break;
            default:
                break;
        }
        GameObject proj = Instantiate(projectilePrefab[index], spawnProj.position, projectilePrefab[index].transform.rotation);
        Vector3 scale = proj.transform.localScale;
        proj.transform.localScale = new Vector3(
            scale.x * transform.localScale.x > 0 ? -1 : 1,
            scale.y,
            scale.z);

    }

    

}
