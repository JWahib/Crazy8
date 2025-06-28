using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Game;

public class Draw2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnMouseDown() {
        if (Game.getHasDrawn() == false) {
            Game.pickRandomCard(Game.playerHand);
            Game.pickRandomCard(Game.playerHand);
            Game.setHasDrawn(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
