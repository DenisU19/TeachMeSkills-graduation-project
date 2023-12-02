using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurroundingObjectsTeleport : MonoBehaviour
{
    [SerializeField]
    private TeleportedObjectData[] _teleportedObjects;

    private void Awake()
    {
        foreach (var teleportedObject in _teleportedObjects)
        {
            teleportedObject.FindTeleportedObject();
        }
    }

    public void TeleporObject()
    {
        for (int i = 0; i < _teleportedObjects.Length; i++)
        {
            _teleportedObjects[i]._objectForTeleport.transform.position = _teleportedObjects[i].NewObjectPosition.position;

            _teleportedObjects[i]._objectForTeleport.transform.rotation = _teleportedObjects[i].NewObjectPosition.rotation;
        }
    }
}
