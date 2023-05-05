using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Operation_Item : MonoBehaviour
{

}

public class OperationAdd : Operation
{
    public int DoOperation(int num1, int num2)
    {
        return num1 + num2;
    }
}

public class OperationSub : Operation
{
    public int DoOperation(int num1, int num2)
    {
        return num1 - num2;
    }
}

public class OperationMul : Operation
{
    public int DoOperation(int num1, int num2)
    {
        return num1 * num2;
    }
}

public class OperationDivi : Operation
{
    public int DoOperation(int num1, int num2)
    {
        if (num2 != 0)
            return num1 / num2;
        else
        {
            Debug.LogError("除数为零");
            return 0;
        }
    }
}