namespace GLFW_CS.Exceptions {
	public class VersionUnavailableException : Exception {
		public VersionUnavailableException() : base() { }

		public VersionUnavailableException(string message) : base(message) { }

		public VersionUnavailableException(string message, Exception innerException) : base(message, innerException) { }
	}
}
