namespace UnityLayer.Ammunition.DataModel
{
    using UnityEngine;

    public abstract class AmmunitionAbstract : MonoBehaviour
    {
        private void Update()
        {
            var transformLocalCache = transform;
            transformLocalCache.position = new Vector2
            {
                x = transformLocalCache.position.x,
                y = transformLocalCache.position.y + .1f
            };
        }
    }
}