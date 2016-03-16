//-----------------------------------------------------------------------
// <copyright file="Input.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace OpenTK.InputManager
{
    using System;
    using Newtonsoft.Json;
    using OpenTK.Input;

    /// <summary>
    /// Represents an input item such as a key press or button press.
    /// </summary>
    public struct Input
    {
        /// <summary>
        /// The joystick axis.
        /// </summary>
        private readonly JoystickAxis? joystickAxis;

        /// <summary>
        /// The joystick button.
        /// </summary>
        private readonly JoystickButton? joystickButton;

        /// <summary>
        /// The joystick hat.
        /// </summary>
        private readonly JoystickHat? joystickHat;

        /// <summary>
        /// The key.
        /// </summary>
        private readonly Key? key;

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> struct.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="inputName">The name of the input.</param>
        [JsonConstructor]
        public Input(string actionName, string inputName)
        {
            ActionName = actionName;
            this.key = null;
            this.joystickAxis = null;
            this.joystickButton = null;
            this.joystickHat = null;

            Key key;
            if (Enum.TryParse(inputName, out key))
            {
                this.key = key;
                return;
            }

            JoystickAxis joystickAxis;
            if (Enum.TryParse(inputName, out joystickAxis))
            {
                this.joystickAxis = joystickAxis;
                return;
            }

            JoystickButton joystickButton;
            if (Enum.TryParse(inputName, out joystickButton))
            {
                this.joystickButton = joystickButton;
                return;
            }

            JoystickHat joystickHat;
            if (Enum.TryParse(inputName, out joystickHat))
            {
                this.joystickHat = joystickHat;
                return;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> struct.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="key">The key.</param>
        public Input(string actionName, Key key)
        {
            ActionName = actionName;
            this.key = key;
            joystickAxis = null;
            joystickButton = null;
            joystickHat = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> struct.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="joystickAxis">The joystick axis.</param>
        public Input(string actionName, JoystickAxis joystickAxis)
        {
            ActionName = actionName;
            key = null;
            this.joystickAxis = joystickAxis;
            joystickButton = null;
            joystickHat = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> struct.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="joystickButton">The joystick button.</param>
        public Input(string actionName, JoystickButton joystickButton)
        {
            ActionName = actionName;
            key = null;
            joystickAxis = null;
            this.joystickButton = joystickButton;
            joystickHat = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Input"/> struct.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="joystickHat">The joystick hat.</param>
        public Input(string actionName, JoystickHat joystickHat)
        {
            ActionName = actionName;
            key = null;
            joystickAxis = null;
            joystickButton = null;
            this.joystickHat = joystickHat;
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <value>The name of the action.</value>
        public string ActionName { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name
        {
            get
            {
                if (key != null)
                    return key.ToString();
                if (joystickAxis != null)
                    return joystickAxis.ToString();
                if (joystickButton != null)
                    return joystickButton.ToString();
                if (joystickHat != null)
                    return joystickHat.ToString();

                return base.ToString();
            }
        }

        /// <summary>
        /// Gets the state of this instance.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The state of this instance.</returns>
        public bool GetState(int index)
        {
            var result = false;

            if (key != null)
                result = Keyboard.GetState(index)[(Key)key];
            if (joystickAxis != null)
                throw new NotImplementedException(nameof(JoystickAxis));
            if (joystickButton != null)
                throw new NotImplementedException(nameof(JoystickButton));
            if (joystickHat != null)
                throw new NotImplementedException(nameof(JoystickHat));

            return result;
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return Name;
        }
    }
}