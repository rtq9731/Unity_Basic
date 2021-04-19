using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momomo : MonoBehaviour
{

    void Start()
    {
        Student kim = new Student();
        Debug.Log(kim.Eat("오리"));
        kim.SetAddress("군포시 어딘가?");
        Debug.Log(kim.Where());

        Student Gwon = new Student("코코넛", "청주시 어딘가");
        Debug.Log(Gwon.Where());
    }

}
