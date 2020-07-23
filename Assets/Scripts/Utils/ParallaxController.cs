using System;
using UnityEngine;

namespace Utils
{
    public class ParallaxController : MonoBehaviour
    {

        public GameObject cam;
        public float parallaxEffect;
        private float _length;
        private float _startPos;

        private void Start()
        {
            _startPos = transform.position.x;
            _length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            var position = cam.transform.position;
            var temp = (position.x * (1 - parallaxEffect));
            var dist = (position.x * parallaxEffect);

            transform.position = new Vector3(_startPos + dist, transform.position.y, transform.position.z);

            if (temp>_startPos+_length)
            {
                _startPos += _length;
            }
            else if (temp<_startPos-_length)
            {
                _startPos -= _length;
            }

        }
    }
}
