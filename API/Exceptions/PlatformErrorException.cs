namespace GLFW_CS.Exceptions {
	public class PlatformErrorException : Exception {
		public PlatformErrorException() : base() { }

		public PlatformErrorException(string message) : base(message) { }

		public PlatformErrorException(string message, Exception innerException) : base(message, innerException) { }
	}
}
