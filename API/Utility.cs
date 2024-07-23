using System.Runtime.InteropServices;
using System.Text;

namespace GLFW_CS {
	internal static class Utility {
		internal static T[] Marshal_From_Unmanaged_Array<T>(nint src_ptr, int length) where T : struct {
			unsafe {
				var dest_array = new T[length];
				var size = Marshal.SizeOf<T>();
				var dest_ptr = (void*)Marshal.UnsafeAddrOfPinnedArrayElement(dest_array, 0);
				Buffer.MemoryCopy((void*)src_ptr, dest_ptr, length * size, length * size);

				return dest_array;
			}
		}

		internal static nint Marshal_To_Unmanaged_Array<T>(T[] source_array) where T : struct {
			unsafe {
				var size = Marshal.SizeOf<T>();
				var dest_ptr = (void*)Marshal.AllocHGlobal(source_array.Length * size);
				var src_ptr = (void*)Marshal.UnsafeAddrOfPinnedArrayElement(source_array, 0);
				Buffer.MemoryCopy(src_ptr, dest_ptr, source_array.Length * size, source_array.Length * size);

				return (nint)dest_ptr;
			}
		}

		internal static nint Marshal_String_To_Unmanaged_UTF8(string str) {
			int byte_count = Encoding.UTF8.GetByteCount(str);
			byte[] buffer = new byte[byte_count + 1];
			Encoding.UTF8.GetBytes(str, 0, str.Length, buffer, 0);

			nint ptr = Marshal.AllocHGlobal(buffer.Length);

			Marshal.Copy(buffer, 0, ptr, buffer.Length);

			return ptr;
		}
	}
}