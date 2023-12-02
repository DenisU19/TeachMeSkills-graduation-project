using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class SkinData : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected PlayerDataForSave _playerDataForSave;
    [SerializeField]
    protected Image _skinImage;
    [SerializeField]
    protected string _skinName;
    [SerializeField]
    protected string _skinDescription;
    [SerializeField]
    private int _skinPrice;
    [SerializeField][Header("В процентах")]
    protected float _skinEffectValue;
    [SerializeField]
    protected float _currentSkinEffectValue = 1;

    public float CurrentSkinEffectValue => _currentSkinEffectValue;
    //public float _skinCurrentEffectValue { get { return _skinEffectValue; } private set { value = _skinEffectValue; } }
    public bool _isSkinBuy { get; protected set; }
    public bool _isSkinActive { get; protected set; }
    public int SkinPrice => _skinPrice;
    public string SkinName => _skinName;
    public string SkinDescription => _skinDescription;

    private void OnEnable()
    {
        SkinActivate();
    }

    protected void SkinActivate()
    {
        _currentSkinEffectValue = 1 + _skinEffectValue / 100;

        _isSkinActive = true;
    }

    public void SkinDisactivate()
    {
         _currentSkinEffectValue = 1f;

         _isSkinActive = false;
    }

    public void ChangeBuyStatus()
    {
       _isSkinBuy = true; 
    }
}
