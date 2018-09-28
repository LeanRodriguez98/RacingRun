using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleDamageable : Damageable
{
    private Obstacle obstacle;
    void Start()
    {
        obstacle = GetComponent<Obstacle>();
    }   

    public override void SetDamage(int damage)
    {
        obstacle.life -= damage;

    }
}
