using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Other
{
    public interface IThreadService : IDisposable
    {
        bool RunMethodWithThreadPool(int value);

        bool RunMethodWithThreadParallel(List<int> list);
    }
}
