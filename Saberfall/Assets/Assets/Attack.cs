using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage = 2000;
    public Vector2 knockback = new Vector2(0, 0);


    private void OnTriggerEnter2D(Collider2D collision)
    {

        UnitHealth damageable = collision.GetComponentInParent<UnitHealth>();
        if (damageable != null)
        {
            Vector2 deliveredKnockback = transform.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            bool gotHit = damageable.DamageUnit(damage, knockback);
            if (gotHit)
            {
                Debug.Log(collision.name + " hit for " + damage);
            }
        }


    }
}
