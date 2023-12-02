using UnityEngine;
using System;

[Serializable]
public class TeleportedObjectData 
{
    [SerializeField]
    private string _objectName;
    [SerializeField]
    private Transform _newObjectPosition;

    public GameObject _objectForTeleport { get; private set; }

    public string ObjectName => _objectName;

    public Transform NewObjectPosition => _newObjectPosition;

    public void FindTeleportedObject()
    {
        _objectForTeleport = GameObject.Find(_objectName);
    }
}
