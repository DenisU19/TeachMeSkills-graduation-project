using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void HideObject()
    {
        gameObject.SetActive(false);
    }
}
