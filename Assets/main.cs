using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Random = System.Random;
using UnityEngine.EventSystems;
public class main : MonoBehaviour
{

 

// Create Win-Text
    [SerializeField]
    private Text winText;


    [SerializeField]
    private GameObject key;
    [SerializeField]
    private GameObject value;
    [SerializeField]
    private GameObject canvas;

    //array of Button
    private GameObject[] keys;
    private GameObject[] values;
    //size
    private int size;

    
    //// Create score-text
    //public Text scoreText;
    //Create Data
    private Dictionary<string, string> data;



    //isVisited array;
    private Dictionary<string, bool> isVisited;



    void Start()
    {   // getData
        data = this.gameObject.GetComponent<data>().getData();
        //assign size
        size = data.Count;
        // call make buttons
        makeButton(size);

        
        addData(data,size);


        //// display score
        //scoreText.text = "Score: " + score.ToString();
        

    }


    private void makeButton(int size)
    {
        keys = new GameObject[size];
        values = new GameObject[size];
        //make event triggerKey
        EventTrigger[] eventTriggerKey = new EventTrigger[size];
        // make beginDrag
        EventTrigger.Entry[] beginDrag = new EventTrigger.Entry[size];
        // make drag
        EventTrigger.Entry[] drag = new EventTrigger.Entry[size];
        //make ondrop
        EventTrigger.Entry[] endDrag = new EventTrigger.Entry[size];

        //make event triggerValue
        EventTrigger[] eventTriggerValue = new EventTrigger[size];
        //make entryValue1
        EventTrigger.Entry[] entryValue1 = new EventTrigger.Entry[size];
        //make entryValue1
        EventTrigger.Entry[] entryValue2 = new EventTrigger.Entry[size];

        //
     

        // clone value
        for (int i = 0; i < size; i++)
        {
            //values
            values[i] = Instantiate(value) as GameObject;
            values[i].transform.SetParent(canvas.transform, false);
            values[i].transform.position = new Vector3(Screen.width * 3 / 4, Screen.height / (size + 1) + i * (Screen.height / (size + 1)), 0);
            //add event trigger
            eventTriggerValue[i] = values[i].AddComponent<EventTrigger>();

            //

            

            //pointer exit
            entryValue1[i] = new EventTrigger.Entry();
            entryValue1[i].eventID = EventTriggerType.PointerEnter;
            GameObject copy2 = values[i];
            entryValue1[i].callback.AddListener((data) => { valueExit(copy2); });
            eventTriggerValue[i].triggers.Add(entryValue1[i]);


            //pointer Enter
            entryValue2[i] = new EventTrigger.Entry();
            entryValue2[i].eventID = EventTriggerType.PointerEnter;
            GameObject copy4 = values[i];
            entryValue2[i].callback.AddListener((data) => { valueEnter(copy4); });
            eventTriggerValue[i].triggers.Add(entryValue2[i]);
        }

        //clone keys
        for (int i = 0; i < size; i++)
        {
            //keys
            keys[i] = Instantiate(key) as GameObject;
            keys[i].transform.SetParent(canvas.transform, false);
            keys[i].transform.position = new Vector3(Screen.width / 4, Screen.height / (size + 1) + i * (Screen.height / (size + 1)), 0);
            
            
            //add event trigger
            eventTriggerKey[i] = keys[i].AddComponent<EventTrigger>();
            
            //make beginDrag
            beginDrag[i] = new EventTrigger.Entry();
            beginDrag[i].eventID = EventTriggerType.BeginDrag;
            //make a copy of obj, if we don't use it, it doesn't work
            GameObject copy1 = keys[i];
            beginDrag[i].callback.AddListener((data) => { onBeginDrag(copy1); });
            eventTriggerKey[i].triggers.Add(beginDrag[i]);

            //make drag
            drag[i] = new EventTrigger.Entry();
            drag[i].eventID = EventTriggerType.Drag;
            //make a copy of obj, if we don't use it, it doesn't work
            GameObject copy2 = keys[i];
            drag[i].callback.AddListener((data) => { onDrag(copy2); });
            eventTriggerKey[i].triggers.Add(drag[i]);

            //make endDrag
            endDrag[i] = new EventTrigger.Entry();
            endDrag[i].eventID = EventTriggerType.EndDrag;
            //make a copy of obj, if we don't use it, it doesn't work
            GameObject copy3 = keys[i];
            endDrag[i].callback.AddListener((data) => { onDrop(copy3); });
            eventTriggerKey[i].triggers.Add(endDrag[i]);



        }

        //CanvasGroup.blocksRaycast = false;




    }



    string tempKey = "";
    string tempValue = "";
    //target obj to drag
    GameObject dragObj;
    GameObject valueObj;
    Vector3 oriPos;
    //if in drag
    //bool onDrag = false;
    bool answer = false;
    //all event function
    private void onDrag(GameObject obj)
    {
        obj.GetComponent<Button>().transform.position = Input.mousePosition;
        
    }
    private void onBeginDrag(GameObject obj)
    {
        if (isVisited[obj.GetComponentInChildren<Text>().text] == false)
        {
            tempKey = obj.GetComponentInChildren<Text>().text;
            //update dragObj, oriPos, drag
            dragObj = obj;
            oriPos = obj.GetComponent<Button>().transform.position;
        }
    }
    private void onDrop(GameObject obj)
    {
        obj.GetComponent<Button>().transform.position = oriPos;
    }
    //private void keyDown(GameObject obj)
    //{

    //    if (isVisited[obj.GetComponentInChildren<Text>().text] == false)
    //    {
    //        tempKey = obj.GetComponentInChildren<Text>().text;
    //        //update dragObj, oriPos, drag
    //        dragObj = obj;
    //        oriPos = obj.GetComponent<Button>().transform.position;
    //        onDrag = true;
    //    }
    //}
    //private void keyUp(GameObject obj)
    //{
    //    Debug.Log("keyUp");
    //    onDrag = false;
    //    tempKey = "";
    //    tempValue = "";
    //    Debug.Log(answer);
    //    if (!answer)
    //    {
    //        obj.GetComponent<Button>().transform.position = oriPos;

    //    }
    //    else
    //    {
    //        Vector3 tempPos = valueObj.GetComponent<Button>().transform.position;
    //        Debug.Log(tempPos);
    //        tempPos.x = tempPos.x -100;
    //        Debug.Log(tempPos);
    //        obj.GetComponent<Button>().transform.position = tempPos;
    //        isVisited[dragObj.GetComponentInChildren<Text>().text] = true;
    //        answer = false;
    //        StartCoroutine(printWinText());



    //    }
    //}
    public void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("Trigger");
    }
    private void valueExit(GameObject obj)
    {

        Debug.Log("OnPointerEnter");
        //tempValue = "";

    }
    private void valueEnter(GameObject obj)
    {
        //Debug.Log("enter");
        //if (onDrag)
        //{
        //    tempValue = obj.GetComponentInChildren<Text>().text;
        //    if (this.gameObject.GetComponent<data>().checkData(tempKey, tempValue))
        //    {
        //        Debug.Log("true");
        //        answer = true;
        //        valueObj = obj;
        //    }
        //    else
        //    {
        //        Debug.Log("false");
        //        answer = false;
        //    }
        //}
    }

    //to make a randdom array
    private void makeRandArr(int[] arr,int size)
    {
        Random rand = new Random();
        int indexRand = 0;
        while (indexRand < size)
        {
            int numRand = rand.Next(0, size);
            bool isGood = true;
            for (int i = 0; i < indexRand; i++)
            {
                if (numRand == arr[i])
                {
                    isGood = false;
                    break;
                }
            }
            if (isGood)
            {
                arr[indexRand] = numRand;
                indexRand++;
            }
        }
       
    }

    private void addData(Dictionary<string, string> data,int size)
    {
        //initialize isVisited
        isVisited = new Dictionary<string, bool>();
        //rand arr key
        int[] arrRandKey = new int[size]; 
        //rand arr values
        int[] arrRandValue = new int[size];
        // make arr rand 
        makeRandArr(arrRandKey,size);
        System.Threading.Thread.Sleep(50);
        makeRandArr(arrRandValue, size);
      
        int index = 0;
        foreach (KeyValuePair<string, string> i in data)
        {
            // input data to text
            keys[arrRandKey[index]].GetComponentInChildren<Text>().text = i.Key;
            values[arrRandValue[index]].GetComponentInChildren<Text>().text = i.Value;
            //update index
            index++;
            //add to isVisited
            isVisited.Add(i.Key, false);
        }

    }

    // if state is onKey
    //bool onKey = false;
    //string selectedKey;
    //string selectedValue;
    //GameObject preKeyObj;
    //int score = 0;
    //public void onClicked(GameObject button)
    //{
    //    //Debug.Log(button.GetComponentInChildren<Text>().text);
    //    // change the highlightedColor when click on value

    //    if (Input.mousePosition.x < Screen.width/2 && isVisited[button.GetComponentInChildren<Text>().text] == false)
    //    {
    //        //set the highlightedColor
    //        var color = button.GetComponent<Button>().colors;
    //        color.highlightedColor = Color.blue;
    //        button.GetComponent<Button>().colors = color;
    //        //set onKey is true
    //        onKey = true;
    //        //set preKeyObj
    //        preKeyObj = button;
    //        //set selectedKey
    //        selectedKey = button.GetComponentInChildren<Text>().text;



    //    }
    //    // when onKey is true and the click on value
    //    if(Input.mousePosition.x > Screen.width/2 && onKey ==true)
    //    {

    //        //update the selectedValue
    //        selectedValue = button.GetComponentInChildren<Text>().text;
    //        //check data
    //        if(this.gameObject.GetComponent<data>().checkData(selectedKey,selectedValue))
    //        {
    //            //keep the blue color for normalColor of preKeyObj
    //            var color = preKeyObj.GetComponent<Button>().colors;
    //            color.normalColor = Color.blue;
    //            preKeyObj.GetComponent<Button>().colors = color;
    //            //change highlighedColor for the button 
    //            color = button.GetComponent<Button>().colors;
    //            color.highlightedColor = Color.blue;
    //            button.GetComponent<Button>().colors = color;
    //            //keep the blue color for normalColor of the button
    //            color = button.GetComponent<Button>().colors;
    //            color.normalColor = Color.blue;
    //            button.GetComponent<Button>().colors = color;
    //            //update the score
    //            score++;
    //            scoreText.text = "Score: " + score.ToString();
    //            //call the winText
    //            StartCoroutine(printWinText());
    //            //check if done all 4
    //            if (score == 4)
    //                winText.text = "WELL DONE";
    //            //update isVisited
    //            isVisited[preKeyObj.GetComponentInChildren<Text>().text] = true;
    //            //reset onKey
    //            onKey = false;



    //        }
    //    }

    //}
    //display GOOD JOB
    IEnumerator printWinText()
    {
        winText.GetComponent<Text>().text = "Good Job!!";
        yield return new WaitForSeconds(1);
        winText.GetComponent<Text>().text = "";

    }


    void Update()
    {
        //update position to drag
       
    }
    

}
