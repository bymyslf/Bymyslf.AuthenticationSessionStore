using System;

namespace Bymyslf.AuthenticationSessionStore.Utils
{
    /// <summary>
    /// Helper class which allow prettier code for guard clauses
    /// </summary>
    public class Guard
    {
        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion">If set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        public static void Against<TException>(bool assertion, string message)
            where TException : Exception
        {
            Against<TException>(assertion, message, innerException: null);
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion">If set to <c>true</c> [assertion].</param>
        /// <param name="message">The message format.</param>
        /// <param name="args">An object array that contains the elements for <paramref name="message"/>.</param>
        public static void Against<TException>(bool assertion, string messageFormat, params object[] args)
           where TException : Exception
        {
            Against<TException>(assertion, string.Format(messageFormat, args), innerException: null);
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion">If set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public static void Against<TException>(bool assertion, string message, Exception innerException)
            where TException : Exception
        {
            if (assertion == false)
            {
                return;
            }

            throw (TException)typeof(TException).GetConstructor(new Type[] { typeof(string), typeof(Exception) }).Invoke(new object[] { message, innerException });
        }

        /// <summary>
        /// Will throw exception of type <typeparamref name="TException"/>
        /// with the specified message if the assertion is true
        /// </summary>
        /// <typeparam name="TException"></typeparam>
        /// <param name="assertion">If set to <c>true</c> [assertion].</param>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="args">An object array that contains the elements for <paramref name="message"/>.</param>
        public static void Against<TException>(bool assertion, Exception innerException, string messageFormat, params object[] args)
            where TException : Exception
        {
            Against<TException>(assertion, string.Format(messageFormat, args), innerException);
        }
    }
}