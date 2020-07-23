using EnemyScripts;
using UnityEngine;

namespace PlayerScripts
{
    public class PlayerMeleeAttack : MonoBehaviour
    {
        public float damage = 50;
        //public Transform attackPoint;
        //public float attackRange;
        //public LayerMask enemyLayer;

        //public float attackRate;
        //private float _nextAttackTime;

        //private void Update()
        //{
        //    if (Time.time >= _nextAttackTime)
        //    {
        //        if (Input.GetMouseButtonDown(0))
        //        {
        //            Attack();
        //            _nextAttackTime = Time.time + 1f / attackRate;
        //        }
        //    }
        //}


        //public void Attack()
        //{
        //    Collider2D hitEnemies = Physics2D.OverlapCircle(attackPoint.position, attackRange, enemyLayer);

        //    if (hitEnemies)
        //    {
        //        var enemyController = hitEnemies.gameObject.GetComponent<EnemyController>();
        //        enemyController.TakeDamage(meleeDamage);
        //    }


        //}

        //private void OnDrawGizmosSelected()
        //{
        //    if (attackPoint == null)
        //    {
        //        return;
        //    }
        //    Gizmos.color = Color.red;
        //    Gizmos.DrawWireSphere(attackPoint.position,attackRange);
        //}
    }
}
