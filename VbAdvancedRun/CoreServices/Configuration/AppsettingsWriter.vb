Imports System.Text.RegularExpressions

Namespace CoreServices.Configuration

    ''' <summary>
    ''' Implements methods to write to the app settings file.
    ''' </summary>
    Friend Class AppSettingsWriter
        Implements IAppSettingsWriter

        ''' <summary>
        ''' The app data manager used for managing application data directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' The regex pattern for replacing slashes.
        ''' </summary>
        Private Const SlashPattern As String = "[\\/]"

        ''' <summary>
        ''' The compiled regex for replacing slashes.
        ''' </summary>
        Private Shared ReadOnly SlashRegex As New Regex(SlashPattern, RegexOptions.Compiled)

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AppSettingsWriter"/> class.
        ''' </summary>
        ''' <param name="appDataManager">The instance of <see cref="IAppDataManager"/> used to manage application data directories.</param>
        Public Sub New(appDataManager As IAppDataManager)
            _appDataManager = appDataManager
        End Sub

        ''' <summary>
        ''' Writes the settings to the app settings file.
        ''' </summary>
        ''' <returns>A task representing the asynchronous operation.</returns>
        Friend Async Function WriteSettings() As Task(Of Boolean) Implements IAppSettingsWriter.WriteSettings
            Dim filePath As String = _appDataManager.GetAppSettingsFilePath()
            Dim formattedPath As String = SlashRegex.Replace(PathStorage.Instance.ProgramPath, "\")
            Dim settings As New Dictionary(Of String, Object) From {
                {"Logging", New Dictionary(Of String, Object) From {
                    {"LogLevel", New Dictionary(Of String, String) From {
                        {"Default", "Information"},
                        {"Microsoft.Hosting.Lifetime", "Information"}
                    }}
                }},
                {"WorkerServiceSettings", New Dictionary(Of String, String) From {
                    {"FilePath", formattedPath}
                }}
            }
            Try
                Using fileStream As New StreamWriter(filePath)
                    Dim json As String = JsonConvert.SerializeObject(settings, Formatting.Indented)
                    Await fileStream.WriteAsync(json)
                End Using
                Return True
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace
