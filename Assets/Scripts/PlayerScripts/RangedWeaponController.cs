using Managers;
using UnityEngine;

namespace PlayerScripts
{
    public class RangedWeaponController : MonoBehaviour
    {
        public Camera mainCamera;
        public float offset = -90f;

        public GameObject bullet;
        public Transform firePoint;
        public AudioClip bulletFireClip;

        public float fireRate = 1f;
        private float _fireCounter;
        private void Update()
        {
            var diff = mainCamera.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            var rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f,0f,rotZ + offset);

        
            if (_fireCounter <= 0)
            {
                if (Input.GetMouseButton(0))
                {
                    Instantiate(bullet, firePoint.position, transform.rotation);
                    AudioManager.Manager.PlaySfx(bulletFireClip);
                    _fireCounter = fireRate;
                }
            }
            else
            {
                _fireCounter -= Time.deltaTime;
            }
        
        
        
        }
    
    
    }
}
