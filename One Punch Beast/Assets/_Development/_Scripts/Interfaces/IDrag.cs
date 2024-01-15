using NUnit.Framework;
using UnityEngine;

public interface IDrag
{
    void StackIncrease();
    int MaxStack();
    int CurrentStack();

    int Release();
}
