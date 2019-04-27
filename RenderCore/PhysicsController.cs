using System.Collections.Generic;

namespace RenderCore
{
    public class PhysicsController : List<IPhysicalObject>
    {
        public void ResolvePhysics()
        {
            List<IPhysicalObject> objects = new List<IPhysicalObject>(this);
            foreach (IPhysicalObject physicalObject in objects)
            {
                IForce force = physicalObject.CombineAndDequeueForces();
                if (force is NormalForce normalForce)
                {
                    physicalObject.Move(normalForce.ForceVector);
                }
            }
        }
    }
}