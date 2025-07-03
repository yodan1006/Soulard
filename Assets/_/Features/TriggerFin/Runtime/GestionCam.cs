using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class GestionCam : MonoBehaviour
{
    [SerializeField] private CinemachineCamera _cameraInGame;
    [SerializeField] private CinemachineCamera _cameraDeFin;
    [SerializeField] private GameObject _Player;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cameraInGame.Priority = 0;
            _cameraDeFin.Priority = 50;
            
            _Player.GetComponent<Animator>().SetBool("IsWin", true);
            _Player.GetComponent<PlayerInput>().enabled = false;
            _Player.GetComponent<Collider>().enabled = false;
        }
    }
}
