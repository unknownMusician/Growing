using System;

namespace Growing.PlanetGeneration
{
    public ref struct SpanFiller<T>
    {
        private readonly Span<T> span;
        private int index;

        public SpanFiller(Span<T> span)
        {
            this.span = span;
            index = 0;
        }

        public void Add(T value)
        {
            span[index++] = value;
        }
    }
}