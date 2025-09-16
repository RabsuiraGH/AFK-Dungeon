using UnityEngine;

namespace LA
{
    public class RandomService : IRandomService
    {
        public int Range(int minInclusive, int maxExclusive)
        {
            return UnityEngine.Random.Range(minInclusive, maxExclusive);
        }
    }
}