
namespace GameOfLifeVisitor
{
    public class DieCommand : LifeCommand
    {
        public DieCommand(Cell cell) : base(cell)
        {
        }

        public override void Execute()
        {
            CurrentCell.Die();
        }
    }
}
