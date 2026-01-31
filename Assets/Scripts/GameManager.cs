using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using static CharacterSheet;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
      [Header("Info")]
      public List<CharacterSheet> pers = new List<CharacterSheet>();
      [SerializeField] private Transform _itemSpot;
      [SerializeField] private Transform _targetPos;
      [SerializeField] private float _speed;

      [Header("Textos")]
      public TMP_Text text; // aqui é onde aplicaremos a resposta do personagem

      [SerializeField] private int _control = 0;
      [SerializeField] private Image _portrait;

      private Sprite _pic;

      public List<GameObject> obtainedItem = new List<GameObject>();

      private void Start() {
            _pic = pers[_control].picChar;

            _portrait.sprite = _pic;
            _portrait.enabled = true;
      }

      public void changeWitness() {
            _portrait.enabled = false;
            text.text = "";
            
            _control = (_control + 1) % pers.Count;

            _pic = pers[_control].picChar;

            _portrait.sprite = _pic;
            _portrait.enabled = true;
      }

      public void CharAnswer(int a) {
            text.text = pers[_control].answers[a]; // muda a resposta de acordo com a pergunta

            bool hasLockedItem = pers[_control].item.Exists(i => i.linkedIndex == a && i.itemLocked);

            if (hasLockedItem) {
                  ManagerItem(a);
            }             
      }

      public void ManagerItem(int index) {
            var item = pers[_control].item
                .Find(i => i.linkedIndex == index && i.itemLocked);

            if (item == null)
                  return;

            bool alreadySpawned = obtainedItem.Contains(item.obj);
            if (alreadySpawned)
                  return;

            var obj = Instantiate(item.obj, _itemSpot.position, Quaternion.identity);
            obtainedItem.Add(item.obj); 

            StartCoroutine(MoveItem(obj));
      }

      IEnumerator MoveItem(GameObject obj) {
            while (Vector3.Distance(obj.transform.position, _targetPos.position) > 0.01f) {

                  obj.transform.position = Vector3.MoveTowards(
                      obj.transform.position,
                      _targetPos.position,
                      _speed * Time.deltaTime
                  );

                  yield return null; // espera próximo frame
            }
      }
}
