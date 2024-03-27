using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sail : MonoBehaviour
{
    public bool IsStatic => _isStatic;

    [SerializeField] private float _increaseModifier = 0.1f;
    [SerializeField] private float _decreaseModifier = 0.2f;
    [SerializeField] private bool _isStatic = false;

    private Transform _transform;
    private Transform _canvas;

    private Vector2 _canvasSize;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _canvas = GetComponentInChildren<Transform>();

        _canvasSize = _canvas.localScale;
        _transform.localScale = new Vector2(_transform.localScale.x, _canvasSize.y - _decreaseModifier);
    }

    public void Increase()
    {
        _canvas.localScale = new Vector2(_canvasSize.x, Mathf.Lerp(_canvas.localScale.y, _canvasSize.y + _increaseModifier, Time.deltaTime));
    } 
    public void Decrease()
    {
        _canvas.localScale = new Vector2(_canvasSize.x, Mathf.Lerp(_canvas.localScale.y, _canvasSize.y - _decreaseModifier, Time.deltaTime));
    }
    public void Rotate(Vector2 direction)
    {
        var angle = Mathf.Clamp(-direction.x * Mathf.Rad2Deg / 3, -Mathf.Rad2Deg / 3, Mathf.Rad2Deg / 3);
        _transform.localRotation = Quaternion.Lerp(_transform.localRotation, Quaternion.Euler(0, 0, angle), 0.5f * Time.deltaTime);
    }
}
