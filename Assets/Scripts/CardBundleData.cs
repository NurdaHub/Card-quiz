using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundleData", menuName = "Card Bundle Data", order = 10)]
public class CardBundleData : ScriptableObject
{
    [SerializeField] private CardData[] _cardDatas;
    public CardData[] cardDatas => _cardDatas;
}