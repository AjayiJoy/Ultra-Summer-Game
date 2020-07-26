/*
 * Copyright (c) 2020 Joy Ajayi
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
* 
* Heading: This script is written to the game board which instantiates the the star title
* and the covering tile at random time interval
* Author: Joy Ajayi
* Online Repository: 
* File format:Assembly-CSharp (.cs)
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoleScript : MonoBehaviour
{
    public float[] xPositions; //Positions on x axis
    public float[] yPositions;
    public GameObject mole;
    public GameObject cover;
    public List<Vector2> positionOfMole = new List<Vector2>();
    int i = 0;
    public float currentTime;  //amount of time being spent in the game
    public float countdown = 6.0f;
    public Text time;
    public int cd;
    public GameObject timePanel;
    public GameObject youLosePanel;
    public GameObject youWinPanel;
    public GameObject inputPanel;
    public Text correctNumber;
    public Text correctNumberLose;

    void Start()
    {
        StartCoroutine(Spawned());
        time.text = "Time Left:" + countdown;
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        countdown -= Time.deltaTime;
         cd = (int)countdown;
        time.text = "Time Left:" + cd;

        if(currentTime >= 6.0f)
        {
            CancelInvoke("Spawn");
            
        }
        if(currentTime >= 6.5f)
        {
            CancelInvoke("CoverItup");
            timePanel.SetActive(false);
            inputPanel.SetActive(true);
        }
    }
    public IEnumerator Spawned()
    {
        InvokeRepeating("Spawn", 0.04f, Random.Range(0.8f, 1));
        InvokeRepeating("CoverItUp", 0.07f, Random.Range(0.8f, 1));
        yield return null;
    }

    void Spawn()
    {
        int randRow = Random.Range(0, xPositions.Length); //random row for mole spawning
        int randColumn = Random.Range(0, yPositions.Length);
        Instantiate(mole, new Vector2(xPositions[randRow], yPositions[randColumn]), Quaternion.identity); //Spawning of the moles

        positionOfMole.Add(new Vector2(xPositions[randRow], yPositions[randColumn]));
    }

    void CoverItUp()
    {
        Vector2 newPlace = positionOfMole[i]; //Original position of mole
        Instantiate(cover, newPlace, Quaternion.identity);
        i++;
    }

    public void InputAnswer()
    {
        string number = GameObject.Find("InputField").GetComponent<InputField>().text; //Collects data from input field
        int num = int.Parse(number); //Converted the string to input

        if(num == positionOfMole.Count)
        {
            YouWon();
        }
        else
        {
            YouLose();
        }
    }

    void YouWon()
    {
        correctNumber.text = "" + positionOfMole.Count; //Sets the value of the count of the position of mole
        youWinPanel.SetActive(true);
        
    }

    void YouLose()
    {
        correctNumberLose.text = "" + positionOfMole.Count; //Sets the value of the count of the position of mole
        youLosePanel.SetActive(true);
    }

}
