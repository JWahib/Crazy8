using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHand : MonoBehaviour
{
    public GameObject playerHand;
    public GameObject canvas;
    public RectTransform initPlayerHandRect;
    public float initPlayerHandWidth;
    public RectTransform playerHandRect;
    public float playerHandWidth;
    public RectTransform canvasRect;
    public float canvasWidth;
    public float actualHandWidth;
    public int oldNumCards;
    // Start is called before the first frame update
    void Start()
    {
        playerHand = GameObject.Find(this.transform.name);
        canvas = GameObject.Find("Canvas");
        playerHandRect = (RectTransform)playerHand.transform;
        initPlayerHandWidth = playerHandRect.rect.width;
        canvasRect = (RectTransform)canvas.transform;
        canvasWidth = canvasRect.rect.width;
        // oldNumCards = playerHand.transform.childCount;
        oldNumCards = 0;
        // Debug.Log("Drew " + oldNumCards);
        // reposition();
    }

    void reposition() {
        int numCards = playerHand.transform.childCount;
        if(numCards != oldNumCards) {
            playerHandWidth = initPlayerHandWidth * Mathf.Max((float)1.0 ,numCards);
            actualHandWidth = Mathf.Min(playerHandWidth, canvasWidth);
            float distWidth = actualHandWidth - ((float)initPlayerHandWidth - (actualHandWidth / numCards));
            float cardDist = distWidth / numCards;

            GameObject currCard;
            RectTransform currCardRect;
            float inset;
            for(int i = 0; i < numCards; i++) {
                currCard = playerHand.transform.GetChild(i).gameObject;
                currCardRect = (RectTransform)currCard.transform;
                inset = i*cardDist;
                currCardRect.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, inset, currCardRect.rect.width);
                // currCard.GetComponent<SpriteRenderer>().sortingOrder = -numCards;
            }
            oldNumCards = numCards;
        }
    }

    // Update is called once per frame
    void Update()
    {
        reposition();
    }
}
