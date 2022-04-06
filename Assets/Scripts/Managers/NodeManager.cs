using System.Collections;
using System.Collections.Generic;
using Networks.Road;
using Utils;

namespace Managers
{
    public class NodeManager: IdList<Node>
    {
        private static NodeManager singleton;
        public static NodeManager Instance
        {
            get { return singleton ??= new NodeManager(); }
        }
    }
}