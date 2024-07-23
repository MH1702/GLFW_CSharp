namespace GLFW_CS.Exceptions {
	public class ApiUnavailableException : Exception {
		public ApiUnavailableException() : base() { }

		public ApiUnavailableException(string message) : base(message) { }

		public ApiUnavailableException(string message, Exception innerException) : base(message, innerException) { }
	}
}
