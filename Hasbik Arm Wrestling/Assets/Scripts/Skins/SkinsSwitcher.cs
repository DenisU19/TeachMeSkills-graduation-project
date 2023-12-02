using UnityEngine;

public class SkinsSwitcher : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    public SkinData[] _allSkins;

    private SkinData _currentSkin;


    private void Start()
    {
        _eventBus.OnCurrentSkinChanged += ChangeCurrenSkin;
    }

    public void ChangeCurrenSkin(SkinData currentSkin)
    {
        _currentSkin = currentSkin;

        _currentSkin.gameObject.SetActive(true);

        foreach (SkinData skin in _allSkins)
        {
            if (skin != currentSkin)
            {
                skin.SkinDisactivate();
                skin.gameObject.SetActive(false);
            }
        }
    }

    public void GetSavedCurrentSkinData()
    {

    }
    private void OnDestroy()
    {
        _eventBus.OnCurrentSkinChanged -= ChangeCurrenSkin;
    }
}
