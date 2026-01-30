using Unity.VisualScripting;
using UnityEngine;

public class ToggleFunc : MonoBehaviour
{
      [SerializeField] private GameObject _target;

      private bool _controller = false;

      public void Toggle() { // é só uma função para altenar o estado de visibilidade do painel
            _controller = !_controller;
            _target.SetActive(_controller);
      }
}
