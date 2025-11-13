

using System;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public String Title => data.name;
    public String Description => data.Description;
    public Sprite Image => data.Image;
    public List<Effect> Effects => data.Effects;
    public int Mana { get; private set; }
    private readonly CardData data;
    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.Mana;
        
    }

}
