using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ELEMENT
{
    WATER = 0,
    EARTH = 1,
    WIND = 2,
    FIRE = 3
}


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Element", order = 1)]
public class Element : ScriptableObject
{
    public ELEMENT ID;
    public Sprite FaceSprite;
    public Color Color;
    public AudioClip sound;
}