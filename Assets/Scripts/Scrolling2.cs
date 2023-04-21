using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Scrolling2 : MonoBehaviour
{
    public Transform _parent1, _parent2, _parent3;
    public List<Transform> _horizontal1, _horizontal2, _horizontal3, _vertical1, _vertical2, _vertical3;
    public bool _click = false;
    public Camera _camera;
    public Text _text;
    private float _lastMousePositionX, _lastMousePositionY, _currentMousePositionX, _currentMousePositionY;
    public Transform _objecthit1 = null;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _currentMousePositionX = Input.mousePosition.x;
            _currentMousePositionY = Input.mousePosition.y;

            RaycastHit _hit;
            Ray _ray = _camera.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(_ray.origin, _ray.direction * 10000);

            if (Physics.Raycast(_ray, out _hit, 10000))
            {
                _objecthit1 = _hit.collider.transform;
            }
        }

        if(Input.GetMouseButton(0))
        {
            _lastMousePositionX = Input.mousePosition.x;
            _lastMousePositionY = Input.mousePosition.y;

            if (Mathf.Abs(_lastMousePositionX - _currentMousePositionX) > Mathf.Abs(_lastMousePositionY - _currentMousePositionY) & Mathf.Abs(_lastMousePositionX - _currentMousePositionX) > 200)
            {
                if (_lastMousePositionX > _currentMousePositionX)
                {
                    if (_objecthit1.parent.tag == "Loop1" & _click == false)
                    {
                        ScrollHorizontalRight(_horizontal1, _parent1);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.parent.tag == "Loop2" & _click == false)
                    {
                        ScrollHorizontalRight(_horizontal2, _parent2);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.parent.tag == "Loop3" & _click == false)
                    {
                        ScrollHorizontalRight(_horizontal3, _parent3);
                        WinCondition();
                        _click = true;
                    }
                }

                if (_lastMousePositionX < _currentMousePositionX)
                {
                    if (_objecthit1.parent.tag == "Loop1" & _click == false)
                    {
                        ScrollHorizontalLeft(_horizontal1, _parent1);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.parent.tag == "Loop2" & _click == false)
                    {
                        ScrollHorizontalLeft(_horizontal2, _parent2);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.parent.tag == "Loop3" & _click == false)
                    {
                        ScrollHorizontalLeft(_horizontal3, _parent3);
                        WinCondition();
                        _click = true;
                    }
                }
            }

            if (Mathf.Abs(_lastMousePositionX - _currentMousePositionX) < Mathf.Abs(_lastMousePositionY - _currentMousePositionY) & Mathf.Abs(_lastMousePositionY - _currentMousePositionY) > 200)
            {
                if (_lastMousePositionY > _currentMousePositionY)
                {
                    if (_objecthit1.GetSiblingIndex() == 0 & _click == false)
                    {
                        ScrollVerticalUp(_vertical1, 0);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.GetSiblingIndex() == 1 & _click == false)
                    {
                        ScrollVerticalUp(_vertical2, 1);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.GetSiblingIndex() == 2 & _click == false)
                    {
                        ScrollVerticalUp(_vertical3, 2);
                        WinCondition();
                        _click = true;
                    }
                }

                if (_lastMousePositionY < _currentMousePositionY)
                {
                    if (_objecthit1.GetSiblingIndex() == 0 & _click == false)
                    {
                        ScrollVerticalDown(_vertical1, 0);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.GetSiblingIndex() == 1 & _click == false)
                    {
                        ScrollVerticalDown(_vertical2, 1);
                        WinCondition();
                        _click = true;
                    }

                    if (_objecthit1.GetSiblingIndex() == 2 & _click == false)
                    {
                        ScrollVerticalDown(_vertical3, 2);
                        WinCondition();
                        _click = true;
                    }
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            _click = false;
        }
    }

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

    public void ScrollHorizontalLeft(List<Transform> _children, Transform _parent)
    {
        _children.Clear();
        foreach (Transform _child in _parent)
        {
            _children.Add(_child);
        }

        Vector3 _firstObject = _children[_children.Count - 1].position;

        for (int i = _children.Count - 1; i > 0; i--)
        {
            _children[i].position = _children[i - 1].position;
            _children[i].SetSiblingIndex(i - 1);
        }

        _children[0].position = _firstObject;
        _children[0].SetSiblingIndex(_children.Count - 1);
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

    public void ScrollVerticalUp(List<Transform> _vertical, int _childindex)
    {
        _vertical.Clear();
        _vertical.Add(_parent1.GetChild(_childindex));
        _vertical.Add(_parent2.GetChild(_childindex));
        _vertical.Add(_parent3.GetChild(_childindex));

        Vector3 _firstObject = _vertical[_vertical.Count - 1].position;

        for (int i = _vertical.Count - 1; i > 0; i--)
        {
            _vertical[i].position = _vertical[i - 1].position;
        }

        _vertical[0].position = _firstObject;

        _vertical[0].SetParent(_parent3);
        _vertical[0].SetSiblingIndex(_childindex);
        _vertical[1].SetParent(_parent1);
        _vertical[1].SetSiblingIndex(_childindex);
        _vertical[2].SetParent(_parent2);
        _vertical[2].SetSiblingIndex(_childindex);

        _vertical.Clear();
    }

    public void WinCondition()
    {
        if (_parent1.GetChild(0).GetComponent<Transform>().tag == "Red" & _parent1.GetChild(1).GetComponent<Transform>().tag == "Red" & _parent1.GetChild(2).GetComponent<Transform>().tag == "Red" &
            _parent2.GetChild(0).GetComponent<Transform>().tag == "Yellow" & _parent2.GetChild(1).GetComponent<Transform>().tag == "Yellow" & _parent2.GetChild(2).GetComponent<Transform>().tag == "Yellow" &
            _parent3.GetChild(0).GetComponent<Transform>().tag == "Green" & _parent3.GetChild(1).GetComponent<Transform>().tag == "Green" & _parent3.GetChild(2).GetComponent<Transform>().tag == "Green")
        {
            _text.text = "онаедю";
            Debug.Log("WIN");
        }
    }
}
