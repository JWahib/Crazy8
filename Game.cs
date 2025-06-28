using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static int rand1;
    public static int rand2;
    public static GameObject playerHand;
    public static GameObject opponentHand;
    public static GameObject deck;
    public static GameObject playedCards;
    public static GameObject wildSelect;
    public static GameObject currPlayedCard;
    public static bool isPlayerTurn;
    public static bool hasDrawn;
    public static string turnModifier;
    public static string clickedCard;
    public static char wildSuit;


    public static GameObject pickRandomCard(GameObject hand) {
        int maxNum = deck.transform.childCount;
        int sortingOrder = hand.transform.childCount;
        if (maxNum >= 1) {
            rand1 = Random.Range(0, maxNum);
            Transform drawnCard = deck.transform.GetChild(rand1);
            // Move child to hand
            drawnCard.SetParent(hand.transform, false);
            // Debug.Log("Drew " + drawnCard.name);
            drawnCard.gameObject.SetActive(true);
            drawnCard.gameObject.GetComponent<SpriteRenderer>().sortingOrder = sortingOrder;
            drawnCard.gameObject.GetComponent<BoxCollider2D>().layerOverridePriority = sortingOrder;
            drawnCard.position += new Vector3(0f,0f,-0.01f*sortingOrder);
            return drawnCard.gameObject;
        }
        else {
            reshuffleDeck();
            // pickRandomCard(hand);
            return pickRandomCard(hand);
        }
    }

    public static void reshuffleDeck() {
        int numPlayedCards = playedCards.transform.childCount - 1;
        Debug.Log("Deck is Empty! Reshuffling " + numPlayedCards + " cards...");
        for(int i = 0; i < numPlayedCards; i++)
        {
            Transform currCard = playedCards.transform.GetChild(0);
            currCard.SetParent(deck.transform, false);
        }
    }
    
    public static void drawTwo() {
        // Debug.Log("Game Draw");
        // if (isPlayerTurn && hasDrawn == false) {
        //     pickRandomCard(playerHand);
        //     pickRandomCard(playerHand);
        //     hasDrawn = true;
        // }
    }

    public void deal() {
        //deal 5 cards at start of game
        for (int i = 0; i < 5; i++) {
            pickRandomCard(playerHand);
            pickRandomCard(opponentHand);
        }
    }

    public static void passTurn() {
        if (hasDrawn) {
            isPlayerTurn = false;
        }
        else {
            // TODO: Replace with in-game prompt
            Debug.Log("Draw first!!");
        }
    }

    public void playerTurn(GameObject handCard) {
        playCard(handCard);
        isPlayerTurn = false;
        if(turnModifier == "Draw") {
            pickRandomCard(opponentHand);
            pickRandomCard(opponentHand);
        }
        if(turnModifier == "Skip") {
            isPlayerTurn = true;
        }
        if (handCard.name[0] != '8' && cardCanPlay(handCard))
        {
            wildSuit = 'N';
        }
        Debug.Log("End Player Turn");
    }

    public bool cardCanPlay(GameObject card) {
        // name = "8C"
        if (card.name[0] == currPlayedCard.name[0] || 
            (card.name[1] == currPlayedCard.name[1] && currPlayedCard.name[0] != '8') || 
            ((card.name[1] == wildSuit && currPlayedCard.name[0] == '8')) ||
            card.name[0] == '8'
        ) {
            return true;
        }
        return false;
    }

    public IEnumerator waitRoutine()
    {
        // yield return new WaitForSeconds(.1f);
        yield return null;
    }

    public IEnumerator waitSecond()
    {
        Debug.Log("Wait Second");
        yield return new WaitForSeconds(3);
        // yield return null;
    }

    void waitForSuit() {
        if (wildSuit == 'N') {
            StartCoroutine(waitRoutine());
            // continue;
        }
        else {
            Debug.Log("Button pressed! - " + wildSuit);
        }
        // waitForSuit();
    }

    // TODO: When 8 is played, trigger prompt for hearts/clubs/diamonds/spades
    void selectSuit() {
        // disable player hand and buttons until selection is made
        Debug.Log("Disabling buttons...");
        playerHand.gameObject.SetActive(false);
        GameObject.Find("Pass Button").gameObject.GetComponent<Button>().interactable = false;
        GameObject.Find("Draw 2 Button").gameObject.GetComponent<Button>().interactable = false;
        // // drawButton.interactable = false;
        // GameObject.Find("Pass").gameObject.SetActive(false);
        // GameObject.Find("Draw2").gameObject.SetActive(false);
        Debug.Log("wildSuit = " + wildSuit);
        wildSelect.gameObject.SetActive(true);
        // waitForSuit();
        // while (wildSuit == 'N') {
        //     continue;
        // }
    }

    // TODO: When Suit is selected with an 8, depict chosen suit on screen with symbol
    // ... or "Skipped!" on 7
    // ... or "Draw 2!" on Ace
    void displayTurnModifier() {

    }
    public static char getWildSuit() {
        return wildSuit;
    }

    public static bool getHasDrawn() {
        return hasDrawn;
    }

    public static void setHasDrawn(bool val) {
        hasDrawn = val;
    }

    public static char calculateOpponentMostExpensiveSuit() {
        // Create a Dictionary to sum the total points for each suit in Opponent's hand
        IDictionary<char, int> suitSums = new Dictionary<char, int>();
        suitSums.Add('S', 0);
        suitSums.Add('H', 0);
        suitSums.Add('C', 0);
        suitSums.Add('D', 0);
        int val;
        // Loop through the Opponent's hand and calculate the total points for each suit
        for(int i = 0; i < opponentHand.transform.childCount; i++)
        {
            GameObject currCard = opponentHand.transform.GetChild(i).gameObject;
            Debug.Log("Counting Card " + currCard.name);
            switch (currCard.name[0])
            {
                case '1':
                    suitSums[currCard.name[1]] += 10;
                    Debug.Log("Adding " + 10 + " points to " + currCard.name[1]);
                    break;
                case 'J':
                    suitSums[currCard.name[1]] += 10;
                    Debug.Log("Adding " + 10 + " points to " + currCard.name[1]);
                    break;
                case 'Q':
                    suitSums[currCard.name[1]] += 10;
                    Debug.Log("Adding " + 10 + " points to " + currCard.name[1]);
                    break;
                case 'K':
                    suitSums[currCard.name[1]] += 10;
                    Debug.Log("Adding " + 10 + " points to " + currCard.name[1]);
                    break;
                case 'A':
                    suitSums[currCard.name[1]] += 11;
                    Debug.Log("Adding " + 11 + " points to " + currCard.name[1]);
                    break;
                case '8':
                    suitSums[currCard.name[1]] += 25;
                    Debug.Log("Adding " + 25 + " points to " + currCard.name[1]);
                    break;
                default:
                    // TODO: WHY ARE THEY NOT SUMMING CORRECTLY?
                    val = currCard.name[0] - '0';
                    suitSums[currCard.name[1]] += val;
                    Debug.Log("Adding " + val + " points to " + currCard.name[1]);
                    break;
            }
        }
        Debug.Log("Heart value suit = " + suitSums['H']);
        Debug.Log("Spade value suit = " + suitSums['S']);
        Debug.Log("Club value suit = " + suitSums['C']);
        Debug.Log("Diamond value suit = " + suitSums['D']);
        char maxSuit = 'X';
        int maxSuitVal = 0;
        foreach(KeyValuePair<char, int> kvp in suitSums) {
            if(kvp.Value > maxSuitVal) {
                maxSuitVal = kvp.Value;
                maxSuit = kvp.Key;
            }
        }
        // var maxSuit = suitSums.MaxBy(kvp => kvp.Value).Key;
        // var maxSuit = suitSums.Aggregate((l,r) => l.Value > r.Value ? l : r).Key;
        Debug.Log("Highest value suit = " + maxSuit);
        return maxSuit;
    }

    public void opponentTurn() {
        // StartCoroutine(waitSecond());
        GameObject cardToPlay = null;
        bool canPlay = false;
        for(int i = 0; i < opponentHand.transform.childCount; i++)
        {
            GameObject currCard = opponentHand.transform.GetChild(i).gameObject;
            if (cardCanPlay(currCard)) {
                cardToPlay = currCard;
                canPlay = true;
                break;
            }
        }
        // If can't play, draw 2
        if(canPlay == false) {
            // GameObject drawn1;
            // GameObject drawn2;
            GameObject drawn1 = pickRandomCard(opponentHand);
            GameObject drawn2 = pickRandomCard(opponentHand);
            if(cardCanPlay(drawn1)) {
                cardToPlay = drawn1;
            }
            if(!cardCanPlay(drawn1) && cardCanPlay(drawn2)) {
                cardToPlay = drawn2;
            }
        }
        isPlayerTurn = true;
        if (cardToPlay != null) {
            playCard(cardToPlay);
            if(cardToPlay.name[0] == '7') {
                turnModifier = "Skip";
            }
            if(cardToPlay.name[0] == '1') {
                turnModifier = "Rvrs";
            }
            if(cardToPlay.name[0] == '8') {
                turnModifier = "Wild";
                wildSuit = calculateOpponentMostExpensiveSuit();
            }
            else {
                wildSuit = 'N';
            }
            if(cardToPlay.name[0] == 'A') {
                turnModifier = "Draw";
            }
            Debug.Log("Opponent Played " + cardToPlay.name);
        }
        else {
            Debug.Log("Pass!");
        }
        if(turnModifier == "Draw") {
            Debug.Log("Player hit with Ace");
            pickRandomCard(playerHand);
            pickRandomCard(playerHand);
        }
        if(turnModifier == "Skip") {
            // WaitForSeconds(2);
            Debug.Log("Skipping Player Turn");
            isPlayerTurn = false;
        }
        // if (cardToPlay.name[0] != 8)
        // {
        //     wildSuit = 'N';
        // }
        hasDrawn = false;
        Debug.Log("End Opponent Turn");
    }

    void playCard(GameObject handCard){
        // Debug.Log("Valid Card!");
        currPlayedCard.gameObject.SetActive(false);
        // Debug.Log("Disabling " + currPlayedCard.name);
        Transform handCardTrans = handCard.transform;
        handCardTrans.SetParent(playedCards.transform, false);
        currPlayedCard = handCard;
        RectTransform handCardRect = (RectTransform)handCardTrans;
        handCardRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 0, handCardRect.rect.width);
        clickedCard = "0X";
        // Debug.Log("Played Card " + handCard.name + " at index = " + handCardTrans.GetSiblingIndex());
        // }
    }

    // Start is called before the first frame update
    void Start()
    {
        deck = GameObject.Find("Deck");
        playerHand = GameObject.Find("PlayerHand");
        opponentHand = GameObject.Find("OpponentHand");
        playedCards = GameObject.Find("PlayedCards");
        wildSelect = GameObject.Find("Wild Select");
        wildSelect.gameObject.SetActive(false);
        deal();
        currPlayedCard = pickRandomCard(playedCards);
        wildSuit = 'N';
        clickedCard = "0X";
        turnModifier = "";
        System.Random rnd = new System.Random();
        // bool playerTurn;
        if (rnd.Next(1,2) == 1) {
            isPlayerTurn = true;
            Debug.Log("Player start");
        }
        else {
            isPlayerTurn = false;
            Debug.Log("Opponent start");
        }
        hasDrawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        /* TODO:
        - Allow player to see cards in hand when selecting suit
        - Allow player to see Opponent's selected Suit
        - Add Ace stacking
        - Add initial card logic
        - Add win/loss screen
        - Add logic that initial card is played by "dealer"
        - BUG: Returning 8 with 8 breaks
        - BUG: Make waitSecond() work to add a break between turns
        */
        // Wait for Wild interface
        if (turnModifier == "Wild") {
            if (wildSuit == 'N') {
                waitForSuit();
                return;
            }
            else {
                Debug.Log("Reenabling buttons...");
                playerHand.gameObject.SetActive(true);
                GameObject.Find("Pass Button").gameObject.GetComponent<Button>().interactable = true;
                GameObject.Find("Draw 2 Button").gameObject.GetComponent<Button>().interactable = true;
                playerTurn(GameObject.Find(clickedCard));
                turnModifier = "";
                wildSelect.gameObject.SetActive(false);
                return;
            }
        }
        // Found card
        if (clickedCard != "0X" && isPlayerTurn) {
            Debug.Log("Start Player turn... ");
            Debug.Log(turnModifier);
            Debug.Log("Card on ground = " + currPlayedCard.name);
            // set turn modifiers
            if(clickedCard[0] == '7') {
                turnModifier = "Skip";
            }
            if(clickedCard[0] == '1') {
                turnModifier = "Rvrs";
            }
            if(clickedCard[0] == '8') {
                turnModifier = "Wild";
                Debug.Log("Played an 8 ");
                selectSuit();
            }
            if(clickedCard[0] == 'A') {
                turnModifier = "Draw";
            }
            // Wait for Wild interface
            if (turnModifier == "Wild" && wildSuit == 'N') {
                waitForSuit();
                return;
            }
            // Debug.Log("Start Player Turn");
            // play card
            playerTurn(GameObject.Find(clickedCard));
            turnModifier = "";
            // waitSecond();
        }
        if(!isPlayerTurn) {
            Debug.Log("Start Opponent turn... ");
            Debug.Log("Card on ground = " + currPlayedCard.name);
            // Debug.Log("Start Opponent Turn");
            opponentTurn();
            turnModifier = "";
        }
    }
}
