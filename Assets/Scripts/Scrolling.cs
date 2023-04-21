using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrolling : MonoBehaviour
{
    public Transform _parent1, _parent2, _parent3;
    public List<Transform> _horizontal1, _horizontal2, _horizontal3, _vertical1, _vertical2, _vertical3;

    public void ScrollHorizontalRight(List<Transform> _children, Transform _parent)
    {
        _children.Clear();
        foreach (Transform _child in _parent)
        {
            _children.Add(_child);
        }

        Vector3 _firstObject = _children[0].position;

        for (int i = 0; i < _children.Count - 1; i++)
        {
            _children[i].position = _children[i + 1].position;
            _children[i].SetSiblingIndex(i + 1);
        }

        _children[_children.Count - 1].position = _firstObject;
        _children[_children.Count - 1].SetSiblingIndex(0);
        _children.Clear();
    }

    public void ScrollVerticalDown(List<Transform> _vertical, int _childindex)
    {
        _vertical.Clear();
        _vertical.Add(_parent1.GetChild(_childindex));
        _vertical.Add(_parent2.GetChild(_childindex));
        _vertical.Add(_parent3.GetChild(_childindex));

        Vector3 _firstObject = _vertical[0].position;

        for (int i = 0; i < _vertical.Count - 1; i++)
        {
            _vertical[i].position = _vertical[i + 1].position;
        }

        _vertical[_vertical.Count - 1].position = _firstObject;

        _vertical[0].SetParent(_parent2);
        _vertical[0].SetSiblingIndex(_childindex);
        _vertical[1].SetParent(_parent3);
        _vertical[1].SetSiblingIndex(_childindex);
        _vertical[2].SetParent(_parent1);
        _vertical[2].SetSiblingIndex(_childindex);

        _vertical.Clear();
    }

    public void WinCondition()
    {
        if (_parent1.GetChild(0).GetComponent<Transform>().tag == "Red" & _parent1.GetChild(1).GetComponent<Transform>().tag == "Red" & _parent1.GetChild(2).GetComponent<Transform>().tag == "Red" &
            _parent2.GetChild(0).GetComponent<Transform>().tag == "Yellow" & _parent2.GetChild(1).GetComponent<Transform>().tag == "Yellow" & _parent2.GetChild(2).GetComponent<Transform>().tag == "Yellow" &
            _parent3.GetChild(0).GetComponent<Transform>().tag == "Green" & _parent3.GetChild(1).GetComponent<Transform>().tag == "Green" & _parent3.GetChild(2).GetComponent<Transform>().tag == "Green")
        {
            Debug.Log("WIN");
        }
    }

    public void ScrollHorizontal1()
    {
        ScrollHorizontalRight(_horizontal1, _parent1);
        WinCondition();
    }

    public void ScrollHorizontal2()
    {
        ScrollHorizontalRight(_horizontal2, _parent2);
        WinCondition();
    }

    public void ScrollHorizontal3()
    {
        ScrollHorizontalRight(_horizontal3, _parent3);
        WinCondition();
    }

    public void ScrollVertical1()
    {
        ScrollVerticalDown(_vertical1, 0);
        WinCondition();
    }

    public void ScrollVertical2()
    {
        ScrollVerticalDown(_vertical2, 1);
        WinCondition();
    }

    public void ScrollVertical3()
    {
        ScrollVerticalDown(_vertical3, 2);
        WinCondition();
    }
}
