namespace GLFW.Exceptions {
	public class FeatureUnimplementedException : Exception {
		public FeatureUnimplementedException() : base() { }

		public FeatureUnimplementedException(string message) : base(message) { }

		public FeatureUnimplementedException(string message, Exception innerException) : base(message, innerException) { }
	}
}
