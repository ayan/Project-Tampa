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
            if (isViewShown)
            {
                _view.Hide();
                isViewShown = false;
            }
        }

        public void RevertProperties(ControlInstance instance)
        {
            instance.Update();

            if (isViewShown)
            {
                _view.Hide();
                isViewShown = false;
            }
        }

        #endregion

        #region ITampaChildWindowController Members

        public void Show(System.Windows.Forms.IWin32Window tampaWindow)
        {
            if (!isViewShown)
            {
                _view.Show(tampaWindow);
                isViewShown = true;
            }
        }

        #endregion

        bool isViewShown = false;
        PropertiesDialog _view;
    }
}
