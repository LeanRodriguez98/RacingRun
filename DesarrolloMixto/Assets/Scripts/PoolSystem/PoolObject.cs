using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolObject : MonoBehaviour {

    Pool myPool;

    public void SetPool(Pool pool) {
        myPool = pool;
    }

    public void Recycle() {
        myPool.AddToList(gameObject);
    }
}
