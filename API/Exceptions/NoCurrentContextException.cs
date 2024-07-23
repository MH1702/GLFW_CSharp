namespace GLFW_CS.Exceptions {
	public class NoCurrentContextException : Exception {
		public NoCurrentContextException() : base() { }

		public NoCurrentContextException(string message) : base(message) { }

		public NoCurrentContextException(string message, Exception innerException) : base(message, innerException) { }
	}
}
