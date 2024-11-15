using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewCharacterStats", menuName = "Stats/NewcharacterStats")]
public class CharacterStats : ScriptableObject
{

    public string characterName;

    public void ShowName()
    {
        Debug.Log("character name: " + characterName);
    }

}
