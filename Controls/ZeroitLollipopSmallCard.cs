// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopSmallCard.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{

    /// <summary>
    /// A class collection for rendering a Lollipop small card.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [Designer(typeof(LollipopSmallCardDesigner))]
    public class ZeroitLollipopSmallCard : Control
    {

        #region Variables

        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();
        /// <summary>
        /// The image
        /// </summary>
        Image image;

        /// <summary>
        /// The information
        /// </summary>
        string information = "Info";
        /// <summary>
        /// The fontcolor
        /// </summary>
        Color fontcolor = ColorTranslator.FromHtml("#33b679");
        /// <summary>
        /// The thumbnailcolor
        /// </summary>
        Color thumbnailcolor = ColorTranslator.FromHtml("#33b679");

        /// <summary>
        /// The bg color
        /// </summary>
        Color BgColor;
        /// <summary>
        /// The string color
        /// </summary>
        Color StringColor;
        /// <summary>
        /// The thumbnail bg color
        /// </summary>
        Color ThumbnailBGColor;
        /// <summary>
        /// The border color
        /// </summary>
        Color BorderColor = ColorTranslator.FromHtml("#dbdbdb");

        #endregion

        #region Properties

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
        /// Gets or sets the color of the thumbnail.
        /// </summary>
        /// <value>The color of the thumbnail.</value>
        [Category("Appearance")]
        public Color ThumbnailColor
        {
            get { return thumbnailcolor; }
            set
            {
                thumbnailcolor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the title.
        /// </summary>
        /// <value>The color of the title.</value>
        [Category("Appearance")]
        public Color TitleColor
        {
            get { return fontcolor; }
            set
            {
                fontcolor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>The information.</value>
        [Category("Appearance")]
        public string Info
        {
            get { return information; }
            set
            {
                information = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(true)]
        public Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [Browsable(true)]
        public Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseEnter" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            BgColor = ColorTranslator.FromHtml("#fafafb");
            Refresh();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseLeave" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            BgColor = Color.White;
            Refresh();
        }

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
            Height = 52;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopSmallCard" /> class.
        /// </summary>
        public ZeroitLollipopSmallCard()
        {
            Height = 52; Width = 182; DoubleBuffered = true;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            TransInPaint(e.Graphics);
            base.OnPaint(e);

            Graphics G = e.Graphics;
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.Clear(BackColor);

            StringColor = fontcolor;
            ThumbnailBGColor = thumbnailcolor;

            var BG = DrawHelper.CreateRoundRect(1, 1, Width - 3, Height - 3, 10);
            var ThumbnailBG = DrawHelper.CreateLeftRoundRect(1, 1, 50, 49, 1);

            G.FillPath(new SolidBrush(BgColor), BG);
            G.DrawPath(new Pen(BorderColor), BG);

            G.FillPath(new SolidBrush(ThumbnailBGColor), ThumbnailBG);
            G.DrawPath(new Pen(ThumbnailBGColor), ThumbnailBG);

            if (image != null)
            { G.DrawImage(image, 3, 3, 48, 47); }
            if (Enabled)
            { G.DrawString(Text, font.Roboto_Medium10, new SolidBrush(StringColor), new PointF(58.6f, 9f)); }
            else
            { G.DrawString("Wait...", font.Roboto_Medium10, new SolidBrush(StringColor), new PointF(58.6f, 9f)); }

            G.TextRenderingHint = TextRenderingHint.AntiAlias;
            G.DrawString(information, font.Roboto_Regular9, new SolidBrush(ForeColor), new PointF(59.1f, 26f));
        }




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
    /// Class LollipopSmallCardDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopSmallCardDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopSmallCardSmartTagActionList(this.Component));
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
    /// Class LollipopSmallCardSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopSmallCardSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopSmallCard colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopSmallCardSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopSmallCardSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopSmallCard;

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
        /// Gets or sets the image.
        /// </summary>
        /// <value>The image.</value>
        [Category("Appearance")]
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
        /// Gets or sets the color of the thumbnail.
        /// </summary>
        /// <value>The color of the thumbnail.</value>
        [Category("Appearance")]
        public Color ThumbnailColor
        {
            get
            {
                return colUserControl.ThumbnailColor;
            }
            set
            {
                GetPropertyByName("ThumbnailColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the title.
        /// </summary>
        /// <value>The color of the title.</value>
        [Category("Appearance")]
        public Color TitleColor
        {
            get
            {
                return colUserControl.TitleColor;
            }
            set
            {
                GetPropertyByName("TitleColor").SetValue(colUserControl, value);
            }
        }
        /// <summary>
        /// Gets or sets the information.
        /// </summary>
        /// <value>The information.</value>
        [Category("Appearance")]
        public string Info
        {
            get
            {
                return colUserControl.Info;
            }
            set
            {
                GetPropertyByName("Info").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get
            {
                return colUserControl.Text;
            }
            set
            {
                GetPropertyByName("Text").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("ThumbnailColor",
                                 "Thumbnail Color", "Appearance",
                                 "Sets the thumbnail color."));

            items.Add(new DesignerActionPropertyItem("TitleColor",
                                 "Title Color", "Appearance",
                                 "Sets the title color."));

            items.Add(new DesignerActionPropertyItem("Info",
                "Info", "Appearance",
                "Sets the info text."));

            items.Add(new DesignerActionPropertyItem("Text",
                "Text", "Appearance",
                "Sets the Text."));

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
