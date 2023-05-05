using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Context context = new Context(new OperationAdd());
        Debug.Log("10 + 5 = " + context.executeOperation(10 , 5));
                                                            
        context = new Context(new OperationSub());          
        Debug.Log("10 - 5 = " + context.executeOperation(10 , 5));
                                                            
        context = new Context(new OperationMul());          
        Debug.Log("10 * 5 = " + context.executeOperation(10 , 5));
    }


}
