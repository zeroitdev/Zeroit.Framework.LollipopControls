// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopLabel.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Drawing;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{

    /// <summary>
    /// A class collection for rendering a Lollipop label.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Label" />
    public class ZeroitLollipopLabel : Label
    {
        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        public override string Text
        {
            get => base.Text;
            set
            {
                base.Text = value;
                Invalidate();
            }

        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        public override Color ForeColor
        {
            get => base.ForeColor;
            set { base.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopLabel" /> class.
        /// </summary>
        public ZeroitLollipopLabel()
        {
            this.Font = font.Roboto_Medium10;
            ForeColor = ColorTranslator.FromHtml("#999999");
            //BackColor = Color.Transparent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);
        }




        #region Transparency


        #region Include in Paint

        /// <summary>
        /// Transes the in paint.
        /// </summary>
        /// <param name="g">The g.</param>
        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        /// <summary>
        /// The allow transparency
        /// </summary>
        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether [allow transparency].
        /// </summary>
        /// <value><c>true</c> if [allow transparency]; otherwise, <c>false</c>.</value>
        public bool AllowTransparency
        {
            get { return allowTransparency; }
            set
            {
                allowTransparency = value;

                Invalidate();
            }
        }

        #endregion

        #region Method

        //-----------------------------Include in Paint--------------------------//
        //
        // if(AllowTransparency)
        //  {
        //    MakeTransparent(this,g);
        //  }
        //
        //-----------------------------Include in Paint--------------------------//

        /// <summary>
        /// Makes the transparent.
        /// </summary>
        /// <param name="control">The control.</param>
        /// <param name="g">The g.</param>
        private static void MakeTransparent(Control control, Graphics g)
        {
            var parent = control.Parent;
            if (parent == null) return;
            var bounds = control.Bounds;
            var siblings = parent.Controls;
            int index = siblings.IndexOf(control);
            Bitmap behind = null;
            for (int i = siblings.Count - 1; i > index; i--)
            {
                var c = siblings[i];
                if (!c.Bounds.IntersectsWith(bounds)) continue;
                if (behind == null)
                    behind = new Bitmap(control.Parent.ClientSize.Width, control.Parent.ClientSize.Height);
                c.DrawToBitmap(behind, c.Bounds);
            }
            if (behind == null) return;
            g.DrawImage(behind, control.ClientRectangle, bounds, GraphicsUnit.Pixel);
            behind.Dispose();
        }

        #endregion


        #endregion




    }

}