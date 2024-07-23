using System.Runtime.InteropServices;

namespace GLFW_CS {
	public static partial class Bindings {
		private const string DllName = "lib/glfw3";

		[LibraryImport(DllName, EntryPoint = "glfwInit", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int Init();

		[LibraryImport(DllName, EntryPoint = "glfwInitHint", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void InitHint(int hint, int value);

		[LibraryImport(DllName, EntryPoint = "glfwGetVersion", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetVersion(out int major, out int minor, out int revision);

		[LibraryImport(DllName, EntryPoint = "glfwGetVersionString", SetLastError = true)]
		[UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetVersionString();

		[LibraryImport(DllName, EntryPoint = "glfwGetError", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetError(out string message);

		public delegate void ErrorCallbackDelegate(int type, string message);

		[LibraryImport(DllName, EntryPoint = "glfwSetErrorCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial ErrorCallbackDelegate? SetErrorCallback(ErrorCallbackDelegate callback);

		[LibraryImport(DllName, EntryPoint = "glfwGetPlatform", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetPlatform();

		[LibraryImport(DllName, EntryPoint = "glfwPlatformSupported", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int CheckPlatformSupported(int platform);

		[LibraryImport(DllName, EntryPoint = "glfwCreateCursor", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint CreateCursor(nint image, double xhot, double yhot);

		[LibraryImport(DllName, EntryPoint = "glfwCreateStandardCursor", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint CreateStandardCursor(int shape);

		[LibraryImport(DllName, EntryPoint = "glfwTerminate", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void Terminate();

		[LibraryImport(DllName, EntryPoint = "glfwSetClipboardString", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial string GetClipboardString(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetClipboardString", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetClipboardString(nint window, string text);

		[LibraryImport(DllName, EntryPoint = "glfwGetTime", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial double GetTime();

		[LibraryImport(DllName, EntryPoint = "glfwSetTime", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetTime(double time);

		[LibraryImport(DllName, EntryPoint = "glfwGetTimerValue", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial ulong GetTimerValue();

		[LibraryImport(DllName, EntryPoint = "glfwGetTimerFrequency", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial ulong GetTimerFrequency();

		[LibraryImport(DllName, EntryPoint = "glfwSwapInterval", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SwapInterval(int interval);

		[LibraryImport(DllName, EntryPoint = "glfwExtensionSupported", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int CheckExtensionSupported(nint extension);

		[LibraryImport(DllName, EntryPoint = "glfwGetProcAddress", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetProcAddress(nint procname);

		[LibraryImport(DllName, EntryPoint = "glfwVulkanSupported", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int IsVulkanSupported();

		[LibraryImport(DllName, EntryPoint = "glfwGetRequiredInstanceExtensions", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetRequiredInstanceExtensions(out uint count);

		#region Input
		[LibraryImport(DllName, EntryPoint = "glfwPollEvents", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void PollEvents();

		[LibraryImport(DllName, EntryPoint = "glfwWaitEvents", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void WaitEvents();

		[LibraryImport(DllName, EntryPoint = "glfwWaitEventsTimeout", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void WaitEventsTimeout(double timeout);

		[LibraryImport(DllName, EntryPoint = "glfwPostEmptyEvent", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void PostEmptyEvent();

		[LibraryImport(DllName, EntryPoint = "glfwRawMouseMotionSupported", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int IsRawMouseMotionSupported();

		[LibraryImport(DllName, EntryPoint = "glfwGetKeyName", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial string GetKeyName(int key, int scancode);

		[LibraryImport(DllName, EntryPoint = "glfwGetKeyScancode", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetKeyScancode(int key);
		#endregion

		#region Monitor
		[LibraryImport(DllName, EntryPoint = "glfwGetMonitors", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetMonitors(out int count);

		[LibraryImport(DllName, EntryPoint = "glfwGetPrimaryMonitor", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetPrimaryMonitor();

		[LibraryImport(DllName, EntryPoint = "glfwGetMonitorWorkarea", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetMonitorWorkarea(nint monitor, out int xpos, out int ypos, out int width, out int height);

		[LibraryImport(DllName, EntryPoint = "glfwGetMonitorPhysicalSize", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetMonitorPhysicalSize(nint monitor, out int widthMM, out int heightMM);

		[LibraryImport(DllName, EntryPoint = "glfwGetMonitorContentScale", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetMonitorContentScale(nint monitor, out float xscale, out float yscale);

		[LibraryImport(DllName, EntryPoint = "glfwGetMonitorName", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial string GetMonitorName(nint monitor);

		[LibraryImport(DllName, EntryPoint = "glfwSetMonitorUserPointer", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetMonitorUserPointer(nint monitor, nint pointer);

		[LibraryImport(DllName, EntryPoint = "glfwGetMonitorUserPointer", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetMonitorUserPointer(nint monitor);

		public delegate void MonitorCallbackDelegate(nint monitor, int @event);

		[LibraryImport(DllName, EntryPoint = "glfwSetMonitorCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial MonitorCallbackDelegate? SetMonitorCallback(MonitorCallbackDelegate callback);

		[LibraryImport(DllName, EntryPoint = "glfwGetVideoModes", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetVideoModes(nint monitor, out int cout);

		[LibraryImport(DllName, EntryPoint = "glfwGetVideoMode", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetVideoMode(nint monitor);

		[LibraryImport(DllName, EntryPoint = "glfwGetMonitorPos", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetMonitorPosition(nint monitor, out int xpos, out int ypos);

		[LibraryImport(DllName, EntryPoint = "glfwSetGamma", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetGamma(nint monitor, float gamma);

		[LibraryImport(DllName, EntryPoint = "glfwGetGammaRamp", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetGammaRamp(nint monitor);

		[LibraryImport(DllName, EntryPoint = "glfwSetGammaRamp", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetGammaRamp(nint monitor, nint ramp);
		#endregion

		#region Window
		[LibraryImport(DllName, EntryPoint = "glfwDefaultWindowHints", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void ResetWindowHints();

		[LibraryImport(DllName, EntryPoint = "glfwWindowHint", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowHint(int hint, int value);

		[LibraryImport(DllName, EntryPoint = "glfwWindowHintString", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowHintString(int hint, nint value);

		[LibraryImport(DllName, EntryPoint = "glfwCreateWindow", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint CreateWindow(int width, int height, string title, nint monitor, nint share);

		[LibraryImport(DllName, EntryPoint = "glfwDestroyWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void DestroyWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwWindowShouldClose", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetWindowShouldClose(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowShouldClose", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowShouldClose(nint window, int value);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowTitle", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial string GetWindowTitle(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowTitle", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowTitle(nint window, string title);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowIcon", SetLastError = true, StringMarshalling = StringMarshalling.Utf8), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowIcon(nint window, int count, nint images);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowSize", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetWindowSize(nint window, out int width, out int height);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowSize", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowSize(nint window, int width, int height);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowSizeLimits", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowSizeLimits(nint window, int minwidth, int minheight, int maxwidth, int maxheight);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowAspectRatio", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowAspectRatio(nint window, int numerator, int denominator);

		[LibraryImport(DllName, EntryPoint = "glfwGetFramebufferSize", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetFramebufferSize(nint window, out int width, out int height);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowFrameSize", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetWindowFrameSize(nint window, out int left, out int top, out int right, out int bottom);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowContentScale", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetWindowContentScale(nint window, out float xscale, out float yscale);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowOpacity", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial float GetWindowOpacity(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowOpacity", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowOpacity(nint window, float opacity);

		[LibraryImport(DllName, EntryPoint = "glfwIconifyWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void IconifyWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwRestoreWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void RestoreWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwMaximizeWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void MaximizeWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwShowWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void ShowWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwHideWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void HideWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwFocusWindow", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void FocusWindow(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwRequestWindowAttention", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void RequestWindowAttention(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowMonitor", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetWindowMonitor(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowMonitor", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint SetWindowMonitor(nint window, nint monitor, int xpos, int ypos, int width, int height, int refresh_rate);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowAttrib", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetWindowAttribute(nint window, int attribute);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowAttrib", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowAttribute(nint window, int attribute, int value);

		[LibraryImport(DllName, EntryPoint = "glfwGetWindowUserPointer", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetWindowUserPointer(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwsetWindowUserPointer", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetWindowUserPointer(nint window, nint pointer);

		public delegate void WindowMovedCallbackDelegate(nint window, int xpos, int ypos);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowPosCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowMovedCallbackDelegate? SetWindowMovedCallback(nint window, WindowMovedCallbackDelegate callback);

		public delegate void WindowResizedCallbackDelegate(nint window, int width, int height);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowSizeCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowResizedCallbackDelegate? SetWindowResizedCallback(nint window, WindowResizedCallbackDelegate callback);

		public delegate void WindowClosedCallbackDelegate(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowCloseCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowClosedCallbackDelegate? SetWindowClosedCallback(nint window, WindowClosedCallbackDelegate callback);

		public delegate void WindowRefreshedCallbackDelegate(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowRefreshCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowRefreshedCallbackDelegate? SetWindowRefreshedCallback(nint window, WindowRefreshedCallbackDelegate callback);

		public delegate void WindowFocusChangedCallbackDelegate(nint window, int focused);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowFocusCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowFocusChangedCallbackDelegate? SetWindowFocusChangedCallback(nint window, WindowFocusChangedCallbackDelegate callback);

		public delegate void WindowIconifiedChangedCallbackDelegate(nint window, int iconified);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowIconifyCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowIconifiedChangedCallbackDelegate? SetWindowIconifiedChangedCallback(nint window, WindowIconifiedChangedCallbackDelegate callback);

		public delegate void WindowMaximizedChangedCallbackDelegate(nint window, int maximized);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowMaximizeCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowMaximizedChangedCallbackDelegate? SetWindowMaximizedChangedCallback(nint window, WindowMaximizedChangedCallbackDelegate callback);

		public delegate void FramebufferResizedCallbackDelegate(nint window, int width, int height);

		[LibraryImport(DllName, EntryPoint = "glfwSetFramebufferSizeCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial FramebufferResizedCallbackDelegate? SetFramebufferResizedCallback(nint window, FramebufferResizedCallbackDelegate callback);

		public delegate void WindowContentScaleChangedCallbackDelegate(nint window, int xscale, int yscale);

		[LibraryImport(DllName, EntryPoint = "glfwSetWindowContentScaleCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial WindowContentScaleChangedCallbackDelegate? SetWindowContentScaleChangedCallback(nint window, WindowContentScaleChangedCallbackDelegate callback);

		[LibraryImport(DllName, EntryPoint = "glfwGetInputMode", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetInputMode(nint window, int mode);

		[LibraryImport(DllName, EntryPoint = "glfwSetInputMode", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetInputMode(nint window, int mode, int value);

		[LibraryImport(DllName, EntryPoint = "glfwGetKey", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetKeyLastAction(nint window, int key);

		[LibraryImport(DllName, EntryPoint = "glfwGetMouseButton", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial int GetMouseButtonLastAction(nint window, int button);

		[LibraryImport(DllName, EntryPoint = "glfwGetCursorPos", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void GetCursorPosition(nint window, out double xpos, out double ypos);

		[LibraryImport(DllName, EntryPoint = "glfwSetCursorPos", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetCursorPosition(nint window, double xpos, double ypos);

		[LibraryImport(DllName, EntryPoint = "glfwSetCursor", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SetCursor(nint window, nint cursor);

		public delegate void KeyCallback(nint window, int key, int scancode, int action, int modifiers);

		[LibraryImport(DllName, EntryPoint = "glfwSetKeyCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial KeyCallback? SetKeyCallback(nint window, KeyCallback callback);

		public delegate void CharCallback(nint window, uint codepoint);

		[LibraryImport(DllName, EntryPoint = "glfwSetCharCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial CharCallback? SetCharCallback(nint window, CharCallback callback);

		public delegate void CharModifiersCallback(nint window, uint codepoint, int modifiers);

		[LibraryImport(DllName, EntryPoint = "glfwSetCharModsCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial CharModifiersCallback? SetCharModifiersCallback(nint window, CharModifiersCallback callback);

		public delegate void MouseButtonCallback(nint window, int button, int action, int modifiers);

		[LibraryImport(DllName, EntryPoint = "glfwSetMouseButtonCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial MouseButtonCallback? SetMouseButtonCallback(nint window, MouseButtonCallback callback);

		public delegate void CursorMovedCallback(nint window, double xpos, double ypos);

		[LibraryImport(DllName, EntryPoint = "glfwSetCursorPosCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial CursorMovedCallback? SetCursorMovedCallback(nint window, CursorMovedCallback callback);

		public delegate void CursorEnterCallback(nint window, int entered);

		[LibraryImport(DllName, EntryPoint = "glfwSetCursorEnterCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial CursorEnterCallback? SetCursorEnterCallback(nint window, CursorEnterCallback callback);

		public delegate void ScrolledCallback(nint window, double xoffset, double yoffset);

		[LibraryImport(DllName, EntryPoint = "glfwSetScrollCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial ScrolledCallback? SetScrolledCallback(nint window, ScrolledCallback callback);

		public delegate void DroppedCallback(nint window, int path_count, nint paths_ptrs_ptr);

		[LibraryImport(DllName, EntryPoint = "glfwSetDropCallback", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial DroppedCallback? SetDroppedCallback(nint window, DroppedCallback callback);

		[LibraryImport(DllName, EntryPoint = "glfwMakeContextCurrent", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void MakeContextCurrent(nint window);

		[LibraryImport(DllName, EntryPoint = "glfwGetCurrentContext", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial nint GetCurrentContext();

		[LibraryImport(DllName, EntryPoint = "glfwSwapBuffers", SetLastError = true), UnmanagedCallConv(CallConvs = [typeof(System.Runtime.CompilerServices.CallConvCdecl)])]
		internal static partial void SwapBuffers(nint window);
		#endregion
	}
}
