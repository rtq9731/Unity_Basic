using UnityEngine;

public class Student
{
    //º¯¼ö
    string email;
    int grade;
    string circle;
    string name;
    string trophy;
    string address;

    //internal == private
    public void SetAddress(string value)
    {
        address = value;
    }

    public Student() { }
    public Student(string name, string address)
    {
        this.name = name;
        this.address = address;
    }

    public string Eat(string food)
    {
        Debug.Log(food + "¸¦ ¸Ô½À´Ï´Ù.");
        return "²¨¾ï";
    }

    void Walk(int direction)
    {
        Debug.Log("¶Ñ¹÷¶Ñ¹÷");
    }

    public string Where()
    {

        return address;
    }
}
