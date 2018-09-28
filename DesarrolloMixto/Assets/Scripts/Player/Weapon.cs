using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Weapon : MonoBehaviour {
    Pool bulletPool;    
    Vector3 direction;
    public void Fire(Vector3 Direction) {
        bulletPool = PoolManager.instance.GetPool("BulletPool");
        GameObject gameObjectInstance = bulletPool.UseObj();
        gameObjectInstance.transform.position = transform.position;
        gameObjectInstance.GetComponent<Bullet>().Fire(Direction);
    }

   
}
