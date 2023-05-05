using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryPatternDemo :MonoBehaviour
{

    //  我们明确地计划不同条件下创建不同实例时

    private void Awake()
    {
        ShapeFactory shapeFactory = new ShapeFactory();

        //获取 Circle 的对象，并调用它的 draw 方法
        Shape shape1 = shapeFactory.getShape("CIRCLE");

        //调用 Circle 的 draw 方法
        shape1.Draw();

        //获取 Rectangle 的对象，并调用它的 draw 方法
        Shape shape2 = shapeFactory.getShape("RECTANGLE");

        //调用 Rectangle 的 draw 方法
        shape2.Draw();

        //获取 Square 的对象，并调用它的 draw 方法
        Shape shape3 = shapeFactory.getShape("SQUARE");
          
        //调用 Square 的 draw 方法
        shape3.Draw();
    }
}
