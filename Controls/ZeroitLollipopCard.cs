// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopCard.cs" company="Zeroit Dev Technologies">
//    This program is for creating Lollipop controls.
//    Copyright ©  2017  Zeroit Dev Technologies
//
//    This program is free software: you can redistribute it and/or modify
//    it under the terms of the GNU General Public License as published by
//    the Free Software Foundation, either version 3 of the License, or
//    (at your option) any later version.
//
//    This program is distributed in the hope that it will be useful,
//    but WITHOUT ANY WARRANTY; without even the implied warranty of
//    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//    GNU General Public License for more details.
//
//    You should have received a copy of the GNU General Public License
//    along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
//    You can contact me at zeroitdevnet@gmail.com or zeroitdev@outlook.com
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{
    /// <summary>
    /// A class collection for Lollipop card.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(LollipopCardDesigner))]
    public class ZeroitLollipopCard : Control
    {
        #region Enumeration

        /// <summary>
        /// Enum for aligning the OK button for the <c><see cref="ZeroitLollipopCard" /></c>.
        /// </summary>
        public enum OKAlignment
        {
            /// <summary>
            /// The left
            /// </summary>
            Left,
            /// <summary>
            /// The center
            /// </summary>
            Center,
            /// <summary>
            /// The right
            /// </summary>
            Right
        }

        /// <summary>
        /// The button alignment
        /// </summary>
        private OKAlignment buttonAlignment = OKAlignment.Right;

        /// <summary>
        /// Gets or sets the ok button alignment.
        /// </summary>
        /// <value>The ok button alignment.</value>
        public OKAlignment OKButtonAlignment
        {
            get { return buttonAlignment; }
            set
            {
                if (value == OKAlignment.Left)
                {
                    OKButton.Location = new Point(Width - Width + 14, InfoLabel.Location.Y + InfoLabel.Height + 26);

                }
                else if (value == OKAlignment.Center)
                {
                    OKButton.Location = new Point(Width/2 - 40, InfoLabel.Location.Y + InfoLabel.Height + 26);

                }
                else
                {
                    OKButton.Location = new Point(Width -75, InfoLabel.Location.Y + InfoLabel.Height + 26);

                }
                buttonAlignment = value;
                Invalidate();
            }
        }

        #endregion

        #region Variables

        /// <summary>
        /// The image
        /// </summary>
        Image image;
        /// <summary>
        /// The information label
        /// </summary>
        Label InfoLabel = new Label();
        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();
        /// <summary>
        /// The ok button
        /// </summary>
        ZeroitLollipopFlatButton OKButton = new ZeroitLollipopFlatButton();

        /// <summary>
        /// The information
        /// </summary>
        string info = "Card Content is here";
        /// <summary>
        /// The t color
        /// </summary>
        string TColor = "#33b679";
        /// <summary>
        /// The c color
        /// </summary>
        string CColor = "#444444";
        /// <summary>
        /// The b color
        /// </summary>
        string BColor = "#33b679";

        /// <summary>
        /// The b color
        /// </summary>
        private Color bColor = Color.FromArgb(51, 182, 121);

        /// <summary>
        /// The empty color
        /// </summary>
        private Color emptyColor = Color.FromArgb(227, 229, 231);
        /// <summary>
        /// The lower rectangle
        /// </summary>
        private Color lowerRectangle = Color.White;
        /// <summary>
        /// The lower border
        /// </summary>
        private Color lowerBorder = Color.FromArgb(218, 220, 223);

        //Content Parameters
        /// <summary>
        /// The c text color
        /// </summary>
        private Color cTextColor = Color.White;
        /// <summary>
        /// The c color
        /// </summary>
        private Color cColor = Color.FromArgb(121, 144, 62);

        //OK Button Color
        /// <summary>
        /// The ok button
        /// </summary>
        private Color okButton = Color.Gray;

        /// <summary>
        /// The border radius
        /// </summary>
        private int borderRadius = 1;

        /// <summary>
        /// The growing
        /// </summary>
        bool Growing;



        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the content text.
        /// </summary>
        /// <value>The content text.</value>
        [Category("Appearance"), Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string ContentText
        {
            get { return info; }
            set
            {
                info = value;

                InfoLabel.Text = info;
                ResizeLabel();

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the button.
        /// </summary>
        /// <value>The color of the button.</value>
        [Category("Appearance")]
        public Color ButtonColor
        {
            get { return bColor; }
            set
            {
                bColor = value;
                OKButton.ForeColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the content.
        /// </summary>
        /// <value>The color of the content.</value>
        [Category("Appearance")]
        public Color ContentColor
        {
            get { return cColor; }
            set
            {
                cColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [Category("Appearance")]
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the empty color.
        /// </summary>
        /// <value>The empty color.</value>
        public Color EmptyColor
        {
            get { return emptyColor; }
            set { emptyColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the lower rectangle.
        /// </summary>
        /// <value>The lower rectangle.</value>
        public Color LowerRectangle
        {
            get { return lowerRectangle; }
            set { lowerRectangle = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the lower border.
        /// </summary>
        /// <value>The lower border.</value>
        public Color LowerBorder
        {
            get { return lowerBorder; }
            set { lowerBorder = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the ok button.
        /// </summary>
        /// <value>The color of the ok button.</value>
        public Color OKButtonColor
        {
            get { return okButton; }
            set
            {
                okButton = value;
                OKButton.BackgroundColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the ok button radius.
        /// </summary>
        /// <value>The ok button radius.</value>
        public int OKButtonRadius
        {
            get { return OKButton.BorderRadius; }
            set
            {
                OKButton.BorderRadius = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the border radius.
        /// </summary>
        /// <value>The border radius.</value>
        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                Invalidate();
            }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            //Width = 294;
            this.Height = InfoLabel.Location.Y + InfoLabel.Height + 74;

            InfoLabel.Width = Width-20;

            if (OKButtonAlignment == OKAlignment.Left)
            {
                OKButton.Location = new Point(Width - Width + 14, InfoLabel.Location.Y + InfoLabel.Height + 26);

            }
            else if (OKButtonAlignment == OKAlignment.Center)
            {
                OKButton.Location = new Point(Width / 2 - 40, InfoLabel.Location.Y + InfoLabel.Height + 26);

            }
            else
            {
                OKButton.Location = new Point(Width - 75, InfoLabel.Location.Y + InfoLabel.Height + 26);

            }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopCard" /> class.
        /// </summary>
        public ZeroitLollipopCard()
        {
            Width = 294;
            Height = 348;
            DoubleBuffered = true;
            ForeColor = Color.FromArgb(51, 182, 121);
            AddLabel();
            AddButton();
            Controls.Add(InfoLabel);
            Controls.Add(OKButton);

        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Adds the label.
        /// </summary>
        private void AddLabel()
        {
            InfoLabel.AutoSize = false;
            InfoLabel.Font = font.Roboto_Medium9;
            InfoLabel.Location = new Point(13, 209);
            //InfoLabel.ForeColor = ColorTranslator.FromHtml(CColor);
            InfoLabel.ForeColor = cColor;

            //InfoLabel.Width = 265;
            InfoLabel.Width = 265 - 20;
            InfoLabel.Text = info;
            //InfoLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ResizeLabel();
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        private void AddButton()
        {
            //OKButton.FontColor = BColor;
            OKButton.ForeColor = bColor;
            OKButton.Location = new Point(105, InfoLabel.Location.Y + InfoLabel.Height + 26);
            OKButton.Size = new Size(69, 33);
            OKButton.Text = "Got it";
            OKButton.BackColor = lowerRectangle;
            OKButton.BackgroundColor = okButton;
            OKButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        }

        /// <summary>
        /// Resizes the label.
        /// </summary>
        private void ResizeLabel()
        {
            if (Growing) return;
            try
            {
                Growing = true;
                Size sz = new Size(InfoLabel.Width, Int32.MaxValue);
                sz = TextRenderer.MeasureText(InfoLabel.Text, InfoLabel.Font, sz, TextFormatFlags.WordBreak);
                InfoLabel.Height = sz.Height;
            }
            finally
            {
                Growing = false;
            }

            if (OKButtonAlignment == OKAlignment.Left)
            {
                OKButton.Location = new Point(Width - Width + 14, InfoLabel.Location.Y + InfoLabel.Height + 26);

            }
            else if (OKButtonAlignment == OKAlignment.Center)
            {
                OKButton.Location = new Point(Width / 2, InfoLabel.Location.Y + InfoLabel.Height + 26);

            }
            else
            {
                OKButton.Location = new Point(Width - 75, InfoLabel.Location.Y + InfoLabel.Height + 26);

            }

            //OKButton.Location = new Point(210, InfoLabel.Location.Y + InfoLabel.Height + 26);

            this.Height = InfoLabel.Location.Y + InfoLabel.Height + 74;
            Refresh();
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);
            //Bitmap B = new Bitmap(Width, Height);
            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            //G.Clear(Parent.BackColor);

            //Content Text Color
            InfoLabel.BackColor = cTextColor;
            InfoLabel.ForeColor = cColor;

            //Content OK
            OKButton.BackColor = lowerRectangle;

            #region Working Code

            //var PicBG = DrawHelper.CreateUpRoundRect(1, 1, 292, 164, 1);
            //var UpRoundedRec = DrawHelper.CreateUpRoundRect(1, 1, 291, 164, 1);
            //var BG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 5, borderRadius);
            //var ShadowBG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 4, borderRadius);

            #endregion

            var PicBG = DrawHelper.CreateUpRoundRect(1, 1, Width, 164, 1);
            var UpRoundedRec = DrawHelper.CreateUpRoundRect(1, 1, Width, 164, 1);
            var BG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 5, borderRadius);
            var ShadowBG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 4, borderRadius);


            G.FillPath(new SolidBrush(emptyColor), ShadowBG);
            G.DrawPath(new Pen(emptyColor), ShadowBG);

            //Lower Rectangle
            G.FillPath(new SolidBrush(lowerRectangle), BG);
            G.DrawPath(new Pen(lowerBorder), BG);

            G.DrawString(Text, font.Roboto_Medium15, new SolidBrush(ForeColor), 12, 176);


            //G.FillRectangle(new SolidBrush(emptyColor), 16, InfoLabel.Location.Y + InfoLabel.Height + 14, 261, 1);
            G.FillRectangle(new SolidBrush(emptyColor), 16, InfoLabel.Location.Y + InfoLabel.Height + 14, Width - 30, 1);
            
            if (image != null)
            {
                G.SetClip(PicBG);
                //G.DrawImage(image, this.Width/2, this.Height/2, image.Width, image.Height);
                G.DrawImage(image, 1, 1, Width - 3, 164);
            }
            else
            {
                G.FillPath(new SolidBrush(emptyColor), UpRoundedRec);
                G.DrawPath(new Pen(emptyColor), UpRoundedRec);
            }

            //e.Graphics.DrawImage(B, 0, 0);
        }

        #endregion

        
        #region Transparency


        #region Include in Paint

        private void TransInPaint(Graphics g)
        {
            if (AllowTransparency)
            {
                MakeTransparent(this, g);
            }
        }

        #endregion

        #region Include in Private Field

        private bool allowTransparency = true;

        #endregion

        #region Include in Public Properties

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


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopCardDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopCardDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        /// <summary>
        /// The action lists
        /// </summary>
        private DesignerActionListCollection actionLists;

        // Use pull model to populate smart tag menu.
        /// <summary>
        /// Gets the design-time action lists supported by the component associated with the designer.
        /// </summary>
        /// <value>The action lists.</value>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (null == actionLists)
                {
                    actionLists = new DesignerActionListCollection();
                    actionLists.Add(new LollipopCardSmartTagActionList(this.Component));
                }
                return actionLists;
            }
        }

        #region Zeroit Filter (Remove Properties)
        /// <summary>
        /// Remove Button and Control properties that are
        /// not supported by the <see cref="MACButton" />.
        /// </summary>
        /// <param name="Properties">The properties.</param>
        protected override void PostFilterProperties(IDictionary Properties)
        {
            //Properties.Remove("AllowDrop");
            //Properties.Remove("FlatStyle");
            //Properties.Remove("ForeColor");
            //Properties.Remove("ImageIndex");
            //Properties.Remove("ImageList");
        }
        #endregion

    }

    #endregion

    #region SmartTagActionList
    /// <summary>
    /// Class LollipopCardSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopCardSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopCard colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopCardSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopCardSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopCard;

            // Cache a reference to DesignerActionUIService, so the 
            // DesigneractionList can be refreshed. 
            this.designerActionUISvc = GetService(typeof(DesignerActionUIService)) as DesignerActionUIService;
        }

        // Helper method to retrieve control properties. Use of GetProperties enables undo and menu updates to work properly.
        /// <summary>
        /// Gets the name of the property by.
        /// </summary>
        /// <param name="propName">Name of the property.</param>
        /// <returns>PropertyDescriptor.</returns>
        /// <exception cref="System.ArgumentException">Matching ColorLabel property not found!</exception>
        private PropertyDescriptor GetPropertyByName(String propName)
        {
            PropertyDescriptor prop;
            prop = TypeDescriptor.GetProperties(colUserControl)[propName];
            if (null == prop)
                throw new ArgumentException("Matching ColorLabel property not found!", propName);
            else
                return prop;
        }

        #region Properties that are targets of DesignerActionPropertyItem entries.

        /// <summary>
        /// Gets or sets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        public Color BackColor
        {
            get
            {
                return colUserControl.BackColor;
            }
            set
            {
                GetPropertyByName("BackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the fore.
        /// </summary>
        /// <value>The color of the fore.</value>
        public Color ForeColor
        {
            get
            {
                return colUserControl.ForeColor;
            }
            set
            {
                GetPropertyByName("ForeColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the button.
        /// </summary>
        /// <value>The color of the button.</value>
        public Color ButtonColor
        {
            get
            {
                return colUserControl.ButtonColor;
            }
            set
            {
                GetPropertyByName("ButtonColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the content.
        /// </summary>
        /// <value>The color of the content.</value>
        public Color ContentColor
        {
            get
            {
                return colUserControl.ContentColor;
            }
            set
            {
                GetPropertyByName("ContentColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the empty color.
        /// </summary>
        /// <value>The empty color.</value>
        public Color EmptyColor
        {
            get
            {
                return colUserControl.EmptyColor;
            }
            set
            {
                GetPropertyByName("EmptyColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the lower rectangle.
        /// </summary>
        /// <value>The lower rectangle.</value>
        public Color LowerRectangle
        {
            get
            {
                return colUserControl.LowerRectangle;
            }
            set
            {
                GetPropertyByName("LowerRectangle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the lower border.
        /// </summary>
        /// <value>The lower border.</value>
        public Color LowerBorder
        {
            get
            {
                return colUserControl.LowerBorder;
            }
            set
            {
                GetPropertyByName("LowerBorder").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the ok button.
        /// </summary>
        /// <value>The color of the ok button.</value>
        public Color OKButtonColor
        {
            get
            {
                return colUserControl.OKButtonColor;
            }
            set
            {
                GetPropertyByName("OKButtonColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        public Image Image
        {
            get
            {
                return colUserControl.Image;
            }
            set
            {
                GetPropertyByName("Image").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the content text.
        /// </summary>
        /// <value>The content text.</value>
        public string ContentText
        {
            get
            {
                return colUserControl.ContentText;
            }
            set
            {
                GetPropertyByName("ContentText").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the ok button radius.
        /// </summary>
        /// <value>The ok button radius.</value>
        public int OKButtonRadius
        {
            get
            {
                return colUserControl.OKButtonRadius;
            }
            set
            {
                GetPropertyByName("OKButtonRadius").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the border radius.
        /// </summary>
        /// <value>The border radius.</value>
        public int BorderRadius
        {
            get
            {
                return colUserControl.BorderRadius;
            }
            set
            {
                GetPropertyByName("BorderRadius").SetValue(colUserControl, value);
            }
        }


        #endregion

        #region DesignerActionItemCollection

        /// <summary>
        /// Returns the collection of <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> objects contained in the list.
        /// </summary>
        /// <returns>A <see cref="T:System.ComponentModel.Design.DesignerActionItem" /> array that contains the items in this list.</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            //Define static section header entries.
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("ButtonColor",
                                 "Button Color", "Appearance",
                                 "Sets the button color."));

            items.Add(new DesignerActionPropertyItem("ContentColor",
                                 "Content Color", "Appearance",
                                 "Sets the content color."));

            items.Add(new DesignerActionPropertyItem("EmptyColor",
                "Empty Color", "Appearance",
                "Sets the empty color."));

            items.Add(new DesignerActionPropertyItem("LowerRectangle",
                "Lower Rectangle", "Appearance",
                "Sets the lower rectangle color."));

            items.Add(new DesignerActionPropertyItem("LowerBorder",
                "Lower Border", "Appearance",
                "Sets the lower border color."));

            items.Add(new DesignerActionPropertyItem("OKButtonColor",
                "OK Button Color", "Appearance",
                "Sets the OK button color."));

            items.Add(new DesignerActionPropertyItem("Image",
                "Image", "Appearance",
                "Sets the image."));

            items.Add(new DesignerActionPropertyItem("ContentText",
                "Content Text", "Appearance",
                "Sets the content text."));

            items.Add(new DesignerActionPropertyItem("OKButtonRadius",
                "OK Button Radius", "Appearance",
                "Sets the OK button radius."));

            items.Add(new DesignerActionPropertyItem("BorderRadius",
                "Border Radius", "Appearance",
                "Sets the border radius."));
            
            //Create entries for static Information section.
            StringBuilder location = new StringBuilder("Product: ");
            location.Append(colUserControl.ProductName);
            StringBuilder size = new StringBuilder("Version: ");
            size.Append(colUserControl.ProductVersion);
            items.Add(new DesignerActionTextItem(location.ToString(),
                             "Information"));
            items.Add(new DesignerActionTextItem(size.ToString(),
                             "Information"));

            return items;
        }

        #endregion




    }

    #endregion

    #endregion


}
