using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;

public delegate void UpdateStackEvent();

public class ObservableStack<T> : Stack<T>
{
    public event UpdateStackEvent OnPush; //event for pushing

    public event UpdateStackEvent OnPop; //event for popping

    public event UpdateStackEvent OnClear; //event for clearing stack

    public ObservableStack(ObservableStack<T> items) : base(items)
    {

    }

    public ObservableStack()
    {

    }

    public new void Push(T item)
    {
        base.Push(item);

        if (OnPush != null) //Makes sure something is listening to the event before we call it
        {
            OnPush(); //Calls the event
        }
    }

    public new T Pop()
    {
        T item = base.Pop();

        if (OnPop != null) //Makes sure something is listening to the event before we call it
        {
            OnPop();//Calls the event
        }

        return item;
    }

    public new void Clear()
    {
        base.Clear();

        if (OnClear != null)//Makes sure something is listening to the event before we call it
        {
            OnClear();//Calls the event
        }
    }
}
