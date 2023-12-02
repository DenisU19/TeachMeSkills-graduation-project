using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawberrySkin : SkinData
{
    private void Awake()
    {
        _eventBus.OnPlayerDataLoaded += GetSavedSkinData;

        gameObject.SetActive(false);

    }

    public void GetSavedSkinData()
    {
        _isSkinActive = _playerDataForSave.IsStrawberrySkinActive;
        _isSkinBuy = _playerDataForSave.IsStrawberrySkinBougth;

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
