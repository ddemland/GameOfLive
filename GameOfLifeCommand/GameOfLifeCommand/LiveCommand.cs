
namespace GameOfLifeCommand
{
    public class LiveCommand : LifeCommand
    {
        public LiveCommand(Cell cell) : base(cell)
        {
        }

        public override void Execute()
        {
            CurrentCell.Live();
        }
    }
}
