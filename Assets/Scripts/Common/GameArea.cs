using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameArea 
{ 
    private const float PercentScreenForIndent = 0.05f;

    private Vector2 _leftDownPoint;
    private Vector2 _rightUpPoint;

    public void CalculateGameArea(Camera camera)
    {
        Vector2 screen = camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));

        float indent = screen.y * PercentScreenForIndent;

        _leftDownPoint = new Vector2(-screen.x + indent, -screen.y + indent);
        _rightUpPoint = new Vector2(screen.x - indent, screen.y - indent);
    }

    public Vector2 GetRandomPoint()
    {
        float pointX = Random.Range(_leftDownPoint.x, _rightUpPoint.x);
        float pointY = Random.Range(_leftDownPoint.y, _rightUpPoint.y);
        
        return new Vector2(pointX, pointY);
    }
}
