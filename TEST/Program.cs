using Dumpify;

namespace TEST {
	public static class Program {
		static void Main() {
			GLFW.API.Init();

			Console.WriteLine($"Running GLFW {GLFW.API.Version}");

			var window = GLFW.API.Window.Create();

			window.KeyAction += (key, scancode, action, mods) => {
				if (key == GLFW.Enums.Key.Escape) {
					window.Close();
				}
			};

			while (!window.ShouldClose) {
				// Render here

				window.SwapBuffers();
				GLFW.API.PollEvents();
			}

			window.Destroy();
			GLFW.API.Terminate();
		}
	}
}
