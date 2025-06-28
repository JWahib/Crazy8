using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Game;

public class CardSprite : MonoBehaviour
{
    public Image m_Image;
    // Set in Unity to be back sprite
    public Sprite spriteS;
    public Sprite originalSprite;
    public Transform currPlayedCard;
    public GameObject playedCards;
    public Transform thisCard;
    public SpriteRenderer thisSpriteRenderer;
    // public static bool isTurn;
    // Start is called before the first frame update
    void Start()
    {
        // card = this;
        m_Image = this.GetComponent<Image>();
        thisSpriteRenderer = this.GetComponent<SpriteRenderer>();
        originalSprite = m_Image.sprite;
        thisSpriteRenderer.sprite = m_Image.sprite;
        thisSpriteRenderer.drawMode = SpriteDrawMode.Sliced;
        // Debug.Log("Sprite is " + thisSpriteRenderer.size);
        thisSpriteRenderer.size = new Vector2(75f, 105f);
    }

    private void OnMouseDown() {
        // thisSpriteRenderer.size += new Vector2(75f, 105f);
        Debug.Log("Clicked on " + thisCard.name);
        // Debug.Log("Mouse is at: " + Input.mousePosition);
        if (Game.isPlayerTurn == true && this.transform.parent.name == "PlayerHand") {
            // playCard();
            
            // int numPlayedCards = playedCards.transform.childCount;
            // currPlayedCard = playedCards.transform.GetChild(numPlayedCards-1);
            string currPlayedCardName = currPlayedCard.transform.name;
            // Leading digit [A|1-9] = Card Number
            char currPlayedCardNum = currPlayedCardName[0];
            // Second digit [C|D|H|S] = Suit
            char currPlayedCardSuit= currPlayedCardName[1];
            Debug.Log("Current Card = " + currPlayedCardName);
            if (Game.getWildSuit() == 'N')
            {
                Debug.Log("Looking for Non-Wild Suit: " + currPlayedCardSuit);
            }
            else
            {
                Debug.Log("Looking for Wild Suit: " + Game.getWildSuit());
            }
            // Check if card is playable
            if(thisCard.name[0].Equals(currPlayedCardNum) || 
                (thisCard.name[1].Equals(currPlayedCardSuit) && currPlayedCardNum != '8') || 
                (thisCard.name[1].Equals(Game.getWildSuit()) && currPlayedCardNum == '8') ||
                thisCard.name[0].Equals('8')
            ) {
                Debug.Log("Playing " + thisCard.name);
                // Send card to Game
                Game.clickedCard = thisCard.name;
            }
        }
    }

    // public GameObject sendCardToGame(GameObject)

    // Update is called once per frame
    void Update()
    {
        playedCards = GameObject.Find("PlayedCards");
        int numPlayedCards = playedCards.transform.childCount;
        // currPlayedCard = playedCards.transform.GetChild(numPlayedCards - 1);
        currPlayedCard = playedCards.transform.GetChild(playedCards.transform.childCount-1);
        thisCard = this.transform;
        if(this.transform.parent.name == "OpponentHand") {
            m_Image.sprite = spriteS;
            thisSpriteRenderer.sprite = spriteS;
        }
        else {
            m_Image.sprite = originalSprite;
            thisSpriteRenderer.sprite = originalSprite;
        }
    }
}
