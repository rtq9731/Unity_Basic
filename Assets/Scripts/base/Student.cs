using UnityEngine;

public class Student
{
    //����
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
        Debug.Log(food + "�� �Խ��ϴ�.");
        return "����";
    }

    void Walk(int direction)
    {
        Debug.Log("�ѹ��ѹ�");
    }

    public string Where()
    {

        return address;
    }
}
