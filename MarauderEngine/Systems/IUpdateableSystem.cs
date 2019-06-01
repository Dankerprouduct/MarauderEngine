using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEngine.Systems
{
    /// <summary>
    /// Provides an Update method outside of the constrains of the XNA framework.
    /// Intended to be called as needed within core game loop.
    /// </summary>
    public interface IUpdateableSystem
    {
        /// <summary>
        /// 
        /// </summary>
        void Update();

    }
}
