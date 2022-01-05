﻿using System;
using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("ChecksumValidator")]
[assembly: AssemblyDescription("ChecksumValidator: GUI application to compute checksum of a specified file and validate it against expected checksum")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyCopyright("Copyright (C) 2009 Milan Gardian. All rights reserved.")]
[assembly: ComVisible(false)]
[assembly: Guid("10ABE120-00E5-43FB-8101-16ACA2A31A04")]
[assembly: CLSCompliant(true)]

[assembly: AssemblyVersion("1.2")]
[assembly: AssemblyFileVersion("1.2.2009.05130")]
[assembly: AssemblyProduct("ChecksumValidator v1.2 "
#if DEBUG
	+ "(debug)"
#else
	+ "(release)"
#endif
)]
[assembly: AssemblyConfiguration(
#if DEBUG
	"DEBUG"
#else
	"RELEASE"
#endif
)]