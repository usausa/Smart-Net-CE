﻿namespace Smart.Data
{
    using System.Data;

    /// <summary>
    ///
    /// </summary>
    public static class DbConnectionExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="con"></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static void OpenIfNot(this IDbConnection con)
        {
            if ((con.State & ConnectionState.Open) != ConnectionState.Open)
            {
                con.Open();
            }
        }
    }
}
