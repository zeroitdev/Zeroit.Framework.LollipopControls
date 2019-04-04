// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopCheckBox.cs" company="Zeroit Dev Technologies">
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
using System.Text;
using System.Windows.Forms;
using Zeroit.Framework.LollipopControls.Helpers;

namespace Zeroit.Framework.LollipopControls.Controls
{
    /// <summary>
    /// A class collection for rendering a Lollipop checkbox.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.CheckBox" />
    [Designer(typeof(LollipopCheckBoxDesigner))]
    public class ZeroitLollipopCheckBox : Control
    {
        #region Variables

        /// <summary>
        /// The checkmark line
        /// </summary>
        static Point[] CHECKMARK_LINE = { new Point(3, 8), new Point(7, 12), new Point(14, 5) };

        //string HexColor = "#508ef5";

        /// <summary>
        /// The enabled checked color
        /// </summary>
        Color EnabledCheckedColor;
        /// <summary>
        /// The enabled un checked color
        /// </summary>
        Color EnabledUnCheckedColor = ColorTranslator.FromHtml("#9c9ea1");

        /// <summary>
        /// The disabled color
        /// </summary>
        Color DisabledColor = ColorTranslator.FromHtml("#c4c6ca");
        /// <summary>
        /// The enabled string color
        /// </summary>
        Color EnabledStringColor = ColorTranslator.FromHtml("#999999");
        /// <summary>
        /// The disabled string color
        /// </summary>
        Color DisabledStringColor = ColorTranslator.FromHtml("#babbbd");

        /// <summary>
        /// The animation timer
        /// </summary>
        Timer AnimationTimer = new Timer { Interval = 17 };

        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();

        /// <summary>
        /// The size animation number
        /// </summary>
        int SizeAnimationNum = 14;
        /// <summary>
        /// The point animation number
        /// </summary>
        int PointAnimationNum = 3;
        /// <summary>
        /// The alpha
        /// </summary>
        int Alpha = 0;
        /// <summary>
        /// The rectangle width
        /// </summary>
        private int rectangleWidth = 1;


        /// <summary>
        /// The unchecked color
        /// </summary>
        private Color uncheckedColor = Color.White;
        /// <summary>
        /// The checked color
        /// </summary>
        private Color checkedColor = Color.FromArgb(80, 142, 245);

        #endregion

        #region  Properties

        private bool @checked = false;

        public bool Checked
        {
            get { return @checked; }
            set
            {
                @checked = value;
                Invalidate();
            }
        }

        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        //[Category("Appearance")]
        //public string CheckColor
        //{
        //    get { return HexColor; }
        //    set
        //    {
        //        HexColor = value;
        //        Invalidate();
        //    }
        //}

        /// <summary>
        /// Gets or sets the color when unchecked.
        /// </summary>
        /// <value>The color when unchecked.</value>
        public Color UncheckedColor
        {
            get { return uncheckedColor; }
            set
            {
                uncheckedColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color when checked.
        /// </summary>
        /// <value>The color when checked.</value>
        public Color CheckedColor
        {
            get { return checkedColor; }
            set
            {
                checkedColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        /// <value>The width of the rectangle.</value>
        public int RectangleWidth
        {
            get { return rectangleWidth; }
            set
            {
                if (value > 3)
                {
                    value = 3;
                    Invalidate();
                }
                rectangleWidth = value;
                Invalidate();
            }
        }

        #endregion

        #region Events
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            if (Checked)
            {
                Checked = false;
            }
            else
            {
                Checked = true;
            }
        }


        /// <summary>
        /// Handles the <see cref="E:HandleCreated" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AnimationTimer.Start();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            Height = 20;
            Width = 20 + (int)CreateGraphics().MeasureString(Text, font.Roboto_Medium10).Width;

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Height = 20;
            Width = 20 + (int)CreateGraphics().MeasureString(Text, font.Roboto_Medium10).Width;

        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopCheckBox" /> class.
        /// </summary>
        public ZeroitLollipopCheckBox()
        {

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.DoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            ForeColor = Color.Black;
            DoubleBuffered = true;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
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




        #region Events and Private Overrides

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            TransInPaint(pevent.Graphics);
            //base.OnPaint(pevent);
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            var checkMarkLine = new Rectangle(1, 1, 16, 16);
            var checkmarkPath = DrawHelper.CreateRoundRect(1, 1, 17, 17, 1);

            //EnabledCheckedColor = ColorTranslator.FromHtml(HexColor);
            SolidBrush BG = new SolidBrush(checkedColor);
            Pen Pen = new Pen(BG.Color)
            {
                Width = rectangleWidth
            };

            g.FillPath(BG, checkmarkPath);
            g.DrawPath(Pen, checkmarkPath);

            g.FillRectangle(new SolidBrush(uncheckedColor), PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum);
            
            g.DrawImageUnscaledAndClipped(CheckMarkBitmap(), checkMarkLine);

            g.DrawString(Text, font.Roboto_Medium10, new SolidBrush(ForeColor), 21, 0);

        }

        /// <summary>
        /// Animations the tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        void AnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (Alpha < 250)
                {
                    Alpha += 25;
                    this.Invalidate();

                    if (SizeAnimationNum > 0)
                    {
                        SizeAnimationNum -= 2;
                        this.Invalidate();
                    }

                    if (PointAnimationNum < 10)
                    {
                        PointAnimationNum += 1;
                        this.Invalidate();
                    }
                }
            }
            else if (Alpha > 0)
            {
                Alpha -= 25;
                this.Invalidate();

                if (SizeAnimationNum < 14)
                {
                    SizeAnimationNum += 2;
                    this.Invalidate();
                }


                if (PointAnimationNum > 3)
                {
                    PointAnimationNum -= 1;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Checks the mark bitmap.
        /// </summary>
        /// <returns>Bitmap.</returns>
        private Bitmap CheckMarkBitmap()
        {
            var checkMark = new Bitmap(16, 16);
            var g = Graphics.FromImage(checkMark);
            g.Clear(Color.Transparent);

            var pen = new Pen(new SolidBrush(Color.FromArgb(Alpha, 255, 255, 255)), 2);
            g.DrawLines(pen, CHECKMARK_LINE);

            return checkMark;
        }

        #endregion

    }

    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopCheckBoxDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopCheckBoxDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopCheckBoxSmartTagActionList(this.Component));
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
    /// Class LollipopCheckBoxSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopCheckBoxSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopCheckBox colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopCheckBoxSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopCheckBoxSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopCheckBox;

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
        /// Gets or sets the color of the unchecked.
        /// </summary>
        /// <value>The color of the unchecked.</value>
        public Color UncheckedColor
        {
            get
            {
                return colUserControl.UncheckedColor;
            }
            set
            {
                GetPropertyByName("UncheckedColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the checked.
        /// </summary>
        /// <value>The color of the checked.</value>
        public Color CheckedColor
        {
            get
            {
                return colUserControl.CheckedColor;
            }
            set
            {
                GetPropertyByName("CheckedColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the width of the rectangle.
        /// </summary>
        /// <value>The width of the rectangle.</value>
        public int RectangleWidth
        {
            get
            {
                return colUserControl.RectangleWidth;
            }
            set
            {
                GetPropertyByName("RectangleWidth").SetValue(colUserControl, value);
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

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LollipopCheckBoxSmartTagActionList"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public new bool Checked
        {
            get
            {
                return colUserControl.Checked;
            }
            set
            {
                GetPropertyByName("Checked").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("Checked",
                                "Checked", "Appearance",
                                "Sets the autocheck."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("UncheckedColor",
                                 "Unchecked Color", "Appearance",
                                 "Sets the unchecked color."));

            items.Add(new DesignerActionPropertyItem("CheckedColor",
                                 "Checked Color", "Appearance",
                                 "Sets the checked color."));


            items.Add(new DesignerActionPropertyItem("RectangleWidth",
                "Rectangle Width", "Appearance",
                "Sets the rectangle width."));

            items.Add(new DesignerActionPropertyItem("Text",
                "Text", "Appearance",
                "Sets the text."));

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
