using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSheet", menuName = "Scriptable Objects/CharacterSheet")]
public class CharacterSheet : ScriptableObject
{
      [Header("Char Info")]
      public string nameChar;
      public Sprite picChar;

      [Header("Answer Info")]
      public List<string> answers = new List<string>(); // aqui é onde guardamos as falas, preenchemos no inspetor

      [System.Serializable]
      public class Itens { // gerei uma classe personalizada para os itens do jogo
            public GameObject obj;
            public int linkedIndex;
            public bool itemLocked;
      }

      [Header("Item Info")]
      public List<Itens> item = new List<Itens>();

}
