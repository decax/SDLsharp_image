using System;
using System.IO;
using System.Runtime.InteropServices;

namespace SDL
{
	public class Image : IDisposable
	{
		[Flags]
		public enum Flags
		{
			JPG  = 0x00000001,
			PNG  = 0x00000002,
			TIF  = 0x00000004,
			WEBP = 0x00000008
		}

		public Image(Flags flags)
		{
			IMG_Init((int)flags);
		}

		public void Dispose()
		{
			IMG_Quit();
		}

		public static Texture LoadTexture(Renderer renderer, string filename)
		{
			var sdlTexture = IMG_LoadTexture(renderer.sdlRenderer, filename);
			if (sdlTexture == IntPtr.Zero)
			{
				throw new FileNotFoundException();
			}

			return new Texture(renderer, sdlTexture);
		}

		#region Native

		const string sdlImageDLL = "/Library/Frameworks/SDL2_image.framework/SDL2_image";

		[DllImport(sdlImageDLL)]
		private static extern int IMG_Init(int flags);

		[DllImport(sdlImageDLL)]
		private static extern void IMG_Quit();

		[DllImport(sdlImageDLL)]
		private static extern IntPtr IMG_Load(string file);

		[DllImport(sdlImageDLL)]
		private static extern IntPtr IMG_LoadTexture(IntPtr renderer, string file);


		#endregion
	}
}

