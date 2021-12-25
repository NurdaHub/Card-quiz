using System;
using UnityEngine;

[Serializable]
public class CardData
{
    [SerializeField] private string _identifier;
    [SerializeField] private Sprite _sprite;

    public string identifier => _identifier;
    public Sprite sprite => _sprite;
}