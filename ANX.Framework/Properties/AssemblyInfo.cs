using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Allgemeine Informationen über eine Assembly werden über die folgenden 
// Attribute gesteuert. Ändern Sie diese Attributwerte, um die Informationen zu ändern,
// die mit einer Assembly verknüpft sind.
[assembly: AssemblyTitle("ANX.Framework")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("ANX Developer Team")]
[assembly: AssemblyProduct("ANX.Framework")]
[assembly: AssemblyCopyright("Copyright © ANX Developer Team 2011")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Durch Festlegen von ComVisible auf "false" werden die Typen in dieser Assembly unsichtbar 
// für COM-Komponenten. Wenn Sie auf einen Typ in dieser Assembly von 
// COM zugreifen müssen, legen Sie das ComVisible-Attribut für diesen Typ auf "true" fest.
[assembly: ComVisible(false)]

// Die folgende GUID bestimmt die ID der Typbibliothek, wenn dieses Projekt für COM verfügbar gemacht wird
[assembly: Guid("67548701-e7e2-4c56-be3f-18c5fdf5c0fb")]

// Versionsinformationen für eine Assembly bestehen aus den folgenden vier Werten:
//
//      Hauptversion
//      Nebenversion 
//      Buildnummer
//      Revision
//
// Sie können alle Werte angeben oder die standardmäßigen Build- und Revisionsnummern 
// übernehmen, indem Sie "*" eingeben:
[assembly: AssemblyVersion("0.4.22.*")]
[assembly: AssemblyFileVersion("0.4.22.0")]

[assembly:InternalsVisibleTo("ANX.Framework.Windows.DX10")]
[assembly:InternalsVisibleTo("ANX.Framework.Windows.DX11.1")]
[assembly:InternalsVisibleTo("ANX.Framework.Windows.GL3")]
[assembly:InternalsVisibleTo("ANX.Framework.Windows.Kinect")]
[assembly:InternalsVisibleTo("ANX.Framework.Windows.XInput")]
[assembly:InternalsVisibleTo("ANX.Framework.Windows.XAudio")]
