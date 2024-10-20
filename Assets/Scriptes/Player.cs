using System.Collections.Generic;

public class Player
{
    public List<Card> Hand { get; private set; }
    public string Name { get; private set; }

    public Player(string name)
    {
        Name = name;
        Hand = new List<Card>();
    }

    public void AddCardToHand(Card card)
    {
        Hand.Add(card);
    }

    public int GetHandValue()
    {
        int totalValue = 0;
        int aceCount = 0;

        foreach (Card card in Hand)
        {
            totalValue += card.Value;
            if (card.Rank == "Ace")
            {
                aceCount++;
            }
        }

        // Adjust for Aces if the total is over 21
        while (totalValue > 21 && aceCount > 0)
        {
            totalValue -= 10;
            aceCount--;
        }

        return totalValue;
    }

    public void ClearHand()
    {
        Hand.Clear();
    }
}
