using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchWay
{
    interface IAlgorithm
    {
        List<Vertex> GetShortRoute(int first_id, int second_id);
    }
}
