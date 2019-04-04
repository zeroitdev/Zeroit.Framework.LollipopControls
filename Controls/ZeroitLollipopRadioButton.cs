// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopRadioButton.cs" company="Zeroit Dev Technologies">
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
using System.Collections.Generic;
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
    /// A class collection for rendering Lollipop radio button.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.RadioButton" />
    [Designer(typeof(LollipopRadioButtonDesigner))]
    public class ZeroitLollipopRadioButton : Control
    {
        #region Enumeration        
        /// <summary>
        /// Enum for retrieving and setting the type of Radio button
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

        #endregion

        #region Variables

        //string HexColor = "#508ef5";

        //Color EnabledCheckedColor;
        //Color EnabledUnCheckedColor = ColorTranslator.FromHtml("#9c9ea1");

        //Color DisabledColor = ColorTranslator.FromHtml("#c4c6ca");
        //Color EnabledStringColor = ColorTranslator.FromHtml("#929292");

        /// <summary>
        /// The disabled string color
        /// </summary>
        Color DisabledStringColor = ColorTranslator.FromHtml("#babbbd");

        /// <summary>
        /// The alpha animation timer
        /// </summary>
        Timer AlphaAnimationTimer = new Timer { Interval = 16 };
        /// <summary>
        /// The size animation timer
        /// </summary>
        Timer SizeAnimationTimer = new Timer { Interval = 35 };
        /// <summary>
        /// The radius inner timer
        /// </summary>
        Timer RadiusInnerTimer = new Timer { Interval = 35 };
        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();

        /// <summary>
        /// The size animation number
        /// </summary>
        int SizeAnimationNum = 0;
        /// <summary>
        /// The point animation number
        /// </summary>
        int PointAnimationNum = 9;
        /// <summary>
        /// The alpha
        /// </summary>
        int Alpha = 0;
        /// <summary>
        /// The checked color
        /// </summary>
        private Color checkedColor = Color.FromArgb(80, 142, 245);
        /// <summary>
        /// The unchecked color
        /// </summary>
        private Color uncheckedColor = Color.White;
        /// <summary>
        /// The draw type
        /// </summary>
        private DrawType _drawType = DrawType.Circle;

        /// <summary>
        /// The sides
        /// </summary>
        private int sides = 3;
        /// <summary>
        /// The radius
        /// </summary>
        private int radius = 10;
        /// <summary>
        /// The starting angle
        /// </summary>
        private int startingAngle = 90;
        /// <summary>
        /// The center
        /// </summary>
        Point center;
        /// <summary>
        /// The center2
        /// </summary>
        Point center2;
        /// <summary>
        /// The center3
        /// </summary>
        Point center3;

        /// <summary>
        /// The radius inner
        /// </summary>
        private int radiusInner = 8;
        /// <summary>
        /// The radius inner1
        /// </summary>
        private int radiusInner1 = 6;
        /// <summary>
        /// The inner effect
        /// </summary>
        private int innerEffect = 6;
        /// <summary>
        /// The poly point
        /// </summary>
        private int polyPoint = 18;

        /// <summary>
        /// The checked
        /// </summary>
        private bool @checked = false;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the text associated with this control.
        /// </summary>
        /// <value>The text.</value>
        public new string Text
        {
            get { return base.Text; }
            set
            {
                base.Text = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ZeroitLollipopRadioButton"/> is checked.
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
        /// Gets or sets the polygon starting angle.
        /// </summary>
        /// <value>The polygon starting angle.</value>
        public int PolyAngle
        {
            get { return startingAngle; }
            set
            {
                startingAngle = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the polygon sides.
        /// </summary>
        /// <value>The polygon sides.</value>
        public int Polysides
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
        /// Gets or sets the inner radius effect.
        /// </summary>
        /// <value>The inner radius effect.</value>
        public int InnerRadiusEffect
        {
            get { return innerEffect; }
            set
            {
                innerEffect = value;
                radiusInner1 = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the type of the rendered shape.
        /// </summary>
        /// <value>The type of the render.</value>
        public DrawType RenderType
        {
            get { return _drawType; }
            set
            {
                _drawType = value;
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
        /// Gets or sets the color when checked.
        /// </summary>
        /// <value>The color when checked.</value>
        public Color CheckedColor
        {
            get { return checkedColor; }
            set { checkedColor = value; Invalidate(); }
        }

        /// <summary>
        /// Gets or sets the color when unchecked.
        /// </summary>
        /// <value>The color when unchecked.</value>
        public Color UncheckedColor
        {
            get { return uncheckedColor; }
            set { uncheckedColor = value; Invalidate(); }
        }

        #endregion

        #region Events

        /// <summary>
        /// Overrides the <see cref="M:System.Windows.Forms.Control.OnHandleCreated(System.EventArgs)" /> method.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            AlphaAnimationTimer.Start();
            SizeAnimationTimer.Start();
            RadiusInnerTimer.Start();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(EventArgs e)
        {
            Height = 19;
            Width = 19 + (int)CreateGraphics().MeasureString(Text, font.Roboto_Medium10).Width;

        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Height = 19;
            Width = 19 + (int)CreateGraphics().MeasureString(Text, font.Roboto_Medium10).Width;

        }

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

        #region Private Methods

        /// <summary>
        /// Return an array of 10 points to be used in a Draw- or FillPolygon method
        /// </summary>
        /// <param name="sides">The sides.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="startingAngle">The starting angle.</param>
        /// <param name="center">The center.</param>
        /// <returns>Array of 10 PointF structures</returns>
        /// <exception cref="System.ArgumentException">Polygon must have 3 sides or more.</exception>
        public static Point[] CalculateVertices(int sides, int radius, int startingAngle, Point center)
        {


            if (sides < 3)
                throw new ArgumentException("Polygon must have 3 sides or more.");

            List<Point> points = new List<Point>();
            float step = 360.0f / sides;

            float angle = startingAngle; //starting angle
            for (double i = startingAngle; i < startingAngle + 360.0; i += step) //go in a circle
            {
                points.Add(DegreesToXY(angle, radius, center));
                angle += step;
            }

            return points.ToArray();
        }

        /// <summary>
        /// Degreeses to xy.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        /// <param name="radius">The radius.</param>
        /// <param name="origin">The origin.</param>
        /// <returns>Point.</returns>
        public static Point DegreesToXY(float degrees, float radius, Point origin)
        {
            Point xy = new Point();
            double radians = degrees * Math.PI / 180.0;

            xy.X = (int)(Math.Cos(radians) * radius + origin.X);
            xy.Y = (int)(Math.Sin(-radians) * radius + origin.Y);

            return xy;
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




        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopRadioButton" /> class.
        /// </summary>
        public ZeroitLollipopRadioButton()
        {
            AlphaAnimationTimer.Tick += new EventHandler(AlphaAnimationTick);
            SizeAnimationTimer.Tick += new EventHandler(SizeAnimationTick);
            RadiusInnerTimer.Tick += new EventHandler(RadiusAnimationTick);

            
            DoubleBuffered = true;

            ForeColor = Color.FromArgb(53,53,53);
        }

        /// <summary>
        /// Raises the <see cref="M:System.Windows.Forms.ButtonBase.OnPaint(System.Windows.Forms.PaintEventArgs)" /> event.
        /// </summary>
        /// <param name="pevent">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(PaintEventArgs pevent)
        {
            TransInPaint(pevent.Graphics);
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality;
            //g.Clear(BackColor);

            center = new Point(18 / 2, 18 / 2);
            center2 = new Point(18 / 2, 18 / 2);
            center3 = new Point(polyPoint / 2, polyPoint / 2);

            Point[] PolyGon1 = CalculateVertices(sides, radius, startingAngle, center);
            Point[] PolyGon2 = CalculateVertices(sides, radiusInner, startingAngle, center2);
            Point[] PolyGon3 = CalculateVertices(sides, radiusInner1, startingAngle, center3);

            Rectangle BGEllipse = new Rectangle(0, 0, 18, 18);

            //EnabledCheckedColor = ColorTranslator.FromHtml(HexColor);
            SolidBrush BG = new SolidBrush(checkedColor);

            //RadioButton BG

            switch (_drawType)
            {
                case DrawType.Circle:
                    if (Checked)
                    {
                        g.FillEllipse(new SolidBrush(Color.FromArgb(Alpha, BG.Color)), BGEllipse);
                        g.FillEllipse(new SolidBrush(uncheckedColor), new Rectangle(2, 2, 14, 14));
                    }
                    else
                    {
                        g.FillEllipse(BG, BGEllipse);
                        g.FillEllipse(new SolidBrush(uncheckedColor), new Rectangle(2, 2, 14, 14));
                    }

                    g.FillEllipse(BG, new Rectangle(PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum));

                    break;
                case DrawType.Rectangle:
                    if (Checked)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(Alpha, BG.Color)), BGEllipse);
                        g.FillRectangle(new SolidBrush(uncheckedColor), new Rectangle(2, 2, 14, 14));
                    }
                    else
                    {
                        g.FillRectangle(BG, BGEllipse);
                        g.FillRectangle(new SolidBrush(uncheckedColor), new Rectangle(2, 2, 14, 14));
                    }

                    g.FillRectangle(BG, new Rectangle(PointAnimationNum, PointAnimationNum, SizeAnimationNum, SizeAnimationNum));

                    break;
                case DrawType.Polygon:
                    if (Checked)
                    {
                        
                        g.FillPolygon(new SolidBrush(Color.FromArgb(Alpha, BG.Color)), PolyGon1);
                        g.FillPolygon(new SolidBrush(uncheckedColor), PolyGon2);
                    }
                    else
                    {
                        g.FillPolygon(BG, PolyGon1);
                        g.FillPolygon(new SolidBrush(uncheckedColor), PolyGon2);
                    }


                    g.FillPolygon(BG , PolyGon3);
                    //Color color = Color.Red;
                    //g.FillPolygon(new SolidBrush(color), PolyGon3);

                    break;
                
            }

            
            //RadioButton Text
            //g.DrawString(Text, font.Roboto_Medium10, new SolidBrush(Enabled ? EnabledStringColor : DisabledStringColor), 20, 0);
            g.DrawString(Text, font.Roboto_Medium10, new SolidBrush(ForeColor), 20, 0);



        }

        /// <summary>
        /// Alphas the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void AlphaAnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (Alpha < 250)
                {
                    Alpha += 25;
                    this.Invalidate();
                }
            }
            else if (Alpha > 0)
            {
                Alpha -= 25;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Sizes the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void SizeAnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (SizeAnimationNum < 8)
                {
                    SizeAnimationNum += 2;
                    this.Invalidate();

                    if (PointAnimationNum > 5)
                    {
                        PointAnimationNum -= 1;
                        this.Invalidate();
                    }
                }
            }
            else if (SizeAnimationNum > 0)
            {
                SizeAnimationNum -= 2;
                this.Invalidate();

                if (PointAnimationNum < 9)
                {
                    PointAnimationNum += 1;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Radiuses the animation tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void RadiusAnimationTick(object sender, EventArgs e)
        {
            if (Checked)
            {
                if (radiusInner1 < innerEffect)
                {
                    radiusInner1 += 2;
                    this.Invalidate();

                    if (polyPoint > 18)
                    {
                        polyPoint -= 1;
                        this.Invalidate();
                    }
                }
            }
            else if (radiusInner1 > 0)
            {
                radiusInner1 -= 2;
                this.Invalidate();

                if (polyPoint < 18)
                {
                    polyPoint += 1;
                    this.Invalidate();
                }
            }
        }


    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopRadioButtonDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopRadioButtonDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopRadioButtonSmartTagActionList(this.Component));
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
    /// Class LollipopRadioButtonSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopRadioButtonSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopRadioButton colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopRadioButtonSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopRadioButtonSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopRadioButton;

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
        /// Gets or sets the type of the render.
        /// </summary>
        /// <value>The type of the render.</value>
        public ZeroitLollipopRadioButton.DrawType RenderType
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
        /// Gets or sets the poly angle.
        /// </summary>
        /// <value>The poly angle.</value>
        public int PolyAngle
        {
            get
            {
                return colUserControl.PolyAngle;
            }
            set
            {
                GetPropertyByName("PolyAngle").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the polysides.
        /// </summary>
        /// <value>The polysides.</value>
        public int Polysides
        {
            get
            {
                return colUserControl.Polysides;
            }
            set
            {
                GetPropertyByName("Polysides").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the inner radius effect.
        /// </summary>
        /// <value>The inner radius effect.</value>
        public int InnerRadiusEffect
        {
            get
            {
                return colUserControl.InnerRadiusEffect;
            }
            set
            {
                GetPropertyByName("InnerRadiusEffect").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("RenderType",
                                 "Render Type", "Appearance",
                                 "Sets the polygon type."));

            items.Add(new DesignerActionPropertyItem("CheckedColor",
                                 "Checked Color", "Appearance",
                                 "Sets the checked color."));


            items.Add(new DesignerActionPropertyItem("UncheckedColor",
                "Unchecked Color", "Appearance",
                "Sets the unchecked color."));

            items.Add(new DesignerActionPropertyItem("PolyAngle",
                "Poly Angle", "Appearance",
                "Sets the polygon angle."));


            items.Add(new DesignerActionPropertyItem("Polysides",
                "Poly sides", "Appearance",
                "Sets the polygon sides."));

            items.Add(new DesignerActionPropertyItem("InnerRadiusEffect",
                "Inner Radius Effect", "Appearance",
                "Sets the inner radius effect."));

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
