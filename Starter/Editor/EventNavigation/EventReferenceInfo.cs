using System;
using System.Collections.Generic;
using Object = UnityEngine.Object;

namespace Editor
{
    public class EventReferenceInfo
    {
        public Object Owner { get; set; }
        public List<Tuple<Object, string>> Listeners { get; set; } = new List<Tuple<Object, string>>();
    }
}
