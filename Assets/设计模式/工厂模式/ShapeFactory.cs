using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShapeFactory 
{

    //  我们明确地计划不同条件下创建不同实例时
    //使用 getShape 方法获取形状类型的对象
    public Shape getShape(string shapeType)
    {
        if (shapeType == null)
        {
            return null;
        }
        if (shapeType.Equals("CIRCLE"))
        {
            return new Circle();
        }
        else if (shapeType.Equals("RECTANGLE"))
        {
            return new Rectangle();
        }
        else if (shapeType.Equals("SQUARE"))
        {
            return new Square();
        }
        return null;
    }
}
