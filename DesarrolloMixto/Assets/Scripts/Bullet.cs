using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    Vector3 dir;
    float maxLifeTime = 2.5f;
    float timer;
    public int bulletDamage;
	// Update is called once per frame
	void Update () {
        transform.position += dir;
        timer += Time.deltaTime;
        if (timer >= maxLifeTime)
        {
            GetComponent<PoolObject>().Recycle();
        }
	}

    public void Fire(Vector3 _dir) {
        timer = 0;
        dir = _dir;
    }
    public void OnTriggerEnter(Collider other)
    {
        Damageable damageable = other.GetComponent<Damageable>();
        if (damageable!= null)
        {
            damageable.SetDamage(bulletDamage);
        }
    }
   
}
