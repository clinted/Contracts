using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;

namespace BICC
{
	/// <summary>
	/// Implements Design By Contract style operation result validation that throw public exceptions upon failure.
	/// </summary>
	public static class Verify
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
		/// Throws an <see cref="InvalidOperationException"/> if a condition is false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string message )
		{
			if( !condition )
			{
				throw new InvalidOperationException( message );
			}
		}

		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/> if a condition is false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string unformattedMessage, object arg1 )
		{
			if( !condition )
			{
				throw new InvalidOperationException( Format( unformattedMessage, arg1 ) );
			}
		}

		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/> if a condition is false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string unformattedMessage, object arg1, object arg2 )
		{
			if( !condition )
			{
				throw new InvalidOperationException( Format( unformattedMessage, arg1, arg2 ) );
			}
		}

		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/> if a condition is false.
		/// </summary>
		[DebuggerHidden]
		public static void IsTrue( bool condition, string unformattedMessage, params object[] args )
		{
			if( !condition )
			{
				throw new InvalidOperationException( Format( unformattedMessage, args ) );
			}
		}

		/// <summary>
		/// Throws an <see cref="InvalidOperationException"/>.
		/// </summary>
		/// <returns>
		/// Nothing.  This method always throws.
		/// The signature claims to return an exception to allow callers to throw this method
		/// to satisfy C# execution path constraints.
		/// </returns>
		[DebuggerHidden]
		public static Exception FailOperation( string message, params object[] args )
		{
			throw new InvalidOperationException( Format( message, args ) );
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
