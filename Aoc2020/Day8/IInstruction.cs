namespace Day8
{
    public interface IInstruction
    {
        void Execute(ref int inst, ref int acc);
        IInstruction FlipInstruction();
    }

    public class AccInstruction : IInstruction
    {
        private readonly int _amount;
        public AccInstruction(int amount)
        {
            _amount = amount;
        }

        public void Execute(ref int inst, ref int acc)
        {
            acc += _amount;
            inst++;
        }

        public IInstruction FlipInstruction()
        {
            return this;
        }
    }

    public class JmpInstruction : IInstruction
    {
        private readonly int _distance;

        public JmpInstruction(int distance)
        {
            _distance = distance;
        }

        public void Execute(ref int inst, ref int acc)
        {
            inst += _distance;
        }

        public IInstruction FlipInstruction()
        {
            return new NopInstruction(_distance);
        }
    }

    public class NopInstruction : IInstruction
    {
        private readonly int _value;
        public NopInstruction(int value)
        {
            _value = value;
        }

        public void Execute(ref int inst, ref int acc)
        {
            inst++;
        }

        public IInstruction FlipInstruction()
        {
            return new JmpInstruction(_value);
        }
    }
}
