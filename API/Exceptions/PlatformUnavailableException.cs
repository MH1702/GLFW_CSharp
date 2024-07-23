namespace GLFW_CS.Exceptions {
	public class PlatformUnavailableException : Exception {
		public PlatformUnavailableException() : base() { }

		public PlatformUnavailableException(string message) : base(message) { }

		public PlatformUnavailableException(string message, Exception innerException) : base(message, innerException) { }
	}
}
