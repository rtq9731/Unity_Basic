using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Momomo : MonoBehaviour
{

    void Start()
    {
        Student kim = new Student();
        Debug.Log(kim.Eat("����"));
        kim.SetAddress("������ ���?");
        Debug.Log(kim.Where());

        Student Gwon = new Student("���ڳ�", "û�ֽ� ���");
        Debug.Log(Gwon.Where());
    }

}
