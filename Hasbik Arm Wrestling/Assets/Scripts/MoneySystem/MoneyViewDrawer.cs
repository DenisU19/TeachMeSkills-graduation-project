using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyViewDrawer : MonoBehaviour
{
    [SerializeField]
    private EventBus _eventBus;
    [SerializeField]
    private TextMeshProUGUI _goldCountText;

    private void Start()
    {
        _eventBus.OnGoldMoneyViewRedraw += RedrawGoldCount;
    }

    public void RedrawGoldCount(string newGoldCount)
    {
        _goldCountText.text = $"{newGoldCount}";
    }

    private void OnDestroy()
    {
        _eventBus.OnGoldMoneyViewRedraw -= RedrawGoldCount;

    }

}
