//-----------------------------------------------------------------------
// <copyright file="InputProfile.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace OpenTK.InputManager
{
    using System.Collections.Generic;
    using System.IO;
    using Newtonsoft.Json;

    /// <summary>
    /// Input state.
    /// </summary>
    public enum InputState
    {
        Up,
        Down,
        Pressed,
    }

    /// <summary>
    /// Represents a configuration of inputs.
    /// </summary>
    public class InputProfile
    {
        /// <summary>
        /// The inputs that were down last check.
        /// </summary>
        private readonly HashSet<Input> justDown = new HashSet<Input>();

        /// <summary>
        /// Initializes a new instance of the <see cref="InputProfile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="inputs">The inputs.</param>
        /// <param name="index">The input index.</param>
        public InputProfile(string name, List<Input> inputs, int index = 0)
        {
            Name = name;
            Inputs = inputs;
            Index = index;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; }

        /// <summary>
        /// Gets the inputs.
        /// </summary>
        /// <value>The inputs.</value>
        public List<Input> Inputs { get; } = new List<Input>();

        /// <summary>
        /// Gets the device index.
        /// </summary>
        /// <value>The device index.</value>
        public int Index { get; }

        /// <summary>
        /// Loads the <see cref="InputProfile"/> from the specified filename.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns>The <see cref="InputProfile"/>.</returns>
        public static InputProfile Load(string filename)
        {
            // ensure the file exists
            if (File.Exists(filename))
                return JsonConvert.DeserializeObject<InputProfile>(File.ReadAllText(filename));

            // return default if the file does not exist
            return default(InputProfile);
        }

        /// <summary>
        /// Gets the input state of the specified action.
        /// </summary>
        /// <param name="actionName">Name of the action.</param>
        /// <returns>The input state.</returns>
        public InputState GetState(string actionName)
        {
            // ensure an input with the action name exists
            if (!Inputs.Exists(i => i.ActionName == actionName))
                return InputState.Up;

            var input = Inputs.Find(i => i.ActionName == actionName);

            // check the input state
            if (input.GetState(Index))
            {
                // add to our just down collection
                justDown.Add(input);

                return InputState.Down;
            }
            else
            {
                // if the input was just down, return pressed
                if (justDown.Contains(input))
                {
                    // remove from our just down collection
                    justDown.Remove(input);

                    return InputState.Pressed;
                }
                else
                    return InputState.Up;
            }
        }
    }
}