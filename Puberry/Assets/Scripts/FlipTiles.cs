using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlipTiles : MonoBehaviour
{
    private string backgroundImage;
    private string tileNameAdded;
    public bool isOpen;
    private int openCount;
    public int gameScore;
    ArrangeTiles tileArranger;

    void Start()
    {
        isOpen = false;
        openCount = 0;
        tileNameAdded = this.name + " added";
        backgroundImage = this.name;
        gameScore = 3;
        tileArranger = FindObjectOfType<ArrangeTiles>();
    }

    void Update()
    {
        if(openCount == 3)
        {
            tileArranger.RemoveLives();
            openCount = 0;
        }
    }

    public void TileFlip()
    {
        if(!isOpen && !ArrangeTiles.isCheckingTiles)
        {
            ArrangeTiles.tileName.Add(tileNameAdded);
            this.GetComponent<Image>().sprite = Resources.Load<Sprite>(backgroundImage);
            this.name = this.name + " added";
            isOpen = true;
            openCount++;
        }

        else 
        {
            return;
        }

    }
}
