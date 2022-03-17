using UnityEngine;

namespace UsefulScripts.UnityScripts
{
    public static class PhysicsS
    {
        public static bool CheckForSolid(Vector3 pos1, Vector3 pos2, LayerMask solidMask, bool toDebugRay = false)
        {
            var direction = pos2 - pos1;
            if (toDebugRay)
                Debug.DrawRay(pos1, new Vector3(direction.x, direction.y, 0), Color.blue);
            return Physics2D.Raycast(pos1, direction, 0.2f, solidMask);
        }
    }
}