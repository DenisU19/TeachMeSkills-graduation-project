using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "EventBus", menuName = "EventBus")]
public class EventBus : ScriptableObject
{
    public Action OnJoystickTouchedStart;                                                // нажали на джойстик
    public Action OnJoystickTouchEnd;                                                    // отпустили джойстик

    public Action<int> OnEnergeticBusterCollected;                                       // подобрали бустер энергетик
    public Action<int> OnSodaBusterCollected;                                            // подобрали бустер газировку
    public Action<int> OnBarBusterCollected;                                             // подобрали бустер ботончик

    public Action<Vector3> OnBarCollectAnimationStarted;                                 // анимация подбора ботоничка стартовала
    public Action<Vector3> OnSodaCollectAnimationStarted;                                // анимация подбора газировки стартовала
    public Action<Vector3> OnEnergeticCollectAnimationStarted;                           // анимация подбора энергетика стартовала

    public Action<int> OnMoneyCollected;                                                 // подобрали монету
    public Action<int> OnMoneySpended;                                                   // потратили золото
    public Action<string> OnGoldMoneyViewRedraw;                                         // отрисовали количество золота 
    public Action<Vector3> OnCoinCollectAnimationStarted;                                // анимация подбора монеты сратовала

    public Action OnGoldMinerUnlocked;                                                   // разблокировали майнер золота
    public Action OnMiningMineyCollected;                                                // монеты из майнера собраны

    public Action OnRoomCompleted;                                                       // при открытии новой комнаты
    //public Action<GameObject> OnNextDoorRedrawed;                                      // перебросить надпись на дверь следующей комнаты
    public Action<GameObject, int> OnNextRoomSpawned;                                    // заспавнилась новая комната

    public Action OnFadeActivated;                                                       // затемнение экрана произошло
    public Action OnFadeDisactivated;                                                    // затемнение экрана закончилось
    public Action OnLoseFadeActivated;                                                   // затемнение экрана после проигрыша в qte произошло
    public Action OnWinFadeActivated;                                                    // затемнение экрана после победы в qte произошло

    public Action OnPlayerTeleportedStart;                                               // телепортировали игрока
    public Action OnPlayerTeleportedFinish;                                              // телепортировали игрока

    public Action<float> OnStrengthAdded;                                                // добавляется сила
    public Action OnStrengthUpgraded;                                                    // поднялся уровень силы;

    public Action OnExerciseExecuteStart;                                                // упражнение началось
    public Action<float, Transform> OnExerciseExecuting;                                 // упражнение делется 
    public Action OnExerciseExecuteEnd;                                                  // упражнение выполнилось
    public Action OnExerciseInterrupted;                                                 // упражнение прервалось
    public Action<Vector3, float, float> OnExerciseRewardCollected;

    public Action<float> OnWaterSpended;                                                 // тратится вода
    public Action OnWaterAdded;                                                          // добавляется вода

    public Action<SkinData> OnCurrentSkinChanged;                                        // сменили текущий скин    
    public Action OnNewSkinBought;                                                       // купили новый скин     

    public Action OnNewUpgradeBought;                                                    // купили улучшение
    public Action<PlayerVisualEffectsData.GameEvent> OnPlayerVisualEffectStart;          // запускам партиклы на игроке


    public Action<float> OnPlayerTaped;                                                   // игрок нажал при QTE
    public Action OnQTEStarted;                                                           // Мини игра началась
    public Action OnQTECompleted;                                                         // при прохождении QTE
    public Action OnQTEFailed;                                                            // при проигрыше QTE
    public Action<float> OnArmwrestlingEnemyActive;                                       // Соперник мешает нам
    public Action OnNextRoomStarted;                                                      // Начинаем новую комнату
    public Action OnCurrentRoomRestarted;                                                 // возвращаемся в сратую комнату


    public Action OnBusterBougth;                                                         // Купили бустер

    public Action OnPlayerDataLoaded;                                                     // данные игрока загружены
    public Action OnPlayerInitializated;                                                  // игрок впервые зашел в игру


    public Action OnExerciseButtonActivated;                                              // кнопка для упражнений появляется
    public Action OnExerciseButtonDisactivated;                                           // кнопка для упражнений исчезает
    public Action OnStopExerciseButtonPressed;                                            // нажали кнопку остановить упражнение
    public Action OnExerciseButtonPressed;                                                // Нажали кнопку упражнений


    public Action<GameObject, GameObject, float> OnPlayerTraceFigure;                     // Игрок ободит фигуру
    public Action OnPlayerStopTrace;                                                      // Игрок отпустил палец
    public Action OnPlayerStartTrace;                                                     // Игрок начал обводить фигуру
    //public Action OnPlayerDisactive;                                                    // Игрок приостановился
    public Action OnExerciseBoostActive;                                                  // активировалось ускорение при вождении пальцем
    public Action OnExerciseBoostDisactive;                                               // Деактивировалось ускорение при вождении пальцем

    public Action OnCameraMovedToPlayer;                                                  // Камера переместилась к игроку
    public Action OnCameraMovedToExerciser;                                               // Камера переместилась к тренажеру

    public Action OnRoomCountInitialized;                                                 // Загрузили номера комнат



}
