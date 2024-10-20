using UnityEngine;

public class BlackjackGame : MonoBehaviour
{
    private Deck deck;
    private Player player;
    private Player dealer;

    void Start()
    {
        // Initialisation
        deck = new Deck();
        player = new Player("Player");
        dealer = new Player("Dealer");

        StartGame();
    }

    void StartGame()
    {
        // Distribution initiale des cartes
        player.AddCardToHand(deck.DrawCard());
        player.AddCardToHand(deck.DrawCard());

        dealer.AddCardToHand(deck.DrawCard());
        dealer.AddCardToHand(deck.DrawCard());

        DisplayHands();

        // Vérification des résultats initiaux
        CheckWinCondition();
    }

    void DisplayHands()
    {
        Debug.Log($"{player.Name}'s Hand: {string.Join(", ", player.Hand)} (Value: {player.GetHandValue()})");
        Debug.Log($"{dealer.Name}'s Hand: {string.Join(", ", dealer.Hand)} (Value: {dealer.GetHandValue()})");
    }

    void CheckWinCondition()
    {
        int playerValue = player.GetHandValue();
        int dealerValue = dealer.GetHandValue();

        if (playerValue == 21)
        {
            Debug.Log("Blackjack! Player wins!");
        }
        else if (dealerValue == 21)
        {
            Debug.Log("Blackjack! Dealer wins!");
        }
        else if (playerValue > 21)
        {
            Debug.Log("Player busts! Dealer wins!");
        }
        else if (dealerValue > 21)
        {
            Debug.Log("Dealer busts! Player wins!");
        }
    }

    // Méthode pour tirer une carte pour le joueur
    public void PlayerHit()
    {
        player.AddCardToHand(deck.DrawCard());
        DisplayHands();
        CheckWinCondition();
    }

    // Méthode pour rester (le tour passe au dealer)
    public void PlayerStand()
    {
        DealerTurn();
        DisplayHands();
        DetermineWinner();
    }

    void DealerTurn()
    {
        while (dealer.GetHandValue() < 17)
        {
            dealer.AddCardToHand(deck.DrawCard());
        }
    }

    void DetermineWinner()
    {
        int playerValue = player.GetHandValue();
        int dealerValue = dealer.GetHandValue();

        if (dealerValue > 21 || playerValue > dealerValue)
        {
            Debug.Log("Player wins!");
        }
        else if (playerValue < dealerValue)
        {
            Debug.Log("Dealer wins!");
        }
        else
        {
            Debug.Log("It's a tie!");
        }
    }
}
