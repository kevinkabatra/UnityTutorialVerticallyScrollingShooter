namespace UnityLayer.Explosions.DataModel
{
    using UnityEngine;

  public class SmallShipExplosion : MonoBehaviour
    {
        private void Start()
        {
            Destroy(gameObject, GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
        }
    }
}