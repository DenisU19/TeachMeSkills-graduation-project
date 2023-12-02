using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EventBus", menuName = "EventBus")]
public class EventBus : ScriptableObject
{
    public Action OnJoystickTouchedStart;                                                // ������ �� ��������
    public Action OnJoystickTouchEnd;                                                    // ��������� ��������

    public Action<int> OnEnergeticBusterCollected;                                       // ��������� ������ ���������
    public Action<int> OnSodaBusterCollected;                                            // ��������� ������ ���������
    public Action<int> OnBarBusterCollected;                                             // ��������� ������ ��������

    public Action<Vector3> OnBarCollectAnimationStarted;                                 // �������� ������� ��������� ����������
    public Action<Vector3> OnSodaCollectAnimationStarted;                                // �������� ������� ��������� ����������
    public Action<Vector3> OnEnergeticCollectAnimationStarted;                           // �������� ������� ���������� ����������

    public Action<int> OnMoneyCollected;                                                 // ��������� ������
    public Action<int> OnMoneySpended;                                                   // ��������� ������
    public Action<string> OnGoldMoneyViewRedraw;                                         // ���������� ���������� ������ 
    public Action<Vector3> OnCoinCollectAnimationStarted;                                // �������� ������� ������ ���������

    public Action OnGoldMinerUnlocked;                                                   // �������������� ������ ������
    public Action OnMiningMineyCollected;                                                // ������ �� ������� �������

    public Action OnRoomCompleted;                                                       // ��� �������� ����� �������
    //public Action<GameObject> OnNextDoorRedrawed;                                      // ����������� ������� �� ����� ��������� �������
    public Action<GameObject, int> OnNextRoomSpawned;                                    // ������������ ����� �������

    public Action OnFadeActivated;                                                       // ���������� ������ ���������
    public Action OnFadeDisactivated;                                                    // ���������� ������ �����������
    public Action OnLoseFadeActivated;                                                   // ���������� ������ ����� ��������� � qte ���������
    public Action OnWinFadeActivated;                                                    // ���������� ������ ����� ������ � qte ���������

    public Action OnPlayerTeleportedStart;                                               // ��������������� ������
    public Action OnPlayerTeleportedFinish;                                              // ��������������� ������

    public Action<float> OnStrengthAdded;                                                // ����������� ����
    public Action OnStrengthUpgraded;                                                    // �������� ������� ����;

    public Action OnExerciseExecuteStart;                                                // ���������� ��������
    public Action<float, Transform> OnExerciseExecuting;                                 // ���������� ������� 
    public Action OnExerciseExecuteEnd;                                                  // ���������� �����������
    public Action OnExerciseInterrupted;                                                 // ���������� ����������
    public Action<Vector3, float, float> OnExerciseRewardCollected;

    public Action<float> OnWaterSpended;                                                 // �������� ����
    public Action OnWaterAdded;                                                          // ����������� ����

    public Action<SkinData> OnCurrentSkinChanged;                                        // ������� ������� ����    
    public Action OnNewSkinBought;                                                       // ������ ����� ����     

    public Action OnNewUpgradeBought;                                                    // ������ ���������
    public Action<PlayerVisualEffectsData.GameEvent> OnPlayerVisualEffectStart;          // �������� �������� �� ������


    public Action<float> OnPlayerTaped;                                                   // ����� ����� ��� QTE
    public Action OnQTEStarted;                                                           // ���� ���� ��������
    public Action OnQTECompleted;                                                         // ��� ����������� QTE
    public Action OnQTEFailed;                                                            // ��� ��������� QTE
    public Action<float> OnArmwrestlingEnemyActive;                                       // �������� ������ ���
    public Action OnNextRoomStarted;                                                      // �������� ����� �������
    public Action OnCurrentRoomRestarted;                                                 // ������������ � ������ �������


    public Action OnBusterBougth;                                                         // ������ ������

    public Action OnPlayerDataLoaded;                                                     // ������ ������ ���������
    public Action OnPlayerInitializated;                                                  // ����� ������� ����� � ����


    public Action OnExerciseButtonActivated;                                              // ������ ��� ���������� ����������
    public Action OnExerciseButtonDisactivated;                                           // ������ ��� ���������� ��������
    public Action OnStopExerciseButtonPressed;                                            // ������ ������ ���������� ����������
    public Action OnExerciseButtonPressed;                                                // ������ ������ ����������


    public Action<GameObject, GameObject, float> OnPlayerTraceFigure;                     // ����� ������ ������
    public Action OnPlayerStopTrace;                                                      // ����� �������� �����
    public Action OnPlayerStartTrace;                                                     // ����� ����� �������� ������
    //public Action OnPlayerDisactive;                                                    // ����� ��������������
    public Action OnExerciseBoostActive;                                                  // �������������� ��������� ��� �������� �������
    public Action OnExerciseBoostDisactive;                                               // ���������������� ��������� ��� �������� �������

    public Action OnCameraMovedToPlayer;                                                  // ������ ������������� � ������
    public Action OnCameraMovedToExerciser;                                               // ������ ������������� � ���������

    public Action OnRoomCountInitialized;                                                 // ��������� ������ ������



}
