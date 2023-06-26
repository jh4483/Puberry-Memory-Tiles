using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeTiles : MonoBehaviour
{
    public GameObject[] availableTiles;
    public GameObject[] availableLives;
    public Text timerCount;
    public static List<string> tileName = new List<string>();
    private string tileOne;
    private string tileTwo;
    public static bool isCheckingTiles;
    public int livesAvailable;
    public Text timerCount;
    public int timeLeft;

    void Start()
    {
        RearrangeTiles();
        livesAvailable = 0;
    }

    void Update()
    {
        if (ArrangeTiles.tileName.Count == 2 && !isCheckingTiles)
        {
            tileOne = ArrangeTiles.tileName[0];
            tileTwo = ArrangeTiles.tileName[1];
            StartCoroutine(CheckTiles());
        }

    }

    public void RearrangeTiles()
    {
        // Fisher-Yates shuffle
        int tileCount = availableTiles.Length;
        for (int i = 0; i < tileCount - 1; i++)
        {
            int randomIndex = Random.Range(i, tileCount);
            GameObject temp = availableTiles[randomIndex];
            availableTiles[randomIndex] = availableTiles[i];
            availableTiles[i] = temp;
        }

        // Update sibling index of the shuffled tiles
        for (int i = 0; i < tileCount; i++)
        {
            availableTiles[i].transform.SetSiblingIndex(i);
        }
    }

    public IEnumerator CheckTiles()
    {
        isCheckingTiles = true;
        ArrangeTiles.tileName.Clear();
        yield return new WaitForSeconds(0.5f);

        if (tileOne == tileTwo)
        {
            GameObject matchedTileOne = GameObject.Find(tileOne);
            matchedTileOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("Completed");
            matchedTileOne.name = "Completed";
            GameObject matchedTileTwo = GameObject.Find(tileTwo);
            matchedTileTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Completed");
            matchedTileTwo.name = "Completed";
        }
        else if (tileOne != tileTwo)
        {
            yield return new WaitForSeconds(1f);
            GameObject unmatchedTileOne = GameObject.Find(tileOne);
            unmatchedTileOne.GetComponent<Image>().sprite = Resources.Load<Sprite>("Original");
            unmatchedTileOne.name = tileOne.Replace(" added", "");
            unmatchedTileOne.GetComponent<FlipTiles>().isOpen = false;
            GameObject unmatchedTileTwo = GameObject.Find(tileTwo);
            unmatchedTileTwo.GetComponent<Image>().sprite = Resources.Load<Sprite>("Original");
            unmatchedTileTwo.name = tileTwo.Replace(" added", "");
            unmatchedTileTwo.GetComponent<FlipTiles>().isOpen = false;
        }

        isCheckingTiles = false;
    }

    public void RemoveLives()
    {
        Destroy(availableLives[livesAvailable]);
        livesAvailable++;
    }
}
