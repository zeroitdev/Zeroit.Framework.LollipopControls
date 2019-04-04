// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 12-18-2018
// ***********************************************************************
// <copyright file="ZeroitLollipopFileInput.cs" company="Zeroit Dev Technologies">
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
    /// A class collection for rendering a control for selecting a file with Lollipop effect.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Control" />
    [DefaultEvent("TextChanged")]
    [Designer(typeof(LollipopFileSelectDesigner))]
    public class ZeroitLollipopSelectFile : Control
    {

        #region  Variables

        /// <summary>
        /// The in put BTN
        /// </summary>
        Button InPutBTN = new Button();
        /// <summary>
        /// The lollipop tb
        /// </summary>
        TextBox LollipopTB = new TextBox();

        /// <summary>
        /// The aln type
        /// </summary>
        HorizontalAlignment ALNType;
        /// <summary>
        /// The maxchars
        /// </summary>
        int maxchars = 32767;
        /// <summary>
        /// The read only
        /// </summary>
        bool readOnly;
        /// <summary>
        /// The previous read only
        /// </summary>
        bool previousReadOnly;
        /// <summary>
        /// The is password masked
        /// </summary>
        bool isPasswordMasked = false;
        /// <summary>
        /// The enable
        /// </summary>
        bool Enable = true;

        /// <summary>
        /// The animation timer
        /// </summary>
        Timer AnimationTimer = new Timer { Interval = 1 };
        /// <summary>
        /// The font
        /// </summary>
        FontManager font = new FontManager();

        /// <summary>
        /// The dialog
        /// </summary>
        public OpenFileDialog Dialog;
        /// <summary>
        /// The filter
        /// </summary>
        string filter = @"All Files (*.*)|*.*";

        /// <summary>
        /// The focus
        /// </summary>
        bool Focus = false;

        /// <summary>
        /// The size animation
        /// </summary>
        float SizeAnimation = 0;
        /// <summary>
        /// The size inc decimal
        /// </summary>
        float SizeInc_Dec;

        /// <summary>
        /// The point animation
        /// </summary>
        float PointAnimation;
        /// <summary>
        /// The point inc decimal
        /// </summary>
        float PointInc_Dec;

        /// <summary>
        /// The font color
        /// </summary>
        Color fontColor = ColorTranslator.FromHtml("#999999");
        /// <summary>
        /// The focus color
        /// </summary>
        Color focusColor = ColorTranslator.FromHtml("#508ef5");

        /// <summary>
        /// The enabled focused color
        /// </summary>
        Color EnabledFocusedColor;
        /// <summary>
        /// The enabled string color
        /// </summary>
        Color EnabledStringColor;

        /// <summary>
        /// The enabled in put color
        /// </summary>
        Color EnabledInPutColor = ColorTranslator.FromHtml("#acacac");
        /// <summary>
        /// The enabled un focused color
        /// </summary>
        Color EnabledUnFocusedColor = ColorTranslator.FromHtml("#dbdbdb");

        /// <summary>
        /// The disabled input color
        /// </summary>
        Color DisabledInputColor = ColorTranslator.FromHtml("#d1d2d4");
        /// <summary>
        /// The disabled un focused color
        /// </summary>
        Color DisabledUnFocusedColor = ColorTranslator.FromHtml("#e9ecee");
        /// <summary>
        /// The disabled string color
        /// </summary>
        Color DisabledStringColor = ColorTranslator.FromHtml("#babbbd");

        /// <summary>
        /// The text back color
        /// </summary>
        private Color textBackColor = Color.White;

        #endregion

        #region  Properties

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        public HorizontalAlignment TextAlignment
        {
            get
            {
                return ALNType;
            }
            set
            {
                ALNType = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the maximum length of the characters.
        /// </summary>
        /// <value>The maximum length.</value>
        [Category("Behavior")]
        public int MaxLength
        {
            get
            {
                return maxchars;
            }
            set
            {
                maxchars = value;
                LollipopTB.MaxLength = MaxLength;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use a system password character.
        /// </summary>
        /// <value><c>true</c> if use system password character; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool UseSystemPasswordChar
        {
            get
            {
                return isPasswordMasked;
            }
            set
            {
                LollipopTB.UseSystemPasswordChar = UseSystemPasswordChar;
                isPasswordMasked = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control has read only attribute set.
        /// </summary>
        /// <value><c>true</c> if read only; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool ReadOnly
        {
            get
            {
                return readOnly;
            }
            set
            {
                readOnly = value;
                if (LollipopTB != null)
                {
                    LollipopTB.ReadOnly = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is enabled.
        /// </summary>
        /// <value><c>true</c> if the control is enabled; otherwise, <c>false</c>.</value>
        [Category("Behavior")]
        public bool IsEnabled
        {
            get { return Enable; }
            set
            {
                Enable = value;

                if (IsEnabled)
                {
                    readOnly = previousReadOnly;
                    LollipopTB.ReadOnly = previousReadOnly;
                    LollipopTB.ForeColor = EnabledStringColor;
                    InPutBTN.Enabled = true;
                }
                else
                {
                    previousReadOnly = ReadOnly;
                    ReadOnly = true;
                    LollipopTB.ForeColor = DisabledStringColor;
                    InPutBTN.Enabled = false;
                }

                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the string filter.
        /// </summary>
        /// <value>The filter.</value>
        [Category("Behavior")]
        public string Filter
        {
            get { return filter; }
            set
            {
                filter = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the color when focused.
        /// </summary>
        /// <value>The color when focused.</value>
        [Category("Appearance")]
        public Color FocusedColor
        {
            get { return focusColor; }
            set
            {
                focusColor = value;
                Invalidate();
            }
        }


        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>The color of the font.</value>
        [Category("Appearance")]
        public Color FontColor
        {
            get { return fontColor; }
            set
            {
                fontColor = value;
                Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control can respond to user interaction.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        [Browsable(false)]
        public bool Enabled
        {
            get { return base.Enabled; }
            set { base.Enabled = value; }
        }

        /// <summary>
        /// Gets or sets the font of the text displayed by the control.
        /// </summary>
        /// <value>The font.</value>
        [Browsable(false)]
        public Font Font
        {
            get { return base.Font; }
            set { base.Font = value; }
        }

        /// <summary>
        /// Gets or sets the foreground color of the control.
        /// </summary>
        /// <value>The color of the fore.</value>
        [Browsable(false)]
        public Color ForeColor
        {
            get { return base.ForeColor; }
            set { base.ForeColor = value; }
        }

        /// <summary>
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Color TextBackgroundColor
        {
            get { return textBackColor; }
            set
            {
                textBackColor = value;
                LollipopTB.BackColor = value;
                Invalidate();
            }
        }

        #endregion

        #region  Events

        /// <summary>
        /// Handles the <see cref="E:KeyDown" /> event.
        /// </summary>
        /// <param name="Obj">The object.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        protected void OnKeyDown(object Obj, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                LollipopTB.SelectAll();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.C)
            {
                LollipopTB.Copy();
                e.SuppressKeyPress = true;
            }
            if (e.Control && e.KeyCode == Keys.X)
            {
                LollipopTB.Cut();
                e.SuppressKeyPress = true;
            }
        }
        /// <summary>
        /// Browses down.
        /// </summary>
        /// <param name="Obj">The object.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BrowseDown(object Obj, EventArgs e)
        {
            Dialog = new OpenFileDialog();
            Dialog.Filter = filter;
            DialogResult result = Dialog.ShowDialog();

            if (result == DialogResult.OK && Dialog.SafeFileName != null)
            {
                Text = Dialog.FileName;
            }
            Focus();
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.TextChanged" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnTextChanged(System.EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.GotFocus" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnGotFocus(System.EventArgs e)
        {
            base.OnGotFocus(e);
            LollipopTB.Focus();
            LollipopTB.SelectionLength = 0;
        }
        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Resize" /> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
        protected override void OnResize(System.EventArgs e)
        {
            base.OnResize(e);

            Height = 24;

            PointAnimation = Width / 2;
            SizeInc_Dec = Width / 18;
            PointInc_Dec = Width / 36;

            LollipopTB.Width = Width - 21;
            InPutBTN.Location = new Point(Width - 21, 1);
            InPutBTN.Size = new Size(21, 20);
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ZeroitLollipopSelectFile" /> class.
        /// </summary>
        public ZeroitLollipopSelectFile()
        {
            Width = 300;
            DoubleBuffered = true;
            previousReadOnly = ReadOnly;

            AddButton();
            AddTextBox();
            Controls.Add(LollipopTB);
            Controls.Add(InPutBTN);

            LollipopTB.TextChanged += (sender, args) => Text = LollipopTB.Text;
            base.TextChanged += (sender, args) => LollipopTB.Text = Text;

            AnimationTimer.Tick += new EventHandler(AnimationTick);
        }

        #endregion

        #region Events and Overrides

        /// <summary>
        /// Raises the <see cref="E:System.Windows.Forms.Control.Paint" /> event.
        /// </summary>
        /// <param name="e">A <see cref="T:System.Windows.Forms.PaintEventArgs" /> that contains the event data.</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            base.OnPaint(e);
            Bitmap B = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(B);
            G.Clear(Color.Transparent);

            LollipopTB.BackColor = textBackColor;

            EnabledStringColor = fontColor;
            EnabledFocusedColor = focusColor;

            LollipopTB.TextAlign = TextAlignment;
            LollipopTB.ForeColor = IsEnabled ? EnabledStringColor : DisabledStringColor;
            LollipopTB.UseSystemPasswordChar = UseSystemPasswordChar;

            G.DrawLine(new Pen(new SolidBrush(IsEnabled ? EnabledUnFocusedColor : DisabledUnFocusedColor)), new Point(0, Height - 2), new Point(Width, Height - 2));
            if (IsEnabled)
            { G.FillRectangle(new SolidBrush(EnabledFocusedColor), PointAnimation, (float)Height - 3, SizeAnimation, 2); }

            G.SmoothingMode = SmoothingMode.AntiAlias;
            G.FillEllipse(new SolidBrush(IsEnabled ? EnabledInPutColor : DisabledInputColor), Width - 5, 9, 4, 4);
            G.FillEllipse(new SolidBrush(IsEnabled ? EnabledInPutColor : DisabledInputColor), Width - 11, 9, 4, 4);
            G.FillEllipse(new SolidBrush(IsEnabled ? EnabledInPutColor : DisabledInputColor), Width - 17, 9, 4, 4);

            e.Graphics.DrawImage((Image)(B.Clone()), 0, 0);
            G.Dispose();
            B.Dispose();
        }

        /// <summary>
        /// Adds the button.
        /// </summary>
        public void AddButton()
        {
            InPutBTN.Location = new Point(Width - 21, 1);
            InPutBTN.Size = new Size(21, 20);

            InPutBTN.ForeColor = Color.FromArgb(255, 255, 255);
            InPutBTN.TextAlign = ContentAlignment.MiddleCenter;
            InPutBTN.BackColor = Color.Transparent;

            InPutBTN.TabStop = false;
            InPutBTN.FlatStyle = FlatStyle.Flat;
            InPutBTN.FlatAppearance.MouseOverBackColor = Color.Transparent;
            InPutBTN.FlatAppearance.MouseDownBackColor = Color.Transparent;
            InPutBTN.FlatAppearance.BorderSize = 0;

            InPutBTN.MouseDown += BrowseDown;
            InPutBTN.MouseEnter += (sender, args) => EnabledInPutColor = EnabledFocusedColor;
            InPutBTN.MouseLeave += (sender, args) => EnabledInPutColor = ColorTranslator.FromHtml("#acacac");
        }

        /// <summary>
        /// Adds the text box.
        /// </summary>
        public void AddTextBox()
        {
            textBackColor = BackColor;
            LollipopTB.Text = Text;
            LollipopTB.Location = new Point(0, 1);
            LollipopTB.Size = new Size(Width - 21, 20);

            LollipopTB.Multiline = false;
            LollipopTB.Font = font.Roboto_Regular10;
            LollipopTB.ScrollBars = ScrollBars.None;
            LollipopTB.BorderStyle = BorderStyle.None;
            LollipopTB.TextAlign = HorizontalAlignment.Left;
            LollipopTB.BackColor = textBackColor;
            LollipopTB.UseSystemPasswordChar = UseSystemPasswordChar;

            LollipopTB.KeyDown += OnKeyDown;

            LollipopTB.GotFocus += (sender, args) => Focus = true; AnimationTimer.Start();
            LollipopTB.LostFocus += (sender, args) => Focus = false; AnimationTimer.Start();
        }

        /// <summary>
        /// Animations the tick.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void AnimationTick(object sender, EventArgs e)
        {
            if (Focus)
            {
                if (SizeAnimation < Width)
                {
                    SizeAnimation += SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation > 0)
                {
                    PointAnimation -= PointInc_Dec;
                    this.Invalidate();
                }
            }
            else
            {
                if (SizeAnimation > 0)
                {
                    SizeAnimation -= SizeInc_Dec;
                    this.Invalidate();
                }

                if (PointAnimation < Width / 2)
                {
                    PointAnimation += PointInc_Dec;
                    this.Invalidate();
                }
            }
        }

        #endregion

    }


    #region Smart Tag Code

    #region Cut and Paste it on top of the component class

    //--------------- [Designer(typeof(myControlDesigner))] --------------------//
    #endregion

    #region ControlDesigner
    /// <summary>
    /// Class LollipopFileSelectDesigner.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Design.ControlDesigner" />
    [System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Demand, Name = "FullTrust")]
    public class LollipopFileSelectDesigner : System.Windows.Forms.Design.ControlDesigner
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
                    actionLists.Add(new LollipopFileSelectSmartTagActionList(this.Component));
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
    /// Class LollipopFileSelectSmartTagActionList.
    /// </summary>
    /// <seealso cref="System.ComponentModel.Design.DesignerActionList" />
    public class LollipopFileSelectSmartTagActionList : System.ComponentModel.Design.DesignerActionList
    {
        //Replace SmartTag with the Component Class Name. In this case the component class name is SmartTag
        /// <summary>
        /// The col user control
        /// </summary>
        private ZeroitLollipopSelectFile colUserControl;


        /// <summary>
        /// The designer action UI SVC
        /// </summary>
        private DesignerActionUIService designerActionUISvc = null;


        /// <summary>
        /// Initializes a new instance of the <see cref="LollipopFileSelectSmartTagActionList"/> class.
        /// </summary>
        /// <param name="component">A component related to the <see cref="T:System.ComponentModel.Design.DesignerActionList" />.</param>
        public LollipopFileSelectSmartTagActionList(IComponent component) : base(component)
        {
            this.colUserControl = component as ZeroitLollipopSelectFile;

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
        /// Gets or sets the font.
        /// </summary>
        /// <value>The font.</value>
        public Font Font
        {
            get
            {
                return colUserControl.Font;
            }
            set
            {
                GetPropertyByName("Font").SetValue(colUserControl, value);
            }
        }

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
        /// Gets or sets the color of the text background.
        /// </summary>
        /// <value>The color of the text background.</value>
        public Color TextBackgroundColor
        {
            get
            {
                return colUserControl.TextBackgroundColor;
            }
            set
            {
                GetPropertyByName("TextBackgroundColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the focused.
        /// </summary>
        /// <value>The color of the focused.</value>
        public Color FocusedColor
        {
            get
            {
                return colUserControl.FocusedColor;
            }
            set
            {
                GetPropertyByName("FocusedColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the color of the font.
        /// </summary>
        /// <value>The color of the font.</value>
        public Color FontColor
        {
            get
            {
                return colUserControl.FontColor;
            }
            set
            {
                GetPropertyByName("FontColor").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>The text alignment.</value>
        public HorizontalAlignment TextAlignment
        {
            get
            {
                return colUserControl.TextAlignment;
            }
            set
            {
                GetPropertyByName("TextAlignment").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use system password character].
        /// </summary>
        /// <value><c>true</c> if [use system password character]; otherwise, <c>false</c>.</value>
        public bool UseSystemPasswordChar
        {
            get
            {
                return colUserControl.UseSystemPasswordChar;
            }
            set
            {
                GetPropertyByName("UseSystemPasswordChar").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [read only].
        /// </summary>
        /// <value><c>true</c> if [read only]; otherwise, <c>false</c>.</value>
        public bool ReadOnly
        {
            get
            {
                return colUserControl.ReadOnly;
            }
            set
            {
                GetPropertyByName("ReadOnly").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is enabled.
        /// </summary>
        /// <value><c>true</c> if this instance is enabled; otherwise, <c>false</c>.</value>
        public bool IsEnabled
        {
            get
            {
                return colUserControl.IsEnabled;
            }
            set
            {
                GetPropertyByName("IsEnabled").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="LollipopFileSelectSmartTagActionList"/> is enabled.
        /// </summary>
        /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
        public bool Enabled
        {
            get
            {
                return colUserControl.Enabled;
            }
            set
            {
                GetPropertyByName("Enabled").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public string Filter
        {
            get
            {
                return colUserControl.Filter;
            }
            set
            {
                GetPropertyByName("Filter").SetValue(colUserControl, value);
            }
        }

        /// <summary>
        /// Gets or sets the maximum length.
        /// </summary>
        /// <value>The maximum length.</value>
        public int MaxLength
        {
            get
            {
                return colUserControl.MaxLength;
            }
            set
            {
                GetPropertyByName("MaxLength").SetValue(colUserControl, value);
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

            items.Add(new DesignerActionPropertyItem("UseSystemPasswordChar",
                "Use System Password Char", "Appearance",
                "Sets the the control to use the systems password character."));

            items.Add(new DesignerActionPropertyItem("ReadOnly",
                "Read Only", "Appearance",
                "Sets the control to have a readonly property."));

            items.Add(new DesignerActionPropertyItem("IsEnabled",
                "Is Enabled", "Appearance",
                "Sets the control to be enabled."));

            items.Add(new DesignerActionPropertyItem("BackColor",
                                 "Back Color", "Appearance",
                                 "Selects the background color."));

            items.Add(new DesignerActionPropertyItem("ForeColor",
                                 "Fore Color", "Appearance",
                                 "Selects the foreground color."));

            items.Add(new DesignerActionPropertyItem("TextBackgroundColor",
                                 "Text Background Color", "Appearance",
                                 "Sets the text background color."));

            items.Add(new DesignerActionPropertyItem("FocusedColor",
                                 "Focused Color", "Appearance",
                                 "Sets the focused color."));

            items.Add(new DesignerActionPropertyItem("FontColor",
                                "Font Color", "Appearance",
                                "Sets the font color."));

            items.Add(new DesignerActionPropertyItem("TextAlignment",
                                "Text Alignment", "Appearance",
                                "Type few characters to filter Cities."));

            
            items.Add(new DesignerActionPropertyItem("Filter",
                "Filter", "Appearance",
                "Sets the filter text.")); 

            items.Add(new DesignerActionPropertyItem("MaxLength",
                "Max Length", "Appearance",
                "Sets the maximum length."));

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


