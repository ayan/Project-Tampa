using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tampa.Interfaces;
using System.Windows.Forms;

namespace Tampa.UI.Controllers
{
    public class PropertyDialogController: IPropertyDialogController
    {
        #region IPropertyDialogController Members

        public PropertyDialogController()
        {
            _view = new PropertiesDialog(this as IPropertyDialogController);
        }

        public void ShowControlProperties(IWin32Window tampaWindow, ControlInstance instance)
        {
            _view.SetControl(instance);
            Show(tampaWindow);

        }

        public void UpdateProperties(ControlInstance instance)
        {
            instance.Slurp();
            _view.Hide();
        }

        public void RevertProperties(ControlInstance instance)
        {
            instance.Update();
            _view.Hide();
        }

        #endregion

        #region ITampaChildWindowController Members

        public void Show(System.Windows.Forms.IWin32Window tampaWindow)
        {
            _view.Show(tampaWindow);
        }

        #endregion

        PropertiesDialog _view;
    }
}
