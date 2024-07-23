namespace GLFW_CS.Exceptions {
	public class FeatureUnavailableException : Exception {
		public FeatureUnavailableException() : base() { }

		public FeatureUnavailableException(string message) : base(message) { }

		public FeatureUnavailableException(string message, Exception innerException) : base(message, innerException) { }
	}
}
