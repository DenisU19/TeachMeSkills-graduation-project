using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueJacketSkin : SkinData
{
    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedSkinData;

        gameObject.SetActive(false);

    }

    public void GetSavedSkinData()
    {
        _isSkinActive = _playerDataForSave.IsBlueJacketSkinActive;
        _isSkinBuy = _playerDataForSave.IsBlueJacketSkinBougth;

        if (_isSkinActive == true)
        {
            gameObject.SetActive(true);
        }
    }

    private void OnDestroy()
    {
        _eventBus.OnPlayerDataLoaded -= GetSavedSkinData;
    }
}
