using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseRotator : MonoBehaviour
{
    public Direction direction = Direction.Right;

    public void setRotation(Direction dir)
    {
        direction = dir;
    }
}

public enum Direction
{
    Right, Left
}