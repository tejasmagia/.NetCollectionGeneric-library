using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ItemCollection.Common
{
    class Main
    {
    }

    public class BaseTest<T>
    {
        ArrayList _test = new ArrayList();

        public T this[int i]
        {
            get { return (T)_test[i]; }
            set { _test[i] = value; }
        }
    }
}
