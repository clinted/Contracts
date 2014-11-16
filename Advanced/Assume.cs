using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace MyProject.Core
{
	/// <summary>
	/// Implements Design By Contract style internal integrity runtime checks that throw public exceptions upon failure.
	/// </summary>
	public static class Assume
	{
		/// <summary>
		/// Throws an exception if the specified value is null.
		/// </summary>
		/// <typeparam name="T">The type of value to test.</typeparam>
		/// <param name="value">The value.</param>
		[DebuggerHidden]
		public static void NotNull<T>( T value, string message = null )
			 where T : class
		{
			Assume.IsTrue( value != null, message );
		}

		/// <summary>
		/// Throws an exception if the specified value is null or empty.
		/// </summary>
		/// <param name="value">The value.</param>
		[DebuggerHidden]
		public static void NotNullOrEmpty( string value, string message = null )
		{
			Assume.NotNull( value, message );
			Assume.IsTrue( value.Length > 0, message );
			Assume.IsTrue( value[ 0 ] != '\0', message );
		}

		/// <summary>
		/// Throws an exception if the specified value is null or empty.
		/// </summary>
		/// <typeparam name="T">The type of value to test.</typeparam>
		[DebuggerHidden]
		public static void NotNullOrEmpty<T>( ICollection<T> values, string message = null )
		{
			Assume.NotNull( values, message );
			Assume.IsTrue( values.Count > 0, message );
		}

		/// <summary>
		/// Throws an exception if the specified value is null or empty.
		/// </summary>
		/// <typeparam name="T">The type of value to test.</typeparam>
		/// <param name="values">The values.</param>
		[DebuggerHidden]
		public static void NotNullOrEmpty<T>( IEnumerable<T> values, string message = null )
		{
			Assume.NotNull( values, message );
			Assume.IsTrue( values.Any(), message );
		}

		/// <summary>
		/// Throws an exception if the specified value is not null.
		/// </summary>
		/// <typeparam name="T">The type of value to test.</typeparam>
		/// <param name="value">The value.</param>
		[DebuggerHidden]
		public static void Null<T>( T value, string message = null )
			where T : class
		{
			Assume.IsTrue( value == null, message );
		}

		/// <summary>
		/// Throws an exception if the specified object is not of a given type.
		/// </summary>
		/// <typeparam name="T">The type the value is expected to be.</typeparam>
		/// <param name="value">The value to test.</param>
		[DebuggerHidden]
		public static void Is<T>( object value, string message = null )
		{
			Assume.IsTrue( value is T, message );
		}

		/// <summary>
		/// Throws an public exception if a condition evaluates to false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string message = null )
		{
			if( !condition )
			{
				Assume.Fail( message );
			}
		}

		/// <summary>
		/// Throws an public exception if a condition evaluates to false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string unformattedMessage, object arg1 )
		{
			if( !condition )
			{
				Assume.Fail( Assume.Format( unformattedMessage, arg1 ) );
			}
		}

		/// <summary>
		/// Throws an public exception if a condition evaluates to false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string unformattedMessage, params object[] args )
		{
			if( !condition )
			{
				Assume.Fail( Assume.Format( unformattedMessage, args ) );
			}
		}

		/// <summary>
		/// Throws an public exception if a condition evaluates to true.
		/// </summary>
		[DebuggerHidden]
		public static void IsFalse( bool condition, string message = null )
		{
			if( condition )
			{
				Assume.Fail( message );
			}
		}

		/// <summary>
		/// Throws an public exception if a condition evaluates to true.
		/// </summary>
		[DebuggerHidden]
		public static void IsFalse( bool condition, string unformattedMessage, object arg1 )
		{
			if( condition )
			{
				Assume.Fail( Assume.Format( unformattedMessage, arg1 ) );
			}
		}

		/// <summary>
		/// Throws an public exception if a condition evaluates to true.
		/// </summary>
		[DebuggerHidden]
		public static void IsFalse( bool condition, string unformattedMessage, params object[] args )
		{
			if( condition )
			{
				Assume.Fail( Assume.Format( unformattedMessage, args ) );
			}
		}

		/// <summary>
		/// Throws an public exception.
		/// </summary>
		/// <returns>Nothing.  This method always throws.  But the signature allows calling code to "throw" this method for C# syntax reasons.</returns>
		[DebuggerHidden]
		public static Exception NotReachable()
		{
			// Keep these two as separate lines of code, so the debugger can come in during the assert dialog
			// that the exception's constructor displays, and the debugger can then be made to skip the throw
			// in order to continue the investigation.
			var exception = new InvalidOperationException();
			bool proceed = true; // allows debuggers to skip the throw statement
			if( proceed )
			{
				throw exception;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Throws an public exception.
		/// </summary>
		/// <returns>Nothing, as this method always throws.  The signature allows for "throwing" Fail so C# knows execution will stop.</returns>
		private static Exception Fail( string message = null )
		{
			var exception = new InvalidOperationException( message );
			bool proceed = true; // allows debuggers to skip the throw statement
			if( proceed )
			{
				throw exception;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Helper method that formats string arguments.
		/// </summary>
		/// <param name="format">The unformatted string.</param>
		/// <param name="arguments">The formatting arguments.</param>
		/// <returns>The formatted string.</returns>
		private static string Format( string format, params object[] arguments )
		{
			return string.Format( CultureInfo.CurrentCulture, format, arguments );
		}
	}
}
