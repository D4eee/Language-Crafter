using System.Collections.Generic;

namespace Yiyang.Narration
{
    [System.Serializable]
    public sealed class NarrationSequence
    {
        public List<NarrationLine> lines = new();
    }
}
