using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SwaPPawS.Entities;
using SwaPPawS.EntityActions;
using UniRx;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private ActionManager _actionManager;

    private ReactiveProperty<Vector2> _direction;
    private Entity _entity;

    void Awake()
    {
        _actionManager = GameObject.FindObjectOfType<ActionManager>();
        _entity = GetComponent<Entity>();
    }

    void Start()
    {
        _direction = new ReactiveProperty<Vector2>();

        Observable.EveryUpdate().Where(x => HasAxist()).Subscribe(x => SetDirection());

        _direction.Where(dir => dir!= Vector2.zero).Subscribe(dir =>
        {
            if (!_actionManager.HasActive(_entity))
            {
                _actionManager.AddAction(_entity, new MoveAction(dir));
            }
        });
    }

    bool HasAxist()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    void SetDirection()
    {
        var xAxis = Input.GetAxis("Horizontal");
        var yAxis = Input.GetAxis("Vertical");

        if (xAxis != 0 || yAxis != 0)
        {
            if (xAxis != 0)
            {
                xAxis = xAxis > 0 ? 1 : -1;
            }

            if (yAxis != 0)
            {
                yAxis = yAxis > 0 ? 1 : -1;
            }

            if (xAxis != 0 && yAxis != 0)
            {
                yAxis = 0;
            }

            var direction = new Vector2(xAxis, yAxis);

            _direction.SetValueAndForceNotify(direction);
        }
    }
}