namespace GLFW_CS.Exceptions {
	public class CursorUnavailableException : Exception {
		public CursorUnavailableException() : base() { }

		public CursorUnavailableException(string message) : base(message) { }

		public CursorUnavailableException(string message, Exception innerException) : base(message, innerException) { }
	}
}
