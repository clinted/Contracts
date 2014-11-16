using System;
using System.Diagnostics;

namespace MyProject.Core
{
	/// <summary>
	/// Implements Design-By-Contract runtime checking.
	/// </summary>
	public static class Contract
	{
		// Require preconditions (things that must be true for us to do our jobs)
		[DebuggerHidden]
		public static void Precondition(bool assertion)
		{
			if (!assertion)	throw new PreconditionException();
		}

		[DebuggerHidden]
		public static void Precondition( bool assertion, string message )
		{
			if (!assertion)	throw new PreconditionException(message);
		}

		[DebuggerHidden]
		public static void Precondition( bool assertion, string message, Exception inner )
		{
			if (!assertion)	throw new PreconditionException(message, inner);
		}


		// Ensure postconditions (state we're in after we did our job)
		[DebuggerHidden]
		public static void Postcondition( bool assertion )
		{
			if (!assertion)	throw new PostconditionException();
		}

		[DebuggerHidden]
		public static void Postcondition( bool assertion, string message )
		{
			if (!assertion)	throw new PostconditionException(message);
		}

		[DebuggerHidden]
		public static void Postcondition( bool assertion, string message, Exception inner )
		{
			if (!assertion)	throw new PostconditionException(message, inner);
		}


		// Invariants safeguards (assumptions that we make about our state 
		// or things that should not have changed when we did our work )
		[DebuggerHidden]
		public static void Invariant( bool assertion )
		{
			if (!assertion)	throw new InvariantException();
		}

		[DebuggerHidden]
		public static void Invariant( bool assertion, string message )
		{
			if (!assertion)	throw new InvariantException(message);
		}

		[DebuggerHidden]
		public static void Invariant( bool assertion, string message, Exception inner )
		{
			if (!assertion)	throw new InvariantException(message, inner);
		}
	}
	
	public class PreconditionException : Exception
	{
		public PreconditionException() {}
		public PreconditionException(string message) : base(message) {}
		public PreconditionException(string message, Exception inner) : base(message, inner) {}
	}
	
	public class PostconditionException : Exception
	{
		public PostconditionException() {}
		public PostconditionException(string message) : base(message) {}
		public PostconditionException(string message, Exception inner) : base(message, inner) {}
	}

	public class InvariantException : Exception
	{
		public InvariantException() {}
		public InvariantException(string message) : base(message) {}
		public InvariantException(string message, Exception inner) : base(message, inner) {}
	}
}
