using System.Collections.Generic;
using GreenGrey.FirebaseStorage;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

namespace GGFirebaseStorage.Samples.SaveLoadSample
{
    /// <summary>
    /// Контроллер сцены примера работы с сохранением и загрузкой данных в Firebase
    /// </summary>
    public class SaveLoadSampleController : MonoBehaviour
    {
        /// <summary>
        /// Ключ, под которым данные пользователя будут храниться в его документе на стороне Firebase
        /// </summary>
        private const string PLAYER_DATA_KEY = "playerData";

        [Header("Buttons")] 
        [SerializeField] private Button m_initButton;
        [SerializeField] private Button m_loadDataButton;
        [SerializeField] private Button m_uploadDataButton;
        [SerializeField] private Button m_deleteUserButton;
        [Header("Inputs")] 
        [SerializeField] private InputField m_playerName;
        [SerializeField] private InputField m_playerLevel;
        [SerializeField] private Slider m_playerLevelProgress;
        [Header("Texts")] 
        [SerializeField] private Text m_playerAuthId;

        /// <summary>
        /// Пример класса данных сохранения.
        /// Выполнен ввиде приватного класса исключительно в целях компактности примера
        /// </summary>
        private class PlayerData
        {
            /// <summary>
            /// Имя пользователя
            /// </summary>
            public string m_name;
            /// <summary>
            /// Уровень пользователя
            /// </summary>
            public int m_level;
            /// <summary>
            /// Прогресс на уровне
            /// </summary>
            public float m_levelProgress;
        }

        private void Awake()
        {
            m_initButton.onClick.AddListener(InitButtonAction);
            m_loadDataButton.onClick.AddListener(LoadButtonAction);
            m_uploadDataButton.onClick.AddListener(UploadButtonAction);
            m_deleteUserButton.onClick.AddListener(DeleteUserButtonAction);
        }

        private async void OnApplicationPause(bool _isPaused)
        {
            if (_isPaused && GgFirebaseStorage.isAuthorised)
                await GgFirebaseStorage.UploadDataAsync(GetData());
        }

        #region Buttons Actions

        /// <summary>
        /// Действие кнопки инициализации
        /// Инициализирует библиотеку и авторизует пользователя
        /// </summary>
        private void InitButtonAction()
        {
            // Пытаемся инициализировать библиотеку
            if (!GgFirebaseStorage.TryInit(true))
            {
                // В случае неудачи - сообщаем о ней и выходим из метода
                Debug.LogError("Cant init sdk. For more information see logs before");
                return;
            }

            // Подписываемся на события библиотеки
            GgFirebaseStorage.apiEvents.AuthoriseCompleteEvent += OnAuthoriseCompleteEvent;
            GgFirebaseStorage.apiEvents.AuthoriseErrorEvent += OnAuthoriseErrorEvent;
            GgFirebaseStorage.apiEvents.DataLoadedEvent += OnDataLoadedEvent;
            GgFirebaseStorage.apiEvents.DataLoadErrorEvent += OnDataLoadErrorEvent;
            GgFirebaseStorage.apiEvents.DataUploadedEvent += OnDataUploadedEvent;
            GgFirebaseStorage.apiEvents.DataUploadErrorEvent += OnDataUploadErrorEvent;

            // Вызываем авторизацию
            GgFirebaseStorage.Authorise();
        }

        /// <summary>
        /// Действие кнопки загрузки данных с сервера
        /// </summary>
        private void LoadButtonAction()
        {
            GgFirebaseStorage.LoadData();
        }

        /// <summary>
        /// Действие кнопки выгрузки данных на сервер
        /// </summary>
        private void UploadButtonAction()
        {
            // Получаем актуальные данные игрока
            var currentPlayerData = GetData();

            // Отправляем их на сервер Firebase
            GgFirebaseStorage.UploadData(currentPlayerData);
        }

        /// <summary>
        /// Действие кнопки удаления юзера
        /// Note:
        /// Добавлено для дебага на устройствах.
        /// Пользовательский интерфейс примера не предполагает обработку подобных событий
        /// Того же эффекта можно добиться с помощью очистки кэша билда на устройстве
        /// </summary>
        private void DeleteUserButtonAction()
        {
            GgFirebaseStorage.DeleteCurrentUser();
        }

        #endregion

        #region GGFirebaseStorage Events Handlers

        /// <summary>
        /// Обработчик события успешной инициализации
        /// </summary>
        /// <param name="_isNewPlayer">Отвечает на вопрос, новый ли это пользователь?</param>
        private void OnAuthoriseCompleteEvent(bool _isNewPlayer)
        {
            if (_isNewPlayer) // Обработка нового пользователя
            {
                Debug.Log($"New user was registered. Uploading default data to storage");

                var defaultData = GetData();

                GgFirebaseStorage.UploadData(defaultData);
            }
            else // Обработка существующего пользователя
            {
                GgFirebaseStorage.LoadData();
            }

            m_playerAuthId.text = GgFirebaseStorage.authId;
        }

        /// <summary>
        /// Обработчик события ошибки авторизации
        /// </summary>
        /// <param name="_error">Сообщение ошибки</param>
        private void OnAuthoriseErrorEvent(string _error)
        {
            Debug.LogError($"Error while authorisation: {_error}");
        }

        /// <summary>
        /// Обработчик события успешной загрузки данных
        /// </summary>
        /// <param name="_data">Данные</param>
        private void OnDataLoadedEvent(Dictionary<string, object> _data)
        {
            Debug.Log($"Data was loaded");
            SetupData(_data);
        }

        /// <summary>
        /// Обработчик события ошибки загрузки данных
        /// </summary>
        /// <param name="_error">Сообщение ошибки</param>
        private void OnDataLoadErrorEvent(string _error)
        {
            Debug.LogError($"Error while loading: {_error}");
        }

        /// <summary>
        /// Обработчик события успешной отправки данных
        /// </summary>
        private void OnDataUploadedEvent()
        {
            Debug.Log($"Data was uploaded");
        }

        /// <summary>
        /// Обработчик события ошибки отправки данных
        /// </summary>
        /// <param name="_error">Сообщение ошибки</param>
        private void OnDataUploadErrorEvent(string _error)
        {
            Debug.LogError($"Error while uploading: {_error}");
        }

        #endregion

        #region Private Controller Methods

        /// <summary>
        /// Метод упаковки данных из пользовательского интерфейса в формат, используемый в Firebase
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, object> GetData()
        {
            // Собираем данные со сцены
            var data = new PlayerData
            {
                m_name = m_playerName.text,
                m_level = int.Parse(m_playerLevel.text),
                m_levelProgress = m_playerLevelProgress.value
            };

            // Сериализуем их
            var dataJson = JsonConvert.SerializeObject(data);

            // Упаковываем в словарь, используемый в Firebase
            return new Dictionary<string, object>
            {
                { PLAYER_DATA_KEY, dataJson }
            };
        }
        
        /// <summary>
        /// Метод установки данных в пользовательский интерфейс
        /// </summary>
        /// <param name="_data">Данные</param>
        private void SetupData(Dictionary<string, object> _data)
        {
            // Валидируем данные (проверяем наличие нужного ключа)
            if (!_data.TryGetValue(PLAYER_DATA_KEY, out var serialisedData))
            {
                Debug.LogError($"Cant find player data in loaded data");
                return;
            }

            // Десериализуем данные
            var dataObj = JsonConvert.DeserializeObject<PlayerData>((string)serialisedData);
            if (dataObj == null)
            {
                Debug.LogError($"Cant deserialize player data:\n{(string)serialisedData}");
                return;
            }

            // Устанавливаем данные в пользовательский интерфейс
            m_playerName.text = dataObj.m_name;
            m_playerLevel.text = dataObj.m_level.ToString();
            m_playerLevelProgress.value = dataObj.m_levelProgress;
        }

        #endregion
    }
}