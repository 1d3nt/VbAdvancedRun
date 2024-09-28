Namespace CoreServices.Configuration

    ''' <summary>
    ''' Manages application settings by providing methods to read and write to the app settings file.
    ''' This class requires an implementation of <see cref="IAppDataManager"/> for managing application data directories.
    ''' </summary>
    Friend Class AppSettingsManager
        Implements IAppSettingsManager

        ''' <summary>
        ''' The app data manager used for managing application data directories.
        ''' </summary>
        Private ReadOnly _appDataManager As IAppDataManager

        ''' <summary>
        ''' The extractor service used for extracting resources and archives.
        ''' </summary>
        Private ReadOnly _extractorService As IExtractorService

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AppSettingsManager"/> class.
        ''' </summary>
        ''' <param name="appDataManager">The instance of <see cref="IAppDataManager"/> used to manage application data directories.</param>
        ''' <param name="extractorService">The instance of <see cref="IExtractorService"/> used for extracting resources and archives.</param>
        Public Sub New(appDataManager As IAppDataManager, extractorService As IExtractorService)
            _appDataManager = appDataManager
            _extractorService = extractorService
        End Sub

        ''' <summary>
        ''' Reads the content of the app settings file and deserializes it into a dictionary.
        ''' </summary>
        ''' <returns>A dictionary representing the app settings.</returns>
        Friend Async Function ReadSettings() As Task(Of Dictionary(Of String, Object)) Implements IAppSettingsManager.ReadSettings
            Await EnsureDirectoriesAndFilesExistAsync()
            Dim jsonString As String = Await File.ReadAllTextAsync(_appDataManager.GetAppSettingsFilePath())
            Return JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonString)
        End Function

        ''' <summary>
        ''' Writes the provided settings to the app settings file.
        ''' </summary>
        ''' <param name="settings">A dictionary representing the app settings to write.</param>
        Friend Async Function WriteSettings(settings As Dictionary(Of String, Object)) As Task Implements IAppSettingsManager.WriteSettings
            Await EnsureDirectoriesAndFilesExistAsync()
            Dim jsonString As String = JsonConvert.SerializeObject(settings, Formatting.Indented)
            Await File.WriteAllTextAsync(_appDataManager.GetAppSettingsFilePath(), jsonString)
        End Function

        ''' <summary>
        ''' Ensures that the service and extracted directories exist.
        ''' This method delegates the directory creation to the <see cref="_appDataManager"/> and uses the <see cref="_extractorService"/> to extract resources and archives.
        ''' </summary>
        Friend Async Function EnsureDirectoriesAndFilesExistAsync() As Task Implements IAppSettingsManager.EnsureDirectoriesAndFilesExistAsync
            _appDataManager.CreateServiceDirectory()
            Await _extractorService.ExtractAllWithMessagingAsync()
        End Function
    End Class
End Namespace
