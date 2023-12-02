using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VendingMachineTeleporter : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private Transform _newMachinePosition;
    [SerializeField]
    private string _vendingMachineName;

    private GameObject _vendingMachineForTeleport;

    private void Start()
    {
        
    }
    public void TeleportVendingMachine()
    {
        _vendingMachineForTeleport.transform.position = _newMachinePosition.position;
    }
}
