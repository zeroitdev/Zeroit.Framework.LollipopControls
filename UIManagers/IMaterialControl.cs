// ***********************************************************************
// Assembly         : Zeroit.Framework.LollipopControls
// Author           : ZEROIT
// Created          : 11-22-2018
//
// Last Modified By : ZEROIT
// Last Modified On : 07-03-2018
// ***********************************************************************
// <copyright file="IMaterialControl.cs" company="Zeroit Dev Technologies">
//     Copyright © Zeroit Dev Technologies  2017. All Rights Reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Zeroit.Framework.LollipopControls.Controls
{
    /// <summary>
    /// Interface IMaterialControl
    /// </summary>
    interface IMaterialControl
    {
        /// <summary>
        /// Gets or sets the depth.
        /// </summary>
        /// <value>The depth.</value>
        int Depth { get; set; }
        /// <summary>
        /// Gets or sets the state of the mouse.
        /// </summary>
        /// <value>The state of the mouse.</value>
        MouseState MouseState { get; set; }

        /// <summary>
        /// Gets the color of the back.
        /// </summary>
        /// <value>The color of the back.</value>
        System.Drawing.Color BackColor { get; } 

    }

    /// <summary>
    /// Enum MouseState
    /// </summary>
    public enum MouseState
    {
        /// <summary>
        /// The hover
        /// </summary>
        HOVER,
        /// <summary>
        /// Down
        /// </summary>
        DOWN,
        /// <summary>
        /// The out
        /// </summary>
        OUT
    }
}
