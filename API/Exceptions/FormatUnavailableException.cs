namespace GLFW.Exceptions {
	public class FormatUnavailableException : Exception {
		public FormatUnavailableException() : base() { }

		public FormatUnavailableException(string message) : base(message) { }

		public FormatUnavailableException(string message, Exception innerException) : base(message, innerException) { }
	}
}
