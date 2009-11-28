
namespace Tampa.Interfaces
{
    public interface ICanvasController: ITampaChildWindowController
    {
        /// <summary>
        /// Adds a control to the canvas
        /// </summary>
        /// <param name="controlTypeToAdd"></param>
        void AddControl(IControl controlTypeToAdd);
    }
}
