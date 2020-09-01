using SwaPPawS.EntityActions;
using UnityEngine;

namespace SwaPPawS.Entities
{
    public class Entity : MonoBehaviour
    {
        public ActionManager ActionManager;

        private void Start()
        {
            ActionManager = GameObject.FindObjectOfType<ActionManager>();
            ActionManager.AddEntity(this);

            // ActionManager.Add(this, new MoveAction(Vector3.one));
            // ActionManager.Add(this, new MoveAction(Vector3.one));
            // ActionManager.Add(this, new MoveAction(Vector3.one));
            // ActionManager.Add(this, new MoveAction(Vector3.one));
        }

        public void AddAction(BaseAction action, bool forced = false)
        {
            ActionManager.AddAction(this, action, forced);
        }
    }
}