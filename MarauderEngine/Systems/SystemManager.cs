using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEngine.Systems
{
    /// <summary>
    /// Base for all Manager-Type classes. Mandates a static, self-referencing Instance property.
    /// </summary>
    public abstract class SystemManager<T> where T : class
    {
        /// <summary>
        /// Static reference to the active instance of the manager.
        /// </summary>
        public static T Instance;

        protected SystemManager()
        {
            SetInstanceReference();
            Initialize();
        }

        private void SetInstanceReference()
        {
            Instance = this as T;
        }

        /// <summary>
        /// Perform any post-construction tasks. Called in constructor.
        /// </summary>
        public abstract void Initialize();


    }
}
