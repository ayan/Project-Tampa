
using Tampa.UI.Models;
namespace Tampa.Interfaces
{
    public interface IPaletteController: ITampaChildWindowController
    {
        PaletteModel GetModel();
        void RequestAddControl(IControl control);
    }
}
