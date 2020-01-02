#Region "Copyright"

' Copyright © 2020, TeamDev. All rights reserved.
' 
' Redistribution and use in source and/or binary forms, with or without
' modification, must retain the above copyright notice and the following
' disclaimer.
' 
' THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
' "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
' LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
' A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT
' OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
' SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT
' LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
' DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
' THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
' (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE
' OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#End Region

Imports System.Threading.Tasks
Imports DotNetBrowser.Browser
Imports DotNetBrowser.Engine

Namespace TransparentWebPage.Wpf
    ''' <summary>
    '''     Interaction logic for MainWindow.xaml
    ''' </summary>
    Partial Public Class MainWindow
        Inherits Window

        Private browser As IBrowser
        Private engine As IEngine

#Region "Constructors"

        Public Sub New()
            Try
                Task.Run(Sub()
                             engine = EngineFactory.Create(New EngineOptions.Builder With {.RenderingMode = RenderingMode.OffScreen}.Build())
                             browser = engine.CreateBrowser()
                             browser.Settings.TransparentBackgroundEnabled = True
                         End Sub).ContinueWith(Sub(t)
                                                   WebBrowser1.InitializeFrom(browser)
                                                   browser.MainFrame.LoadHtml("<html>" & vbLf & "     <body>" & "         <div style='background: yellow; opacity: 0.7;'>" & vbLf & "             This text is in the yellow half-transparent div." & "        </div>" & vbLf & "         <div style='background: red;'>" & vbLf & "             This text is in the red opaque div and should appear as is." & "        </div>" & vbLf & "         <div>" & vbLf & "             This text is in the non-styled div and should appear as a text on the completely transparent background." & "        </div>" & vbLf & "    </body>" & vbLf & " </html>")
                                               End Sub, TaskScheduler.FromCurrentSynchronizationContext())

                InitializeComponent()
            Catch exception As Exception
                Debug.WriteLine(exception)
            End Try
        End Sub

#End Region

#Region "Methods"

        Private Sub Window_Closed(ByVal sender As Object, ByVal e As EventArgs)
            engine?.Dispose()
        End Sub

#End Region
    End Class
End Namespace
