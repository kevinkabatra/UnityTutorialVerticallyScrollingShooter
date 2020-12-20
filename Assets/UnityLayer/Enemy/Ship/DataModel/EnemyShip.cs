namespace UnityLayer.Enemy.Ship.DataModel
{
    using Ammunition.DataModel;
    using UnityEngine;

  public abstract class EnemyShip : MonoBehaviour
  {
      protected float Health = 1;

      private void OnCollisionEnter2D(Collision2D collision)
      {
          HandleDamage(collision.gameObject.GetComponent<AmmunitionAbstract>());
      }

      private void HandleDamage(AmmunitionAbstract ammunition)
      {
          Health -= ammunition.damage;

          if (Health <= 0)
          {
              DestroyShip();
          }
      }

      protected virtual void DestroyShip()
      {
          Destroy(gameObject);
      }
  }
}