using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
      [Header("Textos")]
      public List<string> answers = new List<string>(); // aqui é onde guardamos as falas, preenchemos no inspetos
      public TMP_Text text;

      [System.Serializable]
      public struct Itens { // gerei uma classe personalizada para os itens do jogo
            public GameObject obj;
            public int linkedIndex;
            public bool itemLocked;
      }

      [Header("Objetos")]
      public List<Itens> itens = new List<Itens>();

      private void Start() { // procura os itens que devem começar bloqueados e os desativa
            foreach (var item in itens) {
                  if (item.itemLocked) {
                        item.obj.SetActive(false);
                  }
            }
      }

      public void CharAnswer(int index) {
            text.text = answers[index]; // muda a resposta de acordo com a pergunta

            foreach (var item in itens) { // ativa itens se necessário
                  if (item.linkedIndex == index && item.obj.activeSelf == false) {
                        item.obj.SetActive(true);
                  }
            }
      }
}
