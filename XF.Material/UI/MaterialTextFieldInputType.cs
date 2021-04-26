namespace XF.Material.Forms.UI
{
    /// <summary>
    /// Enumeration used in determining the type of keyboard input to use for <see cref="MaterialTextField"/>
    /// </summary>
    public enum MaterialTextFieldInputType
    {
        Default,
        Plain,
        Chat,
        Email,
        Numeric,
        Telephone,
        Text,
        Url,
        Password,
        NumericPassword,
        Choice,
        /// <summary>
        /// Same as Choice, but without OK button. Clicking an item will simulate a click on the OK button and close the dialog.
        /// </summary>
        SingleImmediateChoice,
        /// <summary>
        /// Triggers the command when clicked instead of focusing the input field
        /// </summary>
        CommandChoice,
        /// <summary>
        /// Does not allow changes, and does not gray out the control
        /// </summary>
        ReadOnly
    }
}
