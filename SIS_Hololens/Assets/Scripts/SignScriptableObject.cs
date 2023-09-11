using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SignScriptableObject", menuName = "ScriptableObjects/Sign")]
public class SignScriptableObject : ScriptableObject
{
    public char character;
    public Sprite signSprite;
    public Animation signAnimation;

    public char getCharacter()
    {
        return character;
    }
}
