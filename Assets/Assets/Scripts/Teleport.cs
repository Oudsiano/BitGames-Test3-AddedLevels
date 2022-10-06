using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Player _player;//указываем игрока
        [SerializeField] private Teleport _teleport;//указываем телепорт
        [SerializeField] private GameObject _teleportExit;//указываем точку выхода из телепорта
        /// <summary>
        /// метод телепорта
        /// </summary>
        private void PlayerTeleport()
        {
            var flatTeleportPosition = new Vector2(_teleport.transform.position.x, _teleport.transform.position.z);
            //приваиваем координаты телепорта переменной
            var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);
            //присваиваем координаты игрока переменной
            var flatExitTeleportPosition = new Vector3(_teleportExit.transform.position.x, _teleportExit.transform.position.y, _teleportExit.transform.position.z);
            //присваиваем координаты выхода из телепорта переменной
            if (flatPlayerPosition == flatTeleportPosition)//сравниваем координаты игрока и телепорта по 2 осям, если равно то...
            {
                _player.transform.position = flatExitTeleportPosition;//присваиваем игроку координаты выхода из телепорта
            }
            else return;
            

        }
        private void Update()
        {
            PlayerTeleport();//запускаем проверку в апдейт
        }
    }
}
