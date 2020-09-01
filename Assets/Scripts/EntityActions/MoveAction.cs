using System.Collections;
using System.Linq;
using SwaPPawS.Components;
using SwaPPawS.Entities;
using UnityEngine;

namespace SwaPPawS.EntityActions
{
    public class MoveAction : BaseAction
    {
        private Vector3 _direction;
        private float _speed;

        public MoveAction(Vector3 direction)
        {
            _direction = direction;            
        }

        private void Reset(){
            _speed = Entity.GetComponent<Move>().Speed;
        }

        public override IEnumerator Action()
        {

            Entity.AddAction(new AnimateAction("run"), true);

            var moveComponent = Entity.GetComponent<Move>();
            _speed = moveComponent.Speed;

            var transform = Entity.transform;
            var stepPosition = transform.position + _direction;

            var entity = Entity.ActionManager.Entities.FirstOrDefault(x => x.transform.position == stepPosition);

            if (entity != null)
            {
                var moveEntity = entity.GetComponent<Move>();
                if (moveEntity)
                {
                    _speed = Mathf.Min(moveEntity.Speed, _speed);
                    entity.AddAction(new MoveAction(_direction), true);
                }
                else
                {
                    yield break;
                }

            }

            while (!stepPosition.Equals(transform.position))
            {
                transform.position = Vector3.MoveTowards(transform.position, stepPosition, Time.deltaTime * _speed);
                yield return null;
            }

            Entity.transform.position = stepPosition;

            Entity.AddAction(new AnimateAction("idle"), true);

            Reset();
        }
    }

}