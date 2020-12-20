namespace UnityLayer.Enemy.Ship.DataModel
{
    using UnityEngine;

    public class SmallEnemyShip : EnemyShip
    {
        [SerializeField] private GameObject explosionAnimation;

        protected override void DestroyShip()
        {
            base.DestroyShip();
            var explosionInstance = Instantiate(explosionAnimation);
            explosionInstance.transform.position = gameObject.transform.position;
        }
    }
}