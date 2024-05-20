namespace GLFW.Exceptions {
	public class NoWindowContextException : Exception {
		public NoWindowContextException() : base() { }

		public NoWindowContextException(string message) : base(message) { }

		public NoWindowContextException(string message, Exception innerException) : base(message, innerException) { }
	}
}
