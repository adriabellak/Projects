using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AllSprites", menuName = "ScriptableObjects/AllSprites", order = 1)]
public class AllSprites : ScriptableObject
{
    public Sprite[] wireSprites;
    public Sprite[] twoWiresSprites;
    public Sprite andSprite;
    public Sprite orSprite;
    public Sprite notSprite;
    public Sprite inputOnSprite;
    public Sprite inputOffSprite;
    public Sprite outputOnSprite;
    public Sprite outputOffSprite;
    public Sprite placeholderSprite;
    public Sprite placeholderBufferSprite;

    public Sprite bufferSprite;
}
