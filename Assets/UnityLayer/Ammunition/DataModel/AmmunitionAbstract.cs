namespace UnityLayer.Ammunition.DataModel
{
    using UnityEngine;

    public abstract class AmmunitionAbstract : MonoBehaviour
    {
        public float damage = 1f;

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

        private void OnTriggerEnter2D(Collider2D collisionCollider)
        {
            HandleCollision(collisionCollider.gameObject, collisionCollider);
        }

        private void HandleCollision(GameObject collisionGameObject, Collider2D collisionCollider)
        {
            if(
                collisionGameObject.CompareTag("Ammunition") ||
                collisionGameObject.CompareTag("Player")
            )
            {
                Physics2D.IgnoreCollision(collisionCollider, _thisCollider);
            }
            else
            {
                Destroy(gameObject);
            }
        }

    }
}