namespace Client.Logic.Exceptions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Class that handles any exceptions that occur in the program.
    /// </summary>
    public class ExceptionHandler
    {
        #region Methods

        /// <summary>
        /// Called when an exception occurs.
        /// </summary>
        /// <param name="ex">The exception.</param>
        public static void OnException(Exception ex)
        {
            ErrorBox.ShowDialog("Unhandled Exception", ex.Message, ex.ToString());
        }

        #endregion Methods
    }
}