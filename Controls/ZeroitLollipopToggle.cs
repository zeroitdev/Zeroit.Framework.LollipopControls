// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopToggle.cs" company="Zeroit Dev Technologies">
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
    /// A class collection for rendering Lollipop toggle control.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.CheckBox" />
    [Designer(typeof(LollipopToggleDesigner))]
    public class ZeroitLollipopToggle : Control
    {

        #region Enumeration

        /// <summary>
        /// Enum for setting the rendering type for <c><see cref="ZeroitLollipopToggle" /></c>
        /// </summary>
        public enum DrawType
        {
            /// <summary>
            /// The circle
            /// </summary>
            Circle,
            /// <summary>
            /// The rectangle
            /// </summary>
            Rectangle,
            /// <summary>
            /// The polygon
            /// </summary>
            Polygon
        }

        /// <summary>
        /// The draw type
        /// </summary>
        private DrawType _drawType = DrawType.Circle;

        #endregion

        #region Variables
        /// <summary>
        /// The checked
        /// </summary>
        private bool @checked = false;
        /// <summary>
        /// The animation timer
        /// </summary>
        Timer AnimationTimer = new Timer { Interval = 1 };
        /// <summary>
        /// The rounded rectangle
        /// </summary>
        GraphicsPath RoundedRectangle;

        /// <summary>
        /// The ellipse bg
        /// </summary>
        Color EllipseBG = ColorTranslator.FromHtml("#508ef5");
        /// <summary>
        /// The ellipse border
        /// </summary>
        Color EllipseBorder = ColorTranslator.FromHtml("#3b73d1");

        /// <summary>
        /// The ellipse back color
        /// </summary>
        Color EllipseBackColor;
        /// <summary>
        /// The ellipse border back color
        /// </summary>
        Color EllipseBorderBackColor;

        /// <summary>
        /// The enabled un checked color
        /// </summary>
        Color enabledUnCheckedColor = ColorTranslator.FromHtml("#bcbfc4");
        /// <summary>
        /// The enabled un checked ellipse border color
        /// </summary>
        Color enabledUnCheckedEllipseBorderColor = ColorTranslator.FromHtml("#a9acb0");

        /// <summary>
        /// The disabled ellipse back color
        /// </summary>
        Color disabledEllipseBackColor = ColorTranslator.FromHtml("#c3c4c6");
        /// <summary>
        /// The disabled ellipse border back color
        /// </summary>
        Color disabledEllipseBorderBackColor = ColorTranslator.FromHtml("#90949a");

        /// <summary>
        /// The circle inactive color
        /// </summary>
        private Color circleInactiveColor = Color.White;

        /// <summary>
        /// The point animation number
        /// </summary>
        int PointAnimationNum = 4;

        /// <summary>
        /// The sides
        /// </summary>
        private int sides = 3;
        /// <summary>
        /// The radius
        /// </summary>
        private int radius = 9;
        /// <summary>
        /// The starting angle
        /// </summary>
        private int startingAngle = 90;
        /// <summary>
        /// The center width
        /// </summary>
        private int centerWidth = 18;
        /// <summary>
        /// The center
        /// </summary>
        Point center;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopToggle"/> is checked.
        /// </summary>
        /// <value><c>true</c> if checked; otherwise, <c>false</c>.</value>
        public bool Checked
        {
            get { return @checked; }
            set
            {
                @checked = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the polygon sides.
        /// </summary>
        /// <value>The polygon sides.</value>
        [Category("Appearance")]
        public int PolySides
        {
            get { return sides; }
            set
            {
                if (value < 3)
                {
                    value = 3;
                    Invalidate();
                }
                sides = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the polygon starting angle.
        /// </summary>
        /// <value>The polygon starting angle.</value>
        [Category("Appearance")]
        public int StartingAngle
        {
            get { return startingAngle; }
            set
            {
                startingAngle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the shape to render.
        /// </summary>
        /// <value>The shape to render.</value>
        [Category("Appearance")]
        public DrawType RenderType
        {
            get { return _drawType; }
            set
            {
                _drawType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the ellipse.
        /// </summary>
        /// <value>The color of the ellipse.</value>
        [Category("Appearance")]
        public Color EllipseColor
        {
            get { return EllipseBG; }
            set
            {
                EllipseBG = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the ellipse border.
        /// </summary>
        /// <value>The color of the ellipse border.</value>
        [Category("Appearance")]
        public Color EllipseBorderColor
        {
            get { return EllipseBorder; }
            set
            {
                EllipseBorder = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color of the inactive.
        /// </summary>
        /// <value>The color of the inactive.</value>
        public Color InactiveColor
        {
            get { return circleInactiveColor; }
            set
            {
                circleInactiveColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the unchecked color if the control is enabled.
        /// </summary>
        /// <value>The unchecked color if the control is enabled.</value>
        public Color EnabledUnCheckedColor
        {
            get { return enabledUnCheckedColor; }
            set { enabledUnCheckedColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the border if the control is unchecked.
        /// </summary>
        /// <value>The color of the border if the control is unchecked.</value>
        public Color UnCheckedBorderColor
        {
            get { return enabledUnCheckedEllipseBorderColor; }
            set { enabledUnCheckedEllipseBorderColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color of the disabled background.
        /// </summary>
        /// <value>The color of the disabled background.</value>
        public Color DisabledBackColor
        {
            get { return disabledEllipseBackColor; }
            set { disabledEllipseBackColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the border color of the disabled control's background.
        /// </summary>
        /// <value>The color of the disabled border.</value>
        public Color DisabledBorderBackColor
        {
            get { return disabledEllipseBorderBackColor; }
            set { disabledEllipseBorderBackColor = value; Invalidate(); }
        }

        #endregion

        #region Events

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
            Height = 19; Width = 47;

            RoundedRectangle = new GraphicsPath();
            int radius = 10;

            RoundedRectangle.AddArc(11, 4, radius - 1, radius, 180, 90);
            RoundedRectangle.AddArc(Width - 21, 4, radius - 1, radius, -90, 90);
            RoundedRectangle.AddArc(Width - 21, Height - 15, radius - 1, radius, 0, 90);
            RoundedRectangle.AddArc(11, Height - 15, radius - 1, radius, 90, 90);

            RoundedRectangle.CloseAllFigures();
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.MouseDown" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data.</param>
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

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopToggle" /> class.
        /// </summary>
        public ZeroitLollipopToggle()
        {
            Height = 19; Width = 47;
            DoubleBuffered = true;
            AnimationTimer.Tick += new EventHandler(AnimationTick);
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            TransInPaint(pevent.Graphics);
            var G = pevent.Graphics;
            G.SmoothingMode = SmoothingMode.AntiAlias;
            //G.Clear(Parent.BackColor);

            center = new Point(centerWidth / 2, 20 / 2);
           
            Point[] PolyGon1 = DrawHelper.CalculateVertices(sides, radius, startingAngle, center);

            EllipseBackColor = EllipseBG;
            EllipseBorderBackColor = EllipseBorder;

            

            G.FillPath(new SolidBrush(Color.FromArgb(115, Enabled ? Checked ? EllipseBackColor : enabledUnCheckedColor : enabledUnCheckedColor)), RoundedRectangle);
            G.DrawPath(new Pen(Color.FromArgb(50, Enabled ? Checked ? EllipseBackColor : enabledUnCheckedColor : enabledUnCheckedColor)), RoundedRectangle);

            switch (_drawType)
            {
                case DrawType.Circle:
                    G.FillEllipse(new SolidBrush(Enabled ? Checked ? EllipseBackColor : circleInactiveColor : disabledEllipseBackColor), PointAnimationNum, 0, 18, 18);
                    G.DrawEllipse(new Pen(Enabled ? Checked ? EllipseBorderBackColor : enabledUnCheckedEllipseBorderColor : disabledEllipseBorderBackColor), PointAnimationNum, 0, 18, 18);

                    break;
                case DrawType.Rectangle:
                    G.FillRectangle(new SolidBrush(Enabled ? Checked ? EllipseBackColor : circleInactiveColor : disabledEllipseBackColor), PointAnimationNum, 0 +2, 14, 14);
                    G.DrawRectangle(new Pen(Enabled ? Checked ? EllipseBorderBackColor : enabledUnCheckedEllipseBorderColor : disabledEllipseBorderBackColor), PointAnimationNum, 0 +2, 14, 14);

                    break;
                case DrawType.Polygon:
                    G.FillPolygon(new SolidBrush(Enabled ? Checked ? EllipseBackColor : circleInactiveColor : disabledEllipseBackColor), PolyGon1);
                    G.DrawPolygon(new Pen(Enabled ? Checked ? EllipseBorderBackColor : enabledUnCheckedEllipseBorderColor : disabledEllipseBorderBackColor), PolyGon1);

                    break;
                
            }
            
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
                if (PointAnimationNum < 24)
                {
                    PointAnimationNum += 1;
                    this.Invalidate();
                }
            }
            else if (PointAnimationNum > 4)
            {
                PointAnimationNum -= 1;
                this.Invalidate();
            }

            if (Checked)
            {
                if (centerWidth < 64)
                {
                    centerWidth += 1;
                    this.Invalidate();
                }
            }
            else if (centerWidth > 24)
            {
                centerWidth -= 1;
                this.Invalidate();
            }
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
    /// Class LollipopToggleDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopToggleDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopToggleSmartTagActionList(this.Component));
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
    /// Class LollipopToggleSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopToggleSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopToggle colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopToggleSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopToggleSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopToggle;

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
        /// Gets or sets the poly sides.
        /// </summary>
        /// <value>The poly sides.</value>
        [Category("Appearance")]
        public int PolySides
        {
            get
            {
                return colUserControl.PolySides;
            }
            set
            {
                GetPropertyByName("PolySides").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the starting angle.
        /// </summary>
        /// <value>The starting angle.</value>
        [Category("Appearance")]
        public int StartingAngle
        {
            get
            {
                return colUserControl.StartingAngle;
            }
            set
            {
                GetPropertyByName("StartingAngle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the type of the render.
        /// </summary>
        /// <value>The type of the render.</value>
        [Category("Appearance")]
        public ZeroitLollipopToggle.DrawType RenderType
        {
            get
            {
                return colUserControl.RenderType;
            }
            set
            {
                GetPropertyByName("RenderType").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the ellipse.
        /// </summary>
        /// <value>The color of the ellipse.</value>
        [Category("Appearance")]
        public Color EllipseColor
        {
            get
            {
                return colUserControl.EllipseColor;
            }
            set
            {
                GetPropertyByName("EllipseColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the ellipse border.
        /// </summary>
        /// <value>The color of the ellipse border.</value>
        [Category("Appearance")]
        public Color EllipseBorderColor
        {
            get
            {
                return colUserControl.EllipseBorderColor;
            }
            set
            {
                GetPropertyByName("EllipseBorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the inactive.
        /// </summary>
        /// <value>The color of the inactive.</value>
        public Color InactiveColor
        {
            get
            {
                return colUserControl.InactiveColor;
            }
            set
            {
                GetPropertyByName("InactiveColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LollipopToggleSmartTagActionList"/> is checked.
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

        /// <summary>
        /// Gets or sets the color of the enabled un checked.
        /// </summary>
        /// <value>The color of the enabled un checked.</value>
        public Color EnabledUnCheckedColor
        {
            get
            {
                return colUserControl.EnabledUnCheckedColor;
            }
            set
            {
                GetPropertyByName("EnabledUnCheckedColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the un checked border.
        /// </summary>
        /// <value>The color of the un checked border.</value>
        public Color UnCheckedBorderColor
        {
            get
            {
                return colUserControl.UnCheckedBorderColor;
            }
            set
            {
                GetPropertyByName("UnCheckedBorderColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled back.
        /// </summary>
        /// <value>The color of the disabled back.</value>
        public Color DisabledBackColor
        {
            get
            {
                return colUserControl.DisabledBackColor;
            }
            set
            {
                GetPropertyByName("DisabledBackColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the disabled border back.
        /// </summary>
        /// <value>The color of the disabled border back.</value>
        public Color DisabledBorderBackColor
        {
            get
            {
                return colUserControl.DisabledBorderBackColor;
            }
            set
            {
                GetPropertyByName("DisabledBorderBackColor").SetValue(colUserControl, value);
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
                "Sets the control to automatically check."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("PolySides",
                                 "Polygon Sides", "Appearance",
                                 "Sets the polygon sides."));

            items.Add(new DesignerActionPropertyItem("StartingAngle",
                                 "Polygon Angle", "Appearance",
                                 "Sets the polygon angle."));

            items.Add(new DesignerActionPropertyItem("RenderType",
                "Render Type", "Appearance",
                "Sets the draw type."));

            items.Add(new DesignerActionPropertyItem("EllipseColor",
                "Color", "Appearance",
                "Sets the Circle color."));

            items.Add(new DesignerActionPropertyItem("EllipseBorderColor",
                "Checked Border Color", "Appearance",
                "Sets the border color."));

            items.Add(new DesignerActionPropertyItem("InactiveColor",
                "Inactive Color", "Appearance",
                "Sets the inactive color."));

            items.Add(new DesignerActionPropertyItem("EnabledUnCheckedColor",
                "UnChecked Color", "Appearance",
                "Sets the inactive unchecked color."));

            items.Add(new DesignerActionPropertyItem("UnCheckedBorderColor",
                "UnChecked Border Color", "Appearance",
                "Sets the inactive unchecked border color."));

            items.Add(new DesignerActionPropertyItem("DisabledBackColor",
                "Disabled BackColor", "Appearance",
                "Sets the disabled backcolor."));

            items.Add(new DesignerActionPropertyItem("DisabledBorderBackColor",
                "Disabled Border BackColor", "Appearance",
                "Sets the disabled back bordercolor."));

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

