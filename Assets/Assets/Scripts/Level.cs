using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Level : MonoBehaviour
    {
        [Header("Timer")]
        [SerializeField] private bool _timerIsOn;
        [SerializeField] private float _timerValue;
        [SerializeField] private Text _timerView;

        [Header("Objects")]
        [SerializeField] private Player _player;
        [SerializeField] private Exit _exitFromLevel;
        [SerializeField] public GameObject TextPaused;//текст паузы
        [SerializeField] public GameObject ButtonNextLevel;//кнопка следующего уровня
        [SerializeField] public GameObject ButtonRestart;//кнопка рестарта
        [SerializeField] public GameObject TextWin;//текст Победы
        [SerializeField] public GameObject TextLose;//текст Проигрыша

        private float _timer = 0;
        private bool _gameIsEnded = false;
        bool isPaused;//булевая паузы


        private void Awake()
        {
            _timer = _timerValue;
        }

        private void Start()
        {
            _exitFromLevel.Close();
        }
              

        private void TimerTick()
        {
            if (_timerIsOn == false)
                return;

            _timer -= Time.deltaTime;
            _timerView.text = $"{_timer:F1}";

            if (_timer <= 0)
                Lose();
        }

        private void TryCompleteLevel()
        {
            if (_exitFromLevel.IsOpen == false)
                return;

            var flatExitPosition = new Vector2(_exitFromLevel.transform.position.x, _exitFromLevel.transform.position.z);
            var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);

            if (flatExitPosition == flatPlayerPosition)
                Victory();
        }

        private void LookAtPlayerHealth()
        {
            if (_player.IsAlive)
                return;

            Lose();
            _player.gameObject.SetActive(false);
        }

        private void LookAtPlayerInventory()
        {
            if (_player.HasKey)
                _exitFromLevel.Open();
        }

        public void Victory()
        {
            _gameIsEnded = true;
            _player.Disable();
            //Debug.LogError("Victory");
            PauseGame(true);//присваиваем булевой паузы тру
            TextPaused.SetActive(true);//Включаем текст паузы
            ButtonNextLevel.SetActive(true);//включаем кнопку для перехода на следующий уровень
            ButtonRestart.SetActive(true);//включаем кнопку рестарта
            TextWin.SetActive(true);//включаем текст победы 
        }

        public void Lose()
        {
            _gameIsEnded = true;
            _player.Disable();
            //Debug.LogError("Lose");
            PauseGame(true);//присваиваем булевой паузы тру
            TextPaused.SetActive(true);//включаем кнопку для перехода на следующий уровень
            ButtonRestart.SetActive(true);//включаем кнопку рестарта
            TextLose.SetActive(true);//включаем текст проигрыша 


        }
        public void NextLevel()
        {
            PauseGame(false);
            var level = SceneManager.GetActiveScene().buildIndex + 1;//увеличение значения уровня
            SceneManager.LoadScene(level);//запуск сцены соответствующего уроня
        }
        
        void SetPause(bool pause)
        {
            PauseGame(pause);
            TextPaused.SetActive(pause);
        }
        void PauseGame(bool pause)
        {
            Time.timeScale = pause ? 0.0f : 1.0f;
            isPaused = pause;
        }
        
        public void RestartLevel()
        {
            PauseGame(false);
            var level = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(level);
        }
        private void Update()
        {
            if (_gameIsEnded)
                return;

            TimerTick();
            LookAtPlayerHealth();
            LookAtPlayerInventory();
            TryCompleteLevel();
            
            
        }
    }
}