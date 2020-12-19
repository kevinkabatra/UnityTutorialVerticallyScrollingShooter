namespace UnityLayer.Ammunition.DataModel
{
    using System;
    using UnityEngine;

    public abstract class AmmunitionAbstract : MonoBehaviour
    {
        private Collider2D _thisCollider;

        private void Start()
        {
            _thisCollider = GetComponent<Collider2D>();
        }

        private void Update()
        {
            var transformLocalCache = transform;
            var positionLocalCache = transformLocalCache.position;
            positionLocalCache = new Vector2
            {
                x = positionLocalCache.x,
                y = positionLocalCache.y + .1f
            };
            transformLocalCache.position = positionLocalCache;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ammunition"))
            {
                Physics2D.IgnoreCollision(collision.collider, _thisCollider);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}