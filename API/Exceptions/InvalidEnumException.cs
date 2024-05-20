namespace GLFW.Exceptions {
	public class InvalidEnumException : Exception {
		public InvalidEnumException() : base() { }

		public InvalidEnumException(string message) : base(message) { }

		public InvalidEnumException(string message, Exception innerException) : base(message, innerException) { }
	}
}
