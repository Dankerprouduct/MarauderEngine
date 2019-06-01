using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarauderEngine.Core
{
    /// <summary>
    /// Not fully implemented but feel free to use
    /// </summary>
    public class Coroutines
    {
        private List<IEnumerator> _routines = new List<IEnumerator>();

        public Coroutines()
        {

        }

        public void Start(IEnumerator routine)
        {
            _routines.Add(routine);
        }

        public void StopAll()
        {
            _routines.Clear();
        }

        public void Update()
        {
            for (int i = 0; i < _routines.Count; i++)
            {
                if (_routines[i].Current is IEnumerator)
                {

                    if (MoveNext((IEnumerator) _routines[i].Current))
                    {
                        continue;
                    }

                    if (!_routines[i].MoveNext())
                    {
                        _routines.RemoveAt(i--);
                    }

                }
            }
        }

        bool MoveNext(IEnumerator routine)
        {
            if (routine.Current is IEnumerator)
            {
                if (MoveNext((IEnumerator)routine.Current))
                {
                    return true;
                }
            }

            return routine.MoveNext();
        }
        

        public int Count => _routines.Count;

        public bool Running => _routines.Count > 0;

    }
}
