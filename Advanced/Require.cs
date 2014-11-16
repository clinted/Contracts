using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyProject.Core
{
	/// <summary>
	/// Implements Design By Contract style requirements checking 
	/// </summary>
	public static class Require
	{
		[DebuggerHidden]
		public static void NotNull<T>( T argument, string argumentName ) where T : class
		{
			if( argument == null )
			{
				throw new ArgumentNullException( argumentName );
			}
		}

		[DebuggerHidden]
		public static void NotNullOrEmpty( string argument, string argumentName )
		{
			if( string.IsNullOrEmpty( argument ) )
			{
				throw new ArgumentNullException( argument, argumentName );
			}
		}

	    [DebuggerHidden]
	    public static void NotNullOrEmpty<T>(IList<T> collection, string argumentName)
	    {
	        if (collection == null || collection.Count == 0)
	        {
	            throw new ArgumentNullException(argumentName);
	        }
	    }

		[DebuggerHidden]
		public static void MinimumLength( string argument, uint miniumumLength, string argumentName )
		{
			if( string.IsNullOrEmpty( argument ) || argument.Length < miniumumLength )
			{
				throw new ArgumentNullException( argument, string.Format( "{0} must be at least {1} characters long.", argument, miniumumLength ) );
			}
		}

		[DebuggerHidden]
		public static void Positive( int number, string argumentName )
		{
			if( number <= 0 )
			{
				throw new ArgumentOutOfRangeException( argumentName, argumentName + " should be positive." );
			}
		}

		[DebuggerHidden]
		public static void Positive( long number, string argumentName )
		{
			if( number <= 0 )
				throw new ArgumentOutOfRangeException( argumentName, argumentName + " should be positive." );
		}

		[DebuggerHidden]
		public static void Nonnegative( long number, string argumentName )
		{
			if( number < 0 )
				throw new ArgumentOutOfRangeException( argumentName, argumentName + " should be non negative." );
		}

		[DebuggerHidden]
		public static void Nonnegative( int number, string argumentName )
		{
			if( number < 0 )
				throw new ArgumentOutOfRangeException( argumentName, argumentName + " should be non negative." );
		}

		[DebuggerHidden]
		public static void NotEmptyGuid( Guid guid, string argumentName )
		{
			if( Guid.Empty == guid )
				throw new ArgumentException( argumentName, argumentName + " shoud be non-empty GUID." );
		}

		[DebuggerHidden]
		public static void Equal( int expected, int actual, string argumentName )
		{
			if( expected != actual )
				throw new ArgumentException( string.Format( "{0} expected value: {1}, actual value: {2}", argumentName, expected, actual ) );
		}

		[DebuggerHidden]
		public static void Equal( long expected, long actual, string argumentName )
		{
			if( expected != actual )
				throw new ArgumentException( string.Format( "{0} expected value: {1}, actual value: {2}", argumentName, expected, actual ) );
		}

		[DebuggerHidden]
		public static void Equal( bool expected, bool actual, string argumentName )
		{
			if( expected != actual )
				throw new ArgumentException( string.Format( "{0} expected value: {1}, actual value: {2}", argumentName, expected, actual ) );
		}
	}
}