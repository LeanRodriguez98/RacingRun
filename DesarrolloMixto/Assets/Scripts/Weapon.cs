using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {
    Pool bulletPool;    
    Vector3 direction;
    public void Fire(GameObject obstacle) {
        bulletPool = PoolManager.instance.GetPool("BulletPool");
        GameObject gameObjectInstance = bulletPool.UseObj();
        gameObjectInstance.transform.position = transform.position;

        direction = obstacle.transform.position - transform.position;
        direction.Normalize();


        gameObjectInstance.GetComponent<Bullet>().Fire(direction);
    }

   
}
