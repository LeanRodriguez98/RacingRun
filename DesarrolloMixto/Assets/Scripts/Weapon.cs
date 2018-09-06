using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {

    Pool bulletPool;
   // RaycastHit hit;
    Vector3 direction;
    public void Fire() {
        bulletPool = PoolManager.instance.GetPool("BulletPool");
        GameObject gameObjectInstance = bulletPool.UseObj();
        gameObjectInstance.transform.position = transform.position;

        //if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 1000000))
          //  direction = hit.point;


        gameObjectInstance.GetComponent<Bullet>().Fire(-transform.right/*direction.normalized*/);
    }  
}
