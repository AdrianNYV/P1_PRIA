using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ClaseQuestion {
    //Serializamos todas los datos usando los nombres de los datos que recibimos de la API, 
    //6 datos por cada pregunta, category, type, difficulty, question, correct_answer e incorrect_answer
    public string category;
    public string type;
    public string difficulty;
    public string question;
    public string correct_answer;
    public string incorrect_answer;
}
