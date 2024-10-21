using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlackjackGame : MonoBehaviour
{
    public GameObject cardPrefab; // Assignez le prefab de la carte dans l'inspecteur
    public Transform spawnCardPlayer; // Assignez les GameObjects SpawnCardPlayer et SpawnCardDealer
    public Transform spawnCardDealer;
    public float cardOffset = 100f; // Décalage entre les cartes
    public TextMeshProUGUI playerHandValueText;
    public TextMeshProUGUI dealerHandValueText;

    private Deck deck;
    private Player player;
    private Player dealer;
    private int playerCardCount = 0;
    private int dealerCardCount = 0;

    void Start()
    {
        deck = new Deck();
        player = new Player("Player");
        dealer = new Player("Dealer");

        StartGame();
    }

    void StartGame()
    {
        // Distribution initiale des cartes
        DrawCardForPlayer();
        DrawCardForPlayer();

        DrawCardForDealer();
        DrawCardForDealer();

        CheckWinCondition();
    }

    void DrawCardForPlayer()
    {
        Card card = deck.DrawCard();
        player.AddCardToHand(card);
        SpawnCardVisual(card, spawnCardPlayer, playerCardCount);
        playerCardCount++;
        DisplayHands();
    }

    void DrawCardForDealer()
    {
        Card card = deck.DrawCard();
        dealer.AddCardToHand(card);
        SpawnCardVisual(card, spawnCardDealer, dealerCardCount);
        dealerCardCount++;
        DisplayHands();
    }

    void SpawnCardVisual(Card card, Transform spawnPoint, int cardIndex)
    {
        // Instancie le prefab de la carte à l'emplacement spécifié
        GameObject cardObject = Instantiate(cardPrefab, spawnPoint.position, Quaternion.identity);
        cardObject.GetComponent<RectTransform>().anchoredPosition += new Vector2(cardIndex * cardOffset, 0);
        //SpriteRenderer spriteRenderer = cardObject.GetComponent<SpriteRenderer>();
        Image spriteRenderer = cardObject.GetComponent<Image>();

        // Change le sprite pour correspondre à la carte tirée
        Debug.Log($"Spirte : " + GetCardSprite(card));
        spriteRenderer.sprite = GetCardSprite(card);

        // Set Parent to the spawn point
        cardObject.transform.SetParent(spawnPoint);
    }

    Sprite GetCardSprite(Card card)
    {
        return Resources.Load<Sprite>($"Sprites/Cards/{card.Suit}_{card.Rank}");
    }

    void DisplayHands()
    {
        Debug.Log($"{player.Name}'s Hand: {string.Join(", ", player.Hand)} (Value: {player.GetHandValue()})");
        Debug.Log($"{dealer.Name}'s Hand: {string.Join(", ", dealer.Hand)} (Value: {dealer.GetHandValue()})");

        playerHandValueText.text = $"{player.GetHandValue()}";
        dealerHandValueText.text = $"{dealer.GetHandValue()}";
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
    public void PlayerHit()
    {
        //player.AddCardToHand(deck.DrawCard());
        CheckWinCondition();
        //Display new card
        DrawCardForPlayer();
    }
    // M�thode pour rester (le tour passe au dealer)
    public void PlayerStand()
    {
        DealerTurn();
        DetermineWinner();
    }
    void DealerTurn()
    {
        while (dealer.GetHandValue() < 17)
        {
            //dealer.AddCardToHand(deck.DrawCard());
            DrawCardForDealer();
        }
    }
    void DetermineWinner()
    {
        int playerValue = player.GetHandValue();
        int dealerValue = dealer.GetHandValue();
        Debug.Log("Score: " + playerValue + " - " + dealerValue);
        if (playerValue > 21)
        {
            Debug.Log("Player busts! Dealer wins!");
        }
        else if (dealerValue > 21)
        {
            Debug.Log("Dealer busts! Player wins!");
            Debug.Log("Player wins!");
        }
        else if (dealerValue > playerValue)
        {
            Debug.Log("Player wins!");
            
        }
        else if (playerValue < dealerValue)
        {
            Debug.Log("Dealer wins!");
        }
        else if (playerValue == dealerValue)
        {
            Debug.Log("It's a tie!");
        }
    }
}
