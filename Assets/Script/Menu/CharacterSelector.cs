using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    public void OnClickCharacterPick(int character) {
        if (PlayerInfo.PI != null) {
            PlayerInfo.PI.mySelectedCharacter = character;
            PlayerPrefs.SetInt("MyCharacter", character);
        }
    }
}
