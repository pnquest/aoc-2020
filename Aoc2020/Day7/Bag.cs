using System;
using System.Collections.Generic;

namespace Day7
{
    public class Bag
    {
        public string Color { get; set; }

        public Dictionary<Bag, int> Contents { get; } = new Dictionary<Bag, int>();
        public List<Bag> ContainedBy { get; } = new List<Bag>();

        public Bag(string color)
        {
            Color = color;
        }

        public IEnumerable<Bag> EnumerateParents()
        {
            foreach(Bag p in ContainedBy)
            {
                yield return p;
                foreach(Bag p2 in p.EnumerateParents())
                {
                    yield return p2;
                }
            }
        }

        public IEnumerable<(int, Bag, int)> EnumerateChildren(int multiplier = 1)
        {
            foreach(KeyValuePair<Bag, int> c in Contents)
            {
                yield return (multiplier, c.Key, c.Value);
                foreach((int, Bag, int) c2 in c.Key.EnumerateChildren(c.Value * multiplier))
                {
                    yield return c2;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Bag bag &&
                   Color == bag.Color;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color);
        }
    }
}
