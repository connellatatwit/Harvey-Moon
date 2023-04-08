using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Score Entity", order = 1, fileName = "Plant Entity")]
public class ScoreEntity :  ScriptableObject
{
    [SerializeField] int value;
    [SerializeField] Sprite image;

    public Sprite Image
    {
        get { return image; }
    }
    public int Value
    {
        get { return value; }
    }
}
