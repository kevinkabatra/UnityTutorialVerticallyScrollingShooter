using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipPositionHandler : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        //var positionUpdateAction = new UnityAction(() => { UpdatePositionOfShip(newPosition); });
        var controller = VirtualController.Get();
        controller.positionUpdateEvent.AddListener(UpdatePositionOfShip);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdatePositionOfShip(Vector2 newPosition)
    {
        transform.position = newPosition;
    }
}
