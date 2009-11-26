using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tampa.Controls;
using Tampa.Interfaces;
using System.Drawing;

namespace Tampa.UI.Models
{
    /// <summary>
    /// Model for the Palette
    /// </summary>
    public class PaletteModel
    {
        public PaletteModel(IPaletteController controller)
        {
            this.PaletteButtons = new List<PaletteButton>();
            _controller = controller;
        }

        /// <summary>
        /// Initialiazes the palette model from the Control Manager
        /// </summary>
        /// <param name="cm"></param>
        public void InitializeFromControlManager(ControlManager cm)        
        {
            foreach (IControl control in cm.Controls)
            {
                AddPaletteButtonFromControl(control); 
            }

            UpdatePaletteBounds();
        }

        /// <summary>
        /// Adds a palette button to the list of palette buttons from a given IControl
        /// </summary>
        /// <param name="control"></param>
        private void AddPaletteButtonFromControl(IControl control)
        {
            PaletteButton button = new PaletteButton(c_buttonWidth, c_buttonHeight, control, _controller);
            this.PaletteButtons.Add(button);
        }

        /// <summary>
        /// Updates the bounds of the palette
        /// </summary>
        private void UpdatePaletteBounds()
        {
            int paletteWidth = (2 * c_buttonWidth) + c_buttonSeperatorPadding;
            int paletteHeight = (this.PaletteButtons.Count * c_buttonHeight) / 2;

            if (paletteHeight < c_minPaletteHeight) paletteHeight = c_minPaletteHeight;

            this.PaletteBounds = new Rectangle(0, 0, paletteWidth, paletteHeight);
        }

        public Rectangle PaletteBounds { get; private set; }
        public List<PaletteButton> PaletteButtons { get; private set; }

        private IPaletteController _controller;

        private const int c_minPaletteHeight = 480;
        private const int c_buttonWidth = 48;
        private const int c_buttonHeight = 48;
        private const int c_buttonSeperatorPadding = 10;        
    }
}
