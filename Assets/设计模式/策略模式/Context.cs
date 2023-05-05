using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Context 
{
    private Operation operation;

    public Context(Operation operation)
    {
        this.operation = operation;
    }
    public int executeOperation(int num1, int num2)
    {
        return operation.DoOperation(num1, num2);
    }
}
