Imports Microsoft.Win32
Imports System.Security.Principal

Public Class RegEdit


    Public EjecucionValida As Boolean = False

    Sub New()

        VerficarEinstalar()
    End Sub


    Private Sub VerficarEinstalar()
        Try

            If IsNothing(Registry.ClassesRoot.OpenSubKey("fotoUom", False)) Then

                Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
                Dim principal As WindowsPrincipal = New WindowsPrincipal(identity)
                If principal.IsInRole(WindowsBuiltInRole.Administrator) = False Then
                    EjecucionValida = False
                    MessageBox.Show("Debe correr la aplicación en modo administrador por primera vez para instalarla." + vbCrLf + "Si no sabe como resolver esto, contacte a su servicio de soporte")
                    Application.Exit()
                    Exit Sub
                End If
                '  no esta instalado            
                Dim regKey As RegistryKey
                regKey = Registry.ClassesRoot.CreateSubKey("fotoUom")
                regKey.SetValue("", "URL:fotoUom Protocol")
                regKey.SetValue("URL Protocol", "")
                Dim defIcon As RegistryKey = regKey.CreateSubKey("DefaultIcon")
                defIcon.SetValue("", """C:\Axyonar\FotoUom\WebCamApp.exe""")
                Dim shell As RegistryKey = regKey.CreateSubKey("shell")
                Dim open As RegistryKey = shell.CreateSubKey("open")
                Dim command As RegistryKey = open.CreateSubKey("command")
                command.SetValue("", """C:\Axyonar\FotoUom\WebCamApp.exe"" ""%1""")

                defIcon.Close()
                shell.Close()
                open.Close()
                command.Close()
                regKey.Close()
            End If
            EjecucionValida = True
        Catch ex As Exception
            EjecucionValida = False
            Throw New Exception("Error: " + vbCrLf + ex.Message + vbCrLf + ex.StackTrace)
        End Try
    End Sub

End Class