//
// NWError.cs: Bindings the Netowrk nw_error API.
//
// Authors:
//   Miguel de Icaza (miguel@microsoft.com)
//
// Copyrigh 2018 Microsoft Inc
//
using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ObjCRuntime;
using Foundation;
using CoreFoundation;

namespace Network {
	public enum NWErrorDomain {
		Invalid = 0,
		Posix = 1,
		Dns = 2,
		Tls = 3,
	}

	[TV (12,0), Mac (10,14), iOS (12,0)]
	public class NWError : NativeObject {
		public NWError (IntPtr handle, bool owns) : base (handle, owns)
		{
		}

		[DllImport (Constants.NetworkLibrary)]
		static extern NWErrorDomain nw_error_get_error_domain (IntPtr error);

		public NWErrorDomain ErrorDomain => nw_error_get_error_domain (GetHandle());

		[DllImport (Constants.NetworkLibrary)]
		static extern int nw_error_get_error_code (IntPtr handle);

		public int ErrorCode => nw_error_get_error_code (GetHandle());

		[DllImport (Constants.NetworkLibrary)]
		static extern IntPtr nw_error_copy_cf_error (IntPtr error);

		public CFException CFError {
			get {
				return CFException.FromCFError (nw_error_copy_cf_error (GetHandle()), true);
			}
		}
	}
}
