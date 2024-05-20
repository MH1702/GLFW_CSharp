namespace GLFW.Exceptions {
	public class InvalidValueException : Exception {
		public InvalidValueException() : base() { }

		public InvalidValueException(string message) : base(message) { }

		public InvalidValueException(string message, Exception innerException) : base(message, innerException) { }
	}
}
