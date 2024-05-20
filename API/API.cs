using GLFW.Enums;
using GLFW.Exceptions;
using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;

namespace GLFW {
	public static class API {
		private static bool IsInitialized = false;

		/// <summary>
		/// Initializes the GLFW library
		/// </summary>
		/// <remarks>
		/// <para>
		/// This function must only be called from the main thread.
		/// </para>
		/// 
		/// <para>
		/// Before most GLFW functions can be used, GLFW must be initialized, and before an application terminates GLFW
		/// should be terminated in order to free any resources allocated during or after initialization.
		/// </para>
		///  
		/// <para>
		/// You should call <see cref="Terminate"/> before the application exits.
		/// </para>
		///  
		/// <para>
		/// Additional calls to this function after successful initialization but before termination will return immediately.
		/// </para>
		///  
		/// <para>
		/// @macOS: This function will change the current directory of the application to the `Contents/Resources` subdirectory of the application's bundle, if present.
		/// This can be disabled with the <see cref="InitHint.Cocoa_Chdir_Resources"/> init hint.
		/// </para>
		/// 
		/// <para>
		/// @macOS: This function will create the main menu and dock icon for the application.
		/// If GLFW finds a `MainMenu.nib` it is loaded and assumed to contain a menu bar.
		/// Otherwise a minimal menu bar is created manually with common commands like Hide, Quit and About.
		/// The About entry opens a minimal about dialog with information from the application's bundle.
		/// The menu bar and dock icon can be disabled entirely with the <see cref="InitHint.Cocoa_Menubar"/> init hint.
		/// </para>
		/// 
		/// <para>
		/// @Wayland @X11: If the library was compiled with support for both Wayland and X11, and the <see cref="InitHint.Platform"/> init hint is set to <see cref="Platform.Any"/>, 
		/// the `XDG_SESSION_TYPE` environment variable affects which platform is picked.
		/// If the environment variable is not set, or is set to something other than `wayland` or `x11`, 
		/// the regular detection mechanism will be used instead.
		/// </para>
		/// 
		/// <para>
		/// @X11: This function will set the `LC_CTYPE` category of the
		/// application locale according to the current environment if that category is
		/// still "C".  This is because the "C" locale breaks Unicode text input.
		/// </para>
		/// 
		/// </remarks>
		/// <param name="hints">
		/// Hints to initialize GLFW with
		/// </param>
		/// <exception cref="Exception"/>
		public static void Init(InitHints? hints = null) {
			if (IsInitialized) return;

			hints ??= InitHints.Default;
			hints.Set();

			if (Bindings.Init() <= 0) {
				throw new Exception("Failed to initialize GLFW");
			}

			Bindings.SetErrorCallback((type, message) => {
				var error = (ErrorType)type;
				throw error switch {
					ErrorType.Not_Initialized => new NotInitializedException(message),
					ErrorType.No_Current_Context => new NoCurrentContextException(message),
					ErrorType.Invalid_Enum => new InvalidEnumException(message),
					ErrorType.Invalid_Value => new InvalidValueException(message),
					ErrorType.Out_Of_Memory => new OutOfMemoryException(message),
					ErrorType.API_Unavailable => new ApiUnavailableException(message),
					ErrorType.Version_Unavailable => new VersionUnavailableException(message),
					ErrorType.Platform_Error => new PlatformErrorException(message),
					ErrorType.Format_Unavailable => new FormatUnavailableException(message),
					ErrorType.No_Window_Context => new NoWindowContextException(message),
					ErrorType.Cursor_Unavailable => new CursorUnavailableException(message),
					ErrorType.Feature_Unavailable => new FeatureUnavailableException(message),
					ErrorType.Feature_Unimplemented => new FeatureUnimplementedException(message),
					ErrorType.Platform_Unavailable => new PlatformUnavailableException(message),
					_ => new Exception(message),
				};
			});

			var monitors_ptrs_ptr = Bindings.GetMonitors(out int count);
			var monitors_ptrs = new nint[count];
			Marshal.Copy(monitors_ptrs_ptr, monitors_ptrs, 0, count);
			foreach (var monitor_ptr in monitors_ptrs) {
				monitors.Add(monitor_ptr, new Monitor(monitor_ptr));
			}

			Bindings.SetMonitorCallback(Monitor.Callback);

			IsInitialized = true;
		}

		public record InitHints {
			/// <summary>
			///  <para>Whether to also expose joystick hats as buttons, for compatibility with earlier versions of GLFW</para>
			/// </summary>
			public bool? Joystick_Hat_Buttons;

			/// <summary>
			/// <para>Specifies the platform type (rendering backend) to request when using OpenGL ES and EGL via <see href="https://chromium.googlesource.com/angle/angle/">ANGLE</see>. If the requested platform type is unavailable, ANGLE will use its default.</para>
			/// </summary>
			public AnglePlatformType? Angle_Platform_Type;

			/// <summary>
			/// <para>Specifies the platform to use for windowing and input</para>
			/// </summary>
			public Platform? Platform;

			/// <summary>
			/// <para>Platform Specific: macOS</para>
			/// <para>Specifies whether to set the current directory to the application to the Contents/Resources subdirectory of the application's bundle, if present</para>
			/// </summary>
			public bool? Cocoa_Chdir_Resources;

			/// <summary>
			/// <para>Platform Specific: macOS</para>
			/// <para>Specifies whether to create the menu bar and dock icon when GLFW is initialized. This applies whether the menu bar is created from a nib or manually by GLFW</para>
			/// </summary>
			public bool? Cocoa_Menubar;

			/// <summary>
			/// <para>Platform Specific: Wayland</para>
			/// <para>Specifies whether to use <see href="https://gitlab.freedesktop.org/libdecor/libdecor">libdecor</see> for window decorations where available</para>
			/// </summary>
			public WaylandLibDecor? Wayland_LibDecor;

			/// <summary>
			/// <para>Platform Specific: X11</para>
			/// <para>Specifies whether to prefer the VK_KHR_xcb_surface extension for creating Vulkan surfaces, or whether to use the VK_KHR_xlib_surface extension</para>
			/// </summary>
			public bool? X11_XCB_Vulkan_Surface;

			public static readonly InitHints Default = new();

			public void Set() {
				if (Joystick_Hat_Buttons is not null) {
					Bindings.InitHint((int)InitHint.Joystick_Hat_Buttons, Joystick_Hat_Buttons.Value ? 1 : 0);
				}
				if (Platform is not null) {
					Bindings.InitHint((int)InitHint.Platform, (int)Platform);
				}
				if (Angle_Platform_Type is not null) {
					Bindings.InitHint((int)InitHint.Angle_Platform_Type, (int)Angle_Platform_Type);
				}
				if (Cocoa_Chdir_Resources is not null) {
					Bindings.InitHint((int)InitHint.Cocoa_Chdir_Resources, Cocoa_Chdir_Resources.Value ? 1 : 0);
				}
				if (Cocoa_Menubar is not null) {
					Bindings.InitHint((int)InitHint.Cocoa_Menubar, Cocoa_Menubar.Value ? 1 : 0);
				}
				if (Wayland_LibDecor is not null) {
					Bindings.InitHint((int)InitHint.Wayland_LibDecor, (int)Wayland_LibDecor);
				}
				if (X11_XCB_Vulkan_Surface is not null) {
					Bindings.InitHint((int)InitHint.X11_XCB_Vulkan_Surface, X11_XCB_Vulkan_Surface.Value ? 1 : 0);
				}
			}
		}

		public static void Terminate() {
			Bindings.Terminate();
		}

		public readonly struct Version_(int major, int minor, int revision) {
			public readonly int Major = major;
			public readonly int Minor = minor;
			public readonly int Revision = revision;

			public override string ToString() {
				return $"{Major}.{Minor}.{Revision}";
			}
		}

		public static Version_ Version {
			get {
				Bindings.GetVersion(out int major, out int minor, out int revision);
				return new Version_(major, minor, revision);
			}
		}

		public static string VersionString {
			get {
				var ptr = Bindings.GetVersionString();
				return Marshal.PtrToStringAnsi(ptr) ?? string.Empty;
			}
		}

		public readonly struct Error_ {
			public required ErrorType Type { get; init; }
			public required string Message { get; init; }
		}

		public static Platform Platform => (Platform)Bindings.GetPlatform();

		public static bool CheckSupported(this Platform platform) {
			return Bindings.IsPlatformSupported((int)platform) > 0;
		}

		public static string Clipboard {
			get {
				return Bindings.GetClipboardString(0);
			}
			set {
				Bindings.SetClipboardString(0, value);
			}
		}

		public static double Time {
			get {
				return Bindings.GetTime();
			}
			set {
				Bindings.SetTime(value);
			}
		}

		public static ulong TimerValue => Bindings.GetTimerValue();

		public static ulong TimerFrequency => Bindings.GetTimerFrequency();

		public static void SetSwapInterval(int interval) {
			Bindings.SwapInterval(interval);
		}

		public static bool IsExtensionSupported(string extension) {
			var ptr = Marshal.StringToHGlobalAnsi(extension);

			return Bindings.IsExtensionSupported(ptr) > 0;
		}

		public static nint GetProcAddress(string procname) {
			var ptr = Marshal.StringToHGlobalAnsi(procname);

			return Bindings.GetProcAddress(ptr);
		}

		public static bool IsVulkanSupported => Bindings.IsVulkanSupported() > 0;

		public static IEnumerable<string?> GetRequiredInstanceExtensions() {
			var exts_ptrs_ptr = Bindings.GetRequiredInstanceExtensions(out uint count);

			var exts_ptrs = Utility.Marshal_From_Unmanaged_Array<nint>(exts_ptrs_ptr, (int)count);

			var extensions = exts_ptrs.Select(x => Marshal.PtrToStringUTF8(x)) ?? [];

			return extensions;
		}

		[StructLayout(LayoutKind.Sequential)]
		public readonly struct Image {
			/// <summary>
			/// The width, in pixels, of this image.
			/// </summary>
			public readonly int Width;

			/// <summary>
			/// The height, in pixels, of this image.
			/// </summary>
			public readonly int Height;

			/// <summary>
			/// The pixel data of this image, arranged left-to-right, top-to-bottom.
			/// </summary>
			public readonly byte[] Pixels;
		}

		#region Monitors
		private static readonly Dictionary<nint, Monitor> monitors = [];

		public static IEnumerable<Monitor> Monitors => monitors.Values;

		public class Monitor {
			internal readonly nint Pointer;

			public Monitor(nint ptr) {
				Pointer = ptr;

				var video_modes_ptr = Bindings.GetVideoModes(Pointer, out int video_modes_count);

				int structSize = Marshal.SizeOf<VideoMode>();

				for (int i = 0; i < video_modes_count; i++) {
					nint video_mode_ptr = nint.Add(video_modes_ptr, i * structSize);
					var video_mode = Marshal.PtrToStructure<VideoMode>(video_mode_ptr);

					video_modes.Add(video_mode_ptr, video_mode);
				}
			}

			public static Monitor Primary {
				get {
					var primary_monitor_ptr = Bindings.GetPrimaryMonitor();
					if (monitors.TryGetValue(primary_monitor_ptr, out Monitor? monitor)) {
						return monitor;
					}
					else throw new Exception();
				}
			}

			public string Name => Bindings.GetMonitorName(Pointer);

			public Vector2 Position {
				get {
					Bindings.GetMonitorPosition(Pointer, out int xpos, out int ypos);
					return new Vector2(xpos, ypos);
				}
			}

			public Rectangle Workarea {
				get {
					Bindings.GetMonitorWorkarea(Pointer, out int xpos, out int ypos, out int width, out int height);
					return new Rectangle(xpos, ypos, width, height);
				}
			}

			public Vector2 PhysicalSize {
				get {
					Bindings.GetMonitorPhysicalSize(Pointer, out int widthMM, out int heightMM);
					return new Vector2(widthMM, heightMM);
				}
			}

			public Vector2 ContentScale {
				get {
					Bindings.GetMonitorPhysicalSize(Pointer, out int widthMM, out int heightMM);
					return new Vector2(widthMM, heightMM);
				}
			}

			public VideoMode CurrentVideoMode {
				get {
					var video_mode_ptr = Bindings.GetVideoMode(Pointer);

					return Marshal.PtrToStructure<VideoMode>(video_mode_ptr);
				}
			}

			[StructLayout(LayoutKind.Sequential)]
			public readonly struct VideoMode {
				/// <summary>
				/// The width, in screen coordinates, of the video mode.
				/// </summary>
				public readonly int Width;

				/// <summary>
				/// The height, in screen coordinates, of the video mode.
				/// </summary>
				public readonly int Height;

				/// <summary>
				/// The bit depth of the red channel of the video mode.
				/// </summary>
				public readonly int Red_Bits;

				/// <summary>
				/// The bit depth of the green channel of the video mode.
				/// </summary>
				public readonly int Green_Bits;

				/// <summary>
				/// The bit depth of the blue channel of the video mode.
				/// </summary>
				public readonly int Blue_Bits;

				/// <summary>
				/// The refresh rate, in Hz, of the video mode.
				/// </summary>
				public readonly int Refresh_Rate;
			}

			private readonly Dictionary<nint, VideoMode> video_modes = [];

			public List<VideoMode> Video_Modes => [.. video_modes.Values];

			public nint UserPointer {
				get {
					return Bindings.GetMonitorUserPointer(Pointer);
				}
				set {
					Bindings.SetMonitorUserPointer(Pointer, value);
				}
			}

			public void SetGamma(float gamma) {
				Bindings.SetGamma(Pointer, gamma);
			}

			[StructLayout(LayoutKind.Sequential)]
			public readonly struct GammaRamp_(ushort[] red, ushort[] green, ushort[] blue) {
				public readonly ushort[] red = red;
				public readonly ushort[] green = green;
				public readonly ushort[] blue = blue;
			}

			[StructLayout(LayoutKind.Sequential)]
			private readonly struct GammaRampIntermediary_(nint red, nint green, nint blue, uint size) {
				public readonly nint red = red;
				public readonly nint green = green;
				public readonly nint blue = blue;
				public readonly uint size = size;
			}

			public GammaRamp_ GammaRamp {
				get {
					var intermediary = Marshal.PtrToStructure<GammaRampIntermediary_>(Bindings.GetGammaRamp(Pointer));

					int size = (int)intermediary.size;

					var red = Utility.Marshal_From_Unmanaged_Array<ushort>(intermediary.red, size);

					var green = Utility.Marshal_From_Unmanaged_Array<ushort>(intermediary.green, size);

					var blue = Utility.Marshal_From_Unmanaged_Array<ushort>(intermediary.blue, size);

					return new GammaRamp_(red, green, blue);
				}
				set {
					int size = value.red.Length;

					if (size != value.green.Length || size != value.blue.Length) {
						throw new ArgumentException("All color arrays must have the same length.");
					}

					nint red_ptr = Utility.Marshal_To_Unmanaged_Array(value.red);
					nint green_ptr = Utility.Marshal_To_Unmanaged_Array(value.green);
					nint blue_ptr = Utility.Marshal_To_Unmanaged_Array(value.blue);

					var intermediary = new GammaRampIntermediary_(red_ptr, green_ptr, blue_ptr, (uint)size);

					GCHandle handle = GCHandle.Alloc(intermediary, GCHandleType.Pinned);
					try {
						nint intermediary_ptr = handle.AddrOfPinnedObject();

						Bindings.SetGammaRamp(Pointer, intermediary_ptr);
					}
					finally {
						handle.Free();
						Marshal.FreeHGlobal(red_ptr);
						Marshal.FreeHGlobal(green_ptr);
						Marshal.FreeHGlobal(blue_ptr);
					}
				}
			}

			
			public static event Action<Monitor>? Connected;
			public static event Action<Monitor>? Disonnected;

			internal static Bindings.MonitorCallbackDelegate Callback = (monitor_ptr, @event) => {
				if (@event == (int)DeviceEvent.Connected) {
					var monitor = new Monitor(monitor_ptr);
					monitors.Add(monitor_ptr, monitor);
					Connected?.Invoke(monitor);
				}
				else {
					if (monitors.TryGetValue(monitor_ptr, out Monitor? monitor)) {
						monitors.Remove(monitor_ptr);
						Disonnected?.Invoke(monitor);
					}
					else throw new Exception();
				}
			};
		}
		#endregion

		#region Windows
		private static readonly Dictionary<nint, Window> windows = [];

		public static IEnumerable<Window> Windows => windows.Values;

		public class Window {
			internal readonly nint Pointer;

			public Window(nint ptr) {
				Pointer = ptr;

				Bindings.SetWindowMovedCallback(Pointer, (window_ptr, xpos, ypos) => {
					windows[window_ptr].Moved?.Invoke(new Vector2(xpos, ypos));
				});

				Bindings.SetWindowResizedCallback(Pointer, (window_ptr, width, height) => {
					windows[window_ptr].Resized?.Invoke(new Vector2(width, height));
				});

				Bindings.SetWindowClosedCallback(Pointer, (window_ptr) => {
					windows[window_ptr].Closed?.Invoke();
				});

				Bindings.SetWindowRefreshedCallback(Pointer, (window_ptr) => {
					windows[window_ptr].Refreshed?.Invoke();
				});

				Bindings.SetWindowFocusChangedCallback(Pointer, (window_ptr, focused) => {
					if (focused > 0) {
						windows[window_ptr].FocusGained?.Invoke();
					} else {
						windows[window_ptr].FocusLost?.Invoke();
					}

				});

				Bindings.SetWindowMaximizedChangedCallback(Pointer, (window_ptr, maximized) => {
					windows[window_ptr].MaximizedChanged?.Invoke(maximized > 0);
				});

				Bindings.SetWindowIconifiedChangedCallback(Pointer, (window_ptr, minimized) => {
					windows[window_ptr].MinimizedChanged?.Invoke(minimized > 0);
				});

				Bindings.SetFramebufferResizedCallback(Pointer, (window_ptr, width, height) => {
					windows[window_ptr].FramebufferResized?.Invoke(new Vector2(width, height));
				});

				Bindings.SetWindowContentScaleChangedCallback(Pointer, (window_ptr, xscale, yscale) => {
					windows[window_ptr].ContentScaleChanged?.Invoke(new Vector2(xscale, yscale));
				});

				Bindings.SetKeyCallback(Pointer, (window_ptr, key, scancode, action, modifiers) => {
					windows[window_ptr].KeyAction?.Invoke((Key)key, scancode, (InputAction)action, (KeyModifierFlags)modifiers);
				});

				Bindings.SetCharCallback(Pointer, (window_ptr, codepoint) => {
					windows[window_ptr].CharInput?.Invoke((char)codepoint);
				});

				Bindings.SetCharModifiersCallback(Pointer, (window_ptr, codepoint, modifiers) => {
					windows[window_ptr].CharModifiersInput?.Invoke((char)codepoint, (KeyModifierFlags)modifiers);
				});

				Bindings.SetMouseButtonCallback(Pointer, (window_ptr, button, action, modifiers) => {
					windows[window_ptr].MouseButtonAction?.Invoke((MouseButton)button, (InputAction)action, (KeyModifierFlags)modifiers);
				});

				Bindings.SetCursorMovedCallback(Pointer, (window_ptr, xpos, ypos) => {
					windows[window_ptr].CursorMoved?.Invoke(new Vector2((float)xpos, (float)ypos));
				});

				Bindings.SetCursorEnterCallback(Pointer, (window_ptr, entered) => {
					if (entered > 0) {
						windows[window_ptr].CursorEntered?.Invoke();
					} else {
						windows[window_ptr].CursorExited?.Invoke();
					}
				});

				Bindings.SetScrolledCallback(Pointer, (window_ptr, xoffset, yoffset) => {
					windows[window_ptr].Scrolled?.Invoke(new Vector2((float)xoffset, (float)yoffset));
				});

				Bindings.SetDroppedCallback(Pointer, (window_ptr, path_count, path_ptrs_ptr) => {
					var path_ptrs = Utility.Marshal_From_Unmanaged_Array<nint>(path_ptrs_ptr, path_count);

					var paths = path_ptrs.Select(x => Marshal.PtrToStringUTF8(x)) ?? [];

					windows[window_ptr].PathsDropped?.Invoke(paths);
				});

				windows.Add(ptr, this);
			}

			public static Window Create(Config? config = null) {
				config ??= Config.Default;

				config.Size ??= new Vector2(800, 600);

				if (config.Size.Value.X <= 0 || config.Size.Value.Y <= 0) throw new Exception();

				config.Hints ??= CreationHints.Default;
				config.Hints.Set();

				var window_ptr = Bindings.CreateWindow((int)config.Size.Value.X, (int)config.Size.Value.Y, config.Title ?? "Unnamed Window", nint.Zero, config.SharedContext?.Pointer ?? nint.Zero);

				if (window_ptr == nint.Zero) {
					throw new Exception();
				}

				var window = new Window(window_ptr);

				return window;
			}

			public record Config {
				public Vector2? Size;
				public string? Title;
				public Window? SharedContext;
				public CreationHints? Hints;

				public static readonly Config Default = new();
			}

			public static Window CreateFullscreen(FullscreenConfig? config = null) {
				config ??= FullscreenConfig.Default;

				config.Monitor ??= Monitor.Primary;

				config.Hints ??= CreationHints.Default;
				config.Hints.Set();

				var window_ptr = Bindings.CreateWindow(config.Monitor.CurrentVideoMode.Width, config.Monitor.CurrentVideoMode.Height, config.Title ?? "Unnamed Window", config.Monitor.Pointer, config.SharedContext?.Pointer ?? nint.Zero);

				if (window_ptr == nint.Zero) {
					throw new Exception();
				}

				var window = new Window(window_ptr);

				return window;
			}

			public record FullscreenConfig {
				public Monitor? Monitor;
				public string? Title;
				public Window? SharedContext;
				public CreationHints? Hints;

				public static readonly FullscreenConfig Default = new();
			}

			public record CreationHints {
				public bool? Focused;
				public bool? Resizable;
				public bool? Visible;
				public bool? Decorated;
				public bool? Auto_Iconify;
				public bool? Floating;
				public bool? Maximized;
				public bool? Center_Cursor;
				public bool? Transparent_Framebuffer;
				public bool? Focus_On_Show;
				public bool? Mouse_Passthrough;
				public Vector2? Position;

				public int? Red_Bits;
				public int? Green_Bits;
				public int? Blue_Bits;
				public int? Alpha_Bits;
				public int? Depth_Bits;
				public int? Stencil_Bits;
				public int? Accum_Red_Bits;
				public int? Accum_Green_Bits;
				public int? Accum_Blue_Bits;
				public int? Accum_Alpha_Bits;
				public bool? Stereo;
				public int? Samples;
				public bool? sRGB_Capable;
				public int? Refresh_Rate;
				public bool? Double_Buffer;

				public ClientAPI? Client_API;
				public Version_? Context_Version;
				public Robustness? Context_Robustness;
				public bool? Context_Debug;
				public bool? Forward_Compatibility;
				public OpenGLProfile? OpenGL_Profile;
				public ReleaseBehaviour? Context_Release_Behaviour;
				public bool? Context_No_Error;
				public ContextAPI? Context_Creation_API;
				public bool? Scale_To_Monitor;
				public bool? Scale_Framebuffer;

				public string? Cocoa_Frame_Name;
				public bool? Cocoa_Graphics_Switching;

				public string? X11_Class_Name;
				public string? X11_Instance_Name;

				public bool? Win32_Keyboard_Menu;
				public bool? Win32_Show_Default;

				public string? Wayland_App_ID;


				public static readonly CreationHints Default = new();

				public void Set() {
					#region General
					if (Focused is not null) {
						Bindings.SetWindowHint((int)WindowHint.Focused, Focused.Value ? 1 : 0);
					}
					if (Resizable is not null) {
						Bindings.SetWindowHint((int)WindowHint.Resizable, Resizable.Value ? 1 : 0);
					}
					if (Visible is not null) {
						Bindings.SetWindowHint((int)WindowHint.Visible, Visible.Value ? 1 : 0);
					}
					if (Decorated is not null) {
						Bindings.SetWindowHint((int)WindowHint.Decorated, Decorated.Value ? 1 : 0);
					}
					if (Auto_Iconify is not null) {
						Bindings.SetWindowHint((int)WindowHint.Auto_Iconify, Auto_Iconify.Value ? 1 : 0);
					}
					if (Floating is not null) {
						Bindings.SetWindowHint((int)WindowHint.Floating, Floating.Value ? 1 : 0);
					}
					if (Maximized is not null) {
						Bindings.SetWindowHint((int)WindowHint.Maximized, Maximized.Value ? 1 : 0);
					}
					if (Maximized is not null) {
						Bindings.SetWindowHint((int)WindowHint.Maximized, Maximized.Value ? 1 : 0);
					}
					if (Center_Cursor is not null) {
						Bindings.SetWindowHint((int)WindowHint.Center_Cursor, Center_Cursor.Value ? 1 : 0);
					}
					if (Transparent_Framebuffer is not null) {
						Bindings.SetWindowHint((int)WindowHint.Transparent_Framebuffer, Transparent_Framebuffer.Value ? 1 : 0);
					}
					if (Focus_On_Show is not null) {
						Bindings.SetWindowHint((int)WindowHint.Focus_On_Show, Focus_On_Show.Value ? 1 : 0);
					}
					if (Mouse_Passthrough is not null) {
						Bindings.SetWindowHint((int)WindowHint.Mouse_Passthrough, Mouse_Passthrough.Value ? 1 : 0);
					}
					if (Position is not null) {
						Bindings.SetWindowHint((int)WindowHint.Position_X, (int)Position.Value.X);
						Bindings.SetWindowHint((int)WindowHint.Position_Y, (int)Position.Value.Y);
					}
					#endregion

					#region Framebuffer
					if (Red_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Red_Bits, Red_Bits.Value);
					}
					if (Green_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Green_Bits, Green_Bits.Value);
					}
					if (Blue_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Blue_Bits, Blue_Bits.Value);
					}
					if (Alpha_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Alpha_Bits, Alpha_Bits.Value);
					}
					if (Depth_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Depth_Bits, Depth_Bits.Value);
					}
					if (Stencil_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Stencil_Bits, Stencil_Bits.Value);
					}
					if (Accum_Red_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Accum_Red_Bits, Accum_Red_Bits.Value);
					}
					if (Accum_Green_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Accum_Green_Bits, Accum_Green_Bits.Value);
					}
					if (Accum_Blue_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Accum_Blue_Bits, Accum_Blue_Bits.Value);
					}
					if (Accum_Alpha_Bits is not null) {
						Bindings.SetWindowHint((int)WindowHint.Accum_Alpha_Bits, Accum_Alpha_Bits.Value);
					}
					if (Stereo is not null) {
						Bindings.SetWindowHint((int)WindowHint.Stereo, Stereo.Value ? 1 : 0);
					}
					if (Samples is not null) {
						Bindings.SetWindowHint((int)WindowHint.Samples, Samples.Value);
					}
					if (sRGB_Capable is not null) {
						Bindings.SetWindowHint((int)WindowHint.sRGB_Capable, sRGB_Capable.Value ? 1 : 0);
					}
					if (Refresh_Rate is not null) {
						Bindings.SetWindowHint((int)WindowHint.Refresh_Rate, Refresh_Rate.Value);
					}
					if (Double_Buffer is not null) {
						Bindings.SetWindowHint((int)WindowHint.Double_Buffer, Double_Buffer.Value ? 1 : 0);
					}
					#endregion

					#region Context
					if (Client_API is not null) {
						Bindings.SetWindowHint((int)WindowHint.Client_API, (int)Client_API.Value);
					}
					if (Context_Version is not null) {
						Bindings.SetWindowHint((int)WindowHint.Context_Major_Version, Context_Version.Value.Major);
						Bindings.SetWindowHint((int)WindowHint.Context_Minor_Version, Context_Version.Value.Minor);
						Bindings.SetWindowHint((int)WindowHint.Context_Revision, Context_Version.Value.Revision);
					}
					if (Context_Robustness is not null) {
						Bindings.SetWindowHint((int)WindowHint.Context_Robustness, (int)Context_Robustness.Value);
					}
					if (Context_Debug is not null) {
						Bindings.SetWindowHint((int)WindowHint.Context_Debug, Context_Debug.Value ? 1 : 0);
					}
					if (Forward_Compatibility is not null) {
						Bindings.SetWindowHint((int)WindowHint.Forward_Compatibility, Forward_Compatibility.Value ? 1 : 0);
					}
					if (OpenGL_Profile is not null) {
						Bindings.SetWindowHint((int)WindowHint.OpenGL_Profile, (int)OpenGL_Profile.Value);
					}
					if (Context_Release_Behaviour is not null) {
						Bindings.SetWindowHint((int)WindowHint.Context_Release_Behaviour, (int)Context_Release_Behaviour.Value);
					}
					if (Context_No_Error is not null) {
						Bindings.SetWindowHint((int)WindowHint.Context_No_Error, Context_No_Error.Value ? 1 : 0);
					}
					if (Context_Creation_API is not null) {
						Bindings.SetWindowHint((int)WindowHint.Context_Creation_API, (int)Context_Creation_API.Value);
					}
					if (Scale_To_Monitor is not null) {
						Bindings.SetWindowHint((int)WindowHint.Scale_To_Monitor, Scale_To_Monitor.Value ? 1 : 0);
					}
					if (Scale_Framebuffer is not null) {
						Bindings.SetWindowHint((int)WindowHint.Scale_Framebuffer, Scale_Framebuffer.Value ? 1 : 0);
					}
					#endregion

					#region Platform Specific
					if (Cocoa_Frame_Name is not null) {
						var ptr = Utility.Marshal_String_To_Unmanaged_UTF8(Cocoa_Frame_Name);

						Bindings.SetWindowHintString((int)WindowHint.Cocoa_Frame_Name, ptr);
					}
					if (Cocoa_Graphics_Switching is not null) {
						Bindings.SetWindowHint((int)WindowHint.Cocoa_Graphics_Switching, Cocoa_Graphics_Switching.Value ? 1 : 0);
					}

					if (X11_Class_Name is not null) {
						var ptr = Marshal.StringToHGlobalAnsi(X11_Class_Name);

						Bindings.SetWindowHintString((int)WindowHint.X11_Class_Name, ptr);
					}
					if (X11_Instance_Name is not null) {
						var ptr = Marshal.StringToHGlobalAnsi(X11_Instance_Name);

						Bindings.SetWindowHintString((int)WindowHint.X11_Instance_Name, ptr);
					}

					if (Win32_Keyboard_Menu is not null) {
						Bindings.SetWindowHint((int)WindowHint.Win32_Keyboard_Menu, Win32_Keyboard_Menu.Value ? 1 : 0);
					}
					if (Win32_Show_Default is not null) {
						Bindings.SetWindowHint((int)WindowHint.Win32_Show_Default, Win32_Show_Default.Value ? 1 : 0);
					}

					if (Wayland_App_ID is not null) {
						var ptr = Marshal.StringToHGlobalAnsi(Wayland_App_ID);

						Bindings.SetWindowHintString((int)WindowHint.Wayland_App_ID, ptr);
					}
					#endregion
				}

				public static void Reset() {
					Bindings.ResetWindowHints();
				}
			}

			public void Close() {
				ShouldClose = true;
			}

			public bool ShouldClose {
				get {
					return Bindings.GetWindowShouldClose(Pointer) > 0;
				}
				set {
					Bindings.SetWindowShouldClose(Pointer, value ? 1 : 0);
				}
			}

			public void Destroy() {
				Bindings.DestroyWindow(Pointer);
			}

			public void SwapBuffers() {
				Bindings.SwapBuffers(Pointer);
			}

			public string Title {
				get {
					return Bindings.GetWindowTitle(Pointer);
				}
				set {
					Bindings.SetWindowTitle(Pointer, value);
				}
			}

			public void SetIcon(IEnumerable<Image> images) {
				var handle = GCHandle.Alloc(images, GCHandleType.Pinned);

				Bindings.SetWindowIcon(Pointer, images.Count(), handle.AddrOfPinnedObject());

				handle.Free();
			}

			public void ResetIcon() {
				Bindings.SetWindowIcon(Pointer, 0, nint.Zero);
			}

			public Vector2 Size {
				get {
					Bindings.GetWindowSize(Pointer, out int width, out int height);
					return new Vector2(width, height);
				}
				set {
					Bindings.SetWindowSize(Pointer, (int)value.X, (int)value.Y);
				}
			}

			public void SetSizeLimits(Vector2 min, Vector2 max) {
				Bindings.SetWindowSizeLimits(Pointer, (int)min.X, (int)min.Y, (int)max.X, (int)max.Y);
			}

			public void SetAspectRatio(int numerator, int denominator) {
				Bindings.SetWindowAspectRatio(Pointer, numerator, denominator);
			}

			public Vector2 FramebufferSize {
				get {
					Bindings.GetFramebufferSize(Pointer, out int width, out int height);
					return new Vector2(width, height);
				}
			}

			public Vector4 FrameSize {
				get {
					Bindings.GetWindowFrameSize(Pointer, out int left, out int top, out int right, out int bottom);
					return new Vector4(left, top, right, bottom);
				}
			}

			public Vector2 ContentScale {
				get {
					Bindings.GetWindowContentScale(Pointer, out float xscale, out float yscale);
					return new Vector2(xscale, yscale);
				}
			}

			public float Opacity {
				get => Bindings.GetWindowOpacity(Pointer);
				set => Bindings.SetWindowOpacity(Pointer, value);
			}

			public void Minimize() {
				Bindings.IconifyWindow(Pointer);
			}

			public void Restore() {
				Bindings.RestoreWindow(Pointer);
			}

			public void Maximize() {
				Bindings.MaximizeWindow(Pointer);
			}

			public void Show() {
				Bindings.ShowWindow(Pointer);
			}

			public void Hide() {
				Bindings.HideWindow(Pointer);
			}

			public void Focus() {
				Bindings.FocusWindow(Pointer);
			}

			public void RequestAttention() {
				Bindings.RequestWindowAttention(Pointer);
			}

			public Monitor? FullscreenMonitor {
				get {
					var monitor_ptr = Bindings.GetWindowMonitor(Pointer);
					if (monitor_ptr == nint.Zero) {
						return null;
					}
					return monitors[monitor_ptr];
				}
			}

			public bool IsFullscreen => FullscreenMonitor is not null;

			public void Fullscreen(Monitor? monitor = null, Rectangle? rect = null, int? refresh_rate = null) {
				monitor ??= Monitor.Primary;
				Rectangle actual_rect = rect ?? new Rectangle(0, 0, monitor.CurrentVideoMode.Width, monitor.CurrentVideoMode.Height);

				Bindings.SetWindowMonitor(Pointer, monitor.Pointer, actual_rect.X, actual_rect.Y, actual_rect.Width, actual_rect.Height, refresh_rate ?? -1);
			}

			#region Attributes
			public bool Iconified {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Iconified) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Iconified, value ? 1 : 0);
				}
			}

			public bool Hovered {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Hovered) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Hovered, value ? 1 : 0);
				}
			}

			public bool Focused {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Focused) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Focused, value ? 1 : 0);
				}
			}

			public bool Visible {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Visible) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Visible, value ? 1 : 0);
				}
			}

			public bool Resizable {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Resizable) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Resizable, value ? 1 : 0);
				}
			}

			public bool Decorated {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Decorated) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Decorated, value ? 1 : 0);
				}
			}

			public bool Auto_Iconify {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Auto_Iconify) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Auto_Iconify, value ? 1 : 0);
				}
			}

			public bool Floating {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Floating) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Floating, value ? 1 : 0);
				}
			}

			public bool Maximized {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Maximized) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Maximized, value ? 1 : 0);
				}
			}

			public bool Transparent_Framebuffer {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Transparent_Framebuffer) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Transparent_Framebuffer, value ? 1 : 0);
				}
			}

			public bool Focus_On_Show {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Focus_On_Show) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Focus_On_Show, value ? 1 : 0);
				}
			}

			public bool Mouse_Passthrough {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Mouse_Passthrough) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Mouse_Passthrough, value ? 1 : 0);
				}
			}

			public bool Double_Buffer {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Double_Buffer) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Double_Buffer, value ? 1 : 0);
				}
			}

			public ClientAPI Client_Api {
				get {
					return (ClientAPI)Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Client_Api);
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Client_Api, (int)value);
				}
			}

			public Version_ Context_Version {
				get {
					var major = Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Major_Version);
					var minor = Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Minor_Version);
					var revision = Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Revision);

					return new(major, minor, revision);
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Major_Version, value.Major);
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Minor_Version, value.Minor);
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Revision, value.Revision);
				}
			}

			public Robustness Context_Robustness {
				get {
					return (Robustness)Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Robustness);
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Robustness, (int)value);
				}
			}

			public bool Context_Debug {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Debug) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Debug, value ? 1 : 0);
				}
			}

			public bool Forward_Compatibility {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Forward_Compatibility) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Forward_Compatibility, value ? 1 : 0);
				}
			}

			public OpenGLProfile OpenGL_Profile {
				get {
					return (OpenGLProfile)Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.OpenGL_Profile);
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.OpenGL_Profile, (int)value);
				}
			}

			public ReleaseBehaviour Context_Release_Behaviour {
				get {
					return (ReleaseBehaviour)Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Release_Behaviour);
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Release_Behaviour, (int)value);
				}
			}

			public bool Context_No_Error {
				get {
					return Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_No_Error) > 0;
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_No_Error, value ? 1 : 0);
				}
			}

			public ContextAPI Context_Creation_API {
				get {
					return (ContextAPI)Bindings.GetWindowAttribute(Pointer, (int)WindowAttribute.Context_Creation_API);
				}
				set {
					Bindings.SetWindowAttribute(Pointer, (int)WindowAttribute.Context_Creation_API, (int)value);
				}
			}
			#endregion

			public nint UserPointer {
				get {
					return Bindings.GetWindowUserPointer(Pointer);
				}
				set {
					Bindings.SetWindowUserPointer(Pointer, value);
				}
			}

			public event Action<Vector2>? Moved;

			public event Action<Vector2>? Resized;

			public event Action? Closed;

			public event Action? Refreshed;

			public event Action? FocusGained;
			public event Action? FocusLost;

			public event Action<bool>? MaximizedChanged;

			public event Action<bool>? MinimizedChanged;

			public event Action<Vector2>? FramebufferResized;

			public event Action<Vector2>? ContentScaleChanged;

			public event Action<Key, int, InputAction, KeyModifierFlags>? KeyAction;

			public event Action<char>? CharInput;

			public event Action<char, KeyModifierFlags>? CharModifiersInput;

			public event Action<MouseButton, InputAction, KeyModifierFlags>? MouseButtonAction;

			public event Action<Vector2>? CursorMoved;

			public event Action? CursorEntered;
			public event Action? CursorExited;

			public event Action<Vector2>? Scrolled;

			public event Action<IEnumerable<string?>>? PathsDropped;

			#region Input Modes
			public CursorState Cursor_State {
				get {
					return (CursorState)Bindings.GetInputMode(Pointer, (int)InputMode.Cursor);
				}
				set {
					Bindings.SetInputMode(Pointer, (int)InputMode.Cursor, (int)value);
				}
			}

			public bool Sticky_Keys {
				get {
					return Bindings.GetInputMode(Pointer, (int)InputMode.Sticky_Keys) > 0;
				}
				set {
					Bindings.SetInputMode(Pointer, (int)InputMode.Sticky_Keys, value ? 1 : 0);
				}
			}

			public bool Sticky_Mouse_Buttons {
				get {
					return Bindings.GetInputMode(Pointer, (int)InputMode.Sticky_Mouse_Buttons) > 0;
				}
				set {
					Bindings.SetInputMode(Pointer, (int)InputMode.Sticky_Mouse_Buttons, value ? 1 : 0);
				}
			}

			public bool Lock_Key_Modifiers {
				get {
					return Bindings.GetInputMode(Pointer, (int)InputMode.Lock_Key_Mods) > 0;
				}
				set {
					Bindings.SetInputMode(Pointer, (int)InputMode.Lock_Key_Mods, value ? 1 : 0);
				}
			}

			public bool Raw_Mouse_Motion {
				get {
					return Bindings.GetInputMode(Pointer, (int)InputMode.Raw_Mouse_Motion) > 0;
				}
				set {
					Bindings.SetInputMode(Pointer, (int)InputMode.Raw_Mouse_Motion, value ? 1 : 0);
				}
			}
			#endregion

			public InputAction GetKeyLastAction(Key key) {
				return (InputAction)Bindings.GetKeyLastAction(Pointer, (int)key);
			}

			public InputAction GetMouseButtonLastAction(MouseButton button) {
				return (InputAction)Bindings.GetMouseButtonLastAction(Pointer, (int)button);
			}

			public Vector2 CursorPosition {
				get {
					Bindings.GetCursorPosition(Pointer, out double xpos, out double ypos);
					return new Vector2((float)xpos, (float)ypos);
				}
				set {
					Bindings.SetCursorPosition(Pointer, value.X, value.Y);
				}
			}

			public void SetCursor(Cursor cursor) {
				Bindings.SetCursor(Pointer, cursor.Pointer);
			}

			public void MakeContextCurrent() {
				Bindings.MakeContextCurrent(Pointer);
			}

			public static Window GetCurrentContext() => windows[Bindings.GetCurrentContext()];
		}
		#endregion

		#region Input
		public static void PollEvents() {
			Bindings.PollEvents();
		}

		public static void WaitEvents() {
			Bindings.WaitEvents();
		}

		public static void WaitEvents(double timeout) {
			Bindings.WaitEventsTimeout(timeout);
		}

		public static void PostEmptyEvent() {
			Bindings.PostEmptyEvent();
		}

		public static bool IsRawMouseMotionSupported() {
			return Bindings.IsRawMouseMotionSupported() > 0;
		}

		public static string GetKeyName(Key key) {
			var key_int = (int)key;

			if (key_int <= 0) {
				throw new ArgumentException("Key must be > 0", nameof(key));
			}

			return Bindings.GetKeyName(key_int, 0);
		}

		public static string GetKeyNameScancode(int scancode) {
			return Bindings.GetKeyName((int)Key.Unknown, scancode);
		}

		public static int GetKeyScancode(Key key) {
			return Bindings.GetKeyScancode((int)key);
		}

		public class Cursor(nint pointer) {
			internal readonly nint Pointer = pointer;

			public void Destroy() {
				Bindings.DestroyWindow(Pointer);
			}

			public static Cursor CreateCustom(Image image, Vector2 hotspot) {
				GCHandle handle = GCHandle.Alloc(image, GCHandleType.Pinned);

				var cursor_ptr = Bindings.CreateCursor(handle.AddrOfPinnedObject(), hotspot.X, hotspot.Y);

				handle.Free();

				var cursor = new Cursor(cursor_ptr);

				return cursor;
			}

			public static Cursor CreateStandard(CursorShape shape) {
				var cursor_ptr = Bindings.CreateStandardCursor((int)shape);

				var cursor = new Cursor(cursor_ptr);

				return cursor;
			}
		}
		#endregion
	}
}
