using Dumpify;

namespace TEST {
	public static class Program {
		static void Main() {
			GLFW_CS.API.Init();

			Console.WriteLine($"Running GLFW {GLFW_CS.API.CurrentVersion}");

			var window = GLFW_CS.API.Window.Create(GLFW_CS.API.Window.Config.Default with { Hints = GLFW_CS.API.Window.CreationHints.Default with { Context_Version = new GLFW_CS.API.Version(3,3,0)} });

			window.KeyAction += (key, scancode, action, mods) => {
				if (key == GLFW_CS.Enums.Key.Escape) {
					window.Close();
				}
			};

			while (!window.ShouldClose) {
				// Render here

				window.SwapBuffers();
				GLFW_CS.API.PollEvents();
			}

			window.Destroy();
			GLFW_CS.API.Terminate();
		}
	}
}
