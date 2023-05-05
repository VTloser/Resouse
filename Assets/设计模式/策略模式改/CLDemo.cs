using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CLDemo : MonoBehaviour
{

    public class Student
    {
        public string Name;
        public int    Age;
        public int    Class;
        public int    Garde;


        public Student(string Name, int Age, int Class, int Garde)
        {
            this.Name  = Name;
            this.Age   = Age;
            this.Class = Class;
            this.Garde = Garde;
        }

    }

    public delegate int MyDelegate(Student std);
    Func<Student, int> MyFunc;

    void Start()
    {
        
        Student[] std = new Student[]
        {
            new Student("小A",11,2,20),
            new Student("小P",22,20,2),
            new Student("小G",33,3,3),
            new Student("小T",44,4,4),
        };

        OutPut(std, Student => Student.Age);
        OutPut(std, _ => _.Class);
        OutPut(std, _ => _.Garde);

        //MyFunc += ReturnAge;
        MyFunc += ReturnClass;
        //MyFunc += ReturnGarde;
        OutPutA(std, MyFunc(new Student("小T", 44, 4, 4)));
    }


    public int ReturnAge( Student student )
    {
        return student.Age;
    }

    public int ReturnClass(Student student)
    {
        return student.Class;
    }

    public int ReturnGarde(Student student)
    {
        return student.Garde;
    }

    public void OutPut(Student[] students, MyDelegate myDelegate)
    {
        int    Max  = 0;
        string Name = "";
        foreach (var item in students)
        {
            if (myDelegate(item) > Max)
            {
                Max  = myDelegate(item);
                Name = item.Name;
            }
        }
        Debug.Log(Name);
    }

    public void OutPutA(Student[] students, int num)
    {
        int Max = 0;
        string Name = "";
        foreach (var item in students)
        {
            if (num > Max)
            {
                Max = num;
                Name = item.Name;
            }
        }
        Debug.Log(Name);
    }

}


