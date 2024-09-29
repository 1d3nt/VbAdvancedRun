Namespace CoreServices.Configuration

    ''' <summary>
    ''' Implements methods to read from the app settings file.
    ''' </summary>
    Friend Class AppSettingsReader
        Implements IAppSettingsReader

        ''' <summary>
        ''' The app data manager used for managing application data directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AppSettingsReader"/> class.
        ''' </summary>
        ''' <param name="appDataManager">The instance of <see cref="IAppDataManager"/> used to manage application data directories.</param>
        Public Sub New(appDataManager As IAppDataManager)
            _appDataManager = appDataManager
        End Sub

        ''' <summary>
        ''' Reads the settings from the app settings file.
        ''' </summary>
        ''' <returns>A task representing the asynchronous operation, with a dictionary of settings as the result.</returns>
        Friend Async Function ReadSettings() As Task(Of Dictionary(Of String, Object)) Implements IAppSettingsReader.ReadSettings
            Dim filePath As String = _appDataManager.GetAppSettingsFilePath()
            Dim settings As Dictionary(Of String, Object)
            Try
                Using fileStream As New StreamReader(filePath)
                    Dim json As String = Await fileStream.ReadToEndAsync()
                    settings = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(json)
                End Using
                Return settings
            Catch
                Throw
            End Try
        End Function
    End Class
End Namespace
