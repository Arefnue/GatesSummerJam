using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class BulletController : MonoBehaviour
    {
        public float speed;
        public float lifeTime;
        public LayerMask groundLayer;
        public float damage = 5;
        public AudioClip bulletDeathClip;
        public GameObject hitParticle;

        private void Start()
        {
            Invoke(nameof(Death),lifeTime);
        }


        private void Update()
        {

            if (GameManager.Manager.gameOver)
            {
                return;
            }
            
            transform.Translate(Vector2.up * (speed * Time.deltaTime));

            var groundCheckHit = Physics2D.CircleCast(transform.position, 0.3f, Vector2.up, 0, groundLayer);
        
            if (groundCheckHit)
            {
                Instantiate(hitParticle, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        
        }
    
        private void Death()
        {
            Instantiate(hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    
        private void OnDestroy()
        {
            AudioManager.Manager.PlaySfxBullet(bulletDeathClip);
        }
        
    }
}
