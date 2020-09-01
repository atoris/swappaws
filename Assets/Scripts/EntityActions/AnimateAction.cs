using System.Collections;
using SwaPPawS.Entities;
using UnityEngine;

namespace SwaPPawS.EntityActions
{
    public class AnimateAction : BaseAction
    {
        private string _animation;
        private float _speed;

        public AnimateAction(string animation)
        {
            _animation = animation;
        }

        public override IEnumerator Action()
        {
            var animator = Entity.GetComponent<Animator>();
            if (animator != null)
            {
                animator.SetTrigger(_animation);
            }
            
            yield return null;
        }
    }

}