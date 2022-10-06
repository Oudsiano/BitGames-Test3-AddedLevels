using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{

    public class Teleport : MonoBehaviour
    {
        [SerializeField] private Player _player;//��������� ������
        [SerializeField] private Teleport _teleport;//��������� ��������
        [SerializeField] private GameObject _teleportExit;//��������� ����� ������ �� ���������
        /// <summary>
        /// ����� ���������
        /// </summary>
        private void PlayerTeleport()
        {
            var flatTeleportPosition = new Vector2(_teleport.transform.position.x, _teleport.transform.position.z);
            //���������� ���������� ��������� ����������
            var flatPlayerPosition = new Vector2(_player.transform.position.x, _player.transform.position.z);
            //����������� ���������� ������ ����������
            var flatExitTeleportPosition = new Vector3(_teleportExit.transform.position.x, _teleportExit.transform.position.y, _teleportExit.transform.position.z);
            //����������� ���������� ������ �� ��������� ����������
            if (flatPlayerPosition == flatTeleportPosition)//���������� ���������� ������ � ��������� �� 2 ����, ���� ����� ��...
            {
                _player.transform.position = flatExitTeleportPosition;//����������� ������ ���������� ������ �� ���������
            }
            else return;
            

        }
        private void Update()
        {
            PlayerTeleport();//��������� �������� � ������
        }
    }
}
