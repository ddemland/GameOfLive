
namespace GameOfLifeCommand
{
    public abstract class LifeCommand
    {
        protected Cell CurrentCell { set; get; }

        public abstract void Execute();

        protected LifeCommand(Cell cell)
        {
            CurrentCell = cell;
        }
    }
}
