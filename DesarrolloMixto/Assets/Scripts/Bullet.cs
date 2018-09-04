using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {


    Vector3 dir;
    float maxLifeTime = 2.5f;
    float timer;

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
    public void OnCollisionEnter(Collision collision)
    {
        Damageable damageable = collision.collider.GetComponent<Damageable>();
        if (damageable!= null)
        {
            damageable.SetDamage(10);
        }
    }
}
