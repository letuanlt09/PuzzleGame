using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;
public class main : MonoBehaviour
{
   
    // Create Key GameObject 
    public GameObject[] key = new GameObject[4];
    //Create Value GameObject
    public GameObject[] value = new GameObject[4];
    // Create Win-Text
    public Text winText;
    // Create score-text
    public Text scoreText;
    //Create Data
    private Dictionary<string, string> data;

    //random array
    int[] arrRandKey;
    int[] arrRandValue;

    //isVisited array;
    private Dictionary<string, bool> isVisited;



    void Start()
    {
        //create arrRandKey
        Random rand = new Random();
        arrRandKey = new int[4];
        int indexRand = 0;
        while(indexRand<4)
        {
            int numRand = rand.Next(0, 4);
            bool isGood = true;
            for(int i = 0; i < indexRand;i++)
            {
                if(numRand == arrRandKey[i])
                {
                    isGood = false;
                    break;
                }
            }
            if(isGood)
            {
                arrRandKey[indexRand] = numRand;
                indexRand++;
            }
        }
        //create arrRandValue
        arrRandValue = new int[4];
        indexRand = 0;
        while (indexRand < 4)
        {
            int numRand = rand.Next(0, 4);
            bool isGood = true;
            for (int i = 0; i < indexRand; i++)
            {
                if (numRand == arrRandValue[i])
                {
                    isGood = false;
                    break;
                }
            }
            if (isGood)
            {
                arrRandValue[indexRand] = numRand;
                indexRand++;
            }
        }

        //initialize isVisited
        isVisited = new Dictionary<string, bool>();


        // getData
        data = this.gameObject.GetComponent<data>().getData();
        //fetch data to Keys and Values
        int index = 0;
        foreach (KeyValuePair<string, string> i in data)
        {
            // input data to text
            key[arrRandKey[index]].GetComponent<Text>().text = i.Key;
            value[arrRandValue[index]].GetComponent<Text>().text = i.Value;
            //update index
            index++;
            //add to isVisited
            isVisited.Add(i.Key, false);
        }
        // display score
        scoreText.text = "Score: " + score.ToString();

        

    }
    // if state is onKey
    bool onKey = false;
    string selectedKey;
    string selectedValue;
    GameObject preKeyObj;
    int score = 0;
    public void onClicked(GameObject button)
    {
        //Debug.Log(button.GetComponentInChildren<Text>().text);
        // change the highlightedColor when click on value
        
        if (Input.mousePosition.x < Screen.width/2 && isVisited[button.GetComponentInChildren<Text>().text] == false)
        {
            //set the highlightedColor
            var color = button.GetComponent<Button>().colors;
            color.highlightedColor = Color.blue;
            button.GetComponent<Button>().colors = color;
            //set onKey is true
            onKey = true;
            //set preKeyObj
            preKeyObj = button;
            //set selectedKey
            selectedKey = button.GetComponentInChildren<Text>().text;



        }
        // when onKey is true and the click on value
        if(Input.mousePosition.x > Screen.width/2 && onKey ==true)
        {
            
            //update the selectedValue
            selectedValue = button.GetComponentInChildren<Text>().text;
            //check data
            if(this.gameObject.GetComponent<data>().checkData(selectedKey,selectedValue))
            {
                //keep the blue color for normalColor of preKeyObj
                var color = preKeyObj.GetComponent<Button>().colors;
                color.normalColor = Color.blue;
                preKeyObj.GetComponent<Button>().colors = color;
                //change highlighedColor for the button 
                color = button.GetComponent<Button>().colors;
                color.highlightedColor = Color.blue;
                button.GetComponent<Button>().colors = color;
                //keep the blue color for normalColor of the button
                color = button.GetComponent<Button>().colors;
                color.normalColor = Color.blue;
                button.GetComponent<Button>().colors = color;
                //update the score
                score++;
                scoreText.text = "Score: " + score.ToString();
                //call the winText
                StartCoroutine(printWinText());
                //check if done all 4
                if (score == 4)
                    winText.text = "WELL DONE";
                //update isVisited
                isVisited[preKeyObj.GetComponentInChildren<Text>().text] = true;
                //reset onKey
                onKey = false;



            }
        }

    }

    IEnumerator printWinText()
    {
        winText.GetComponent<Text>().text = "Correct!!";
        yield return new WaitForSeconds(1);
        winText.GetComponent<Text>().text = "";

    }
    void Update()
    {
        
    }
   
}
