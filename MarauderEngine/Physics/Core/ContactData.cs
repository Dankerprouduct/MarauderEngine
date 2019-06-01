using Microsoft.Xna.Framework;

namespace MarauderEngine.Physics.Core
{
    /// <summary>
    /// A contact represents two bodies in contact
    /// </summary>
    public class ContactData
    {
        
        /// <summary>
        /// Holds the position of the contact in world coordinates
        /// </summary>
        public Vector2 ContactPoint;

        /// <summary>
        /// HOlds the direction of the contact in world coordinates 
        /// </summary>
        public Vector2 ContactNormal;

        /// <summary>
        /// HOlds the depth of penetration at the contact point
        /// </summary>
        public float Penetration; 
    }
}
