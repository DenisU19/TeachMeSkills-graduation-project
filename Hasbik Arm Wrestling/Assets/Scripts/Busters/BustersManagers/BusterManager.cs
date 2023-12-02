using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BusterViewDrawer))]
public class BusterManager : MonoBehaviour
{
    [SerializeField]
    protected EventBus _eventBus;
    [SerializeField]
    protected BustersSettings _bustersSettings;
    [SerializeField]
    protected PlayerDataForSave _playerDataForSave;
    [SerializeField]
    protected int _startBusterCount;

    protected BusterViewDrawer _busterViewDrawer;
    protected float _busterEffectValue;
    protected float _busterActivityTime;
    protected int _currentBusterCount;

    public int CurrentBusterCount => _currentBusterCount;
    public float CurrentBusterEffectValue { get; private set; }

    public static int AllUsedBusters;

    public virtual void Awake()
    {
        _busterViewDrawer = GetComponent<BusterViewDrawer>();

        _currentBusterCount = _startBusterCount;

        CurrentBusterEffectValue = 1;

        _busterViewDrawer.RedrawBusterView(_currentBusterCount);
    }


    public void AddBuster(int busterCount)
    {
        _currentBusterCount += busterCount;
        _busterViewDrawer.RedrawBusterView(_currentBusterCount);

        AllUsedBusters++;
    }

    public virtual void SpendBuster()
    {
        _currentBusterCount--;

        StartCoroutine(BusterActivator());

        StartCoroutine(_busterViewDrawer.BusterReloading(_busterActivityTime, _currentBusterCount));
    }

    public static void ResetUsedBusterCount()
    {
        AllUsedBusters = 0;
    }

    public IEnumerator BusterActivator()
    {
        CurrentBusterEffectValue = 1 + (_busterEffectValue / 100);

        yield return new WaitForSeconds(_busterActivityTime);

        CurrentBusterEffectValue = 1;
    }
}
