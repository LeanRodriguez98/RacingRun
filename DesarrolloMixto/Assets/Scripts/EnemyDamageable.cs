using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageable : Damageable
{
    private Enemy enemy;
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }   

    public override void SetDamage(int damage)
    {
        enemy.life -= damage;
    }
}
