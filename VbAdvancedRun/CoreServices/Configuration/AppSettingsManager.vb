Namespace CoreServices.Configuration

    ''' <summary>
    ''' Manages application settings by providing methods to read and write to the app settings file.
    ''' This class requires an implementation of <see cref="IAppDataManager"/> for managing application data directories.
    ''' </summary>
    ''' <remarks>
    ''' This class ensures that necessary application directories are created and resources extracted.
    ''' </remarks>
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
        ''' The app settings writer used for writing settings to the app settings file.
        ''' </summary>
        Private ReadOnly _appSettingsWriter As IAppSettingsWriter

        ''' <summary>
        ''' The app settings reader used for reading settings from the app settings file.
        ''' </summary>
        Private ReadOnly _appSettingsReader As IAppSettingsReader

        ''' <summary>
        ''' Initializes a new instance of the <see cref="AppSettingsManager"/> class.
        ''' </summary>
        ''' <param name="appDataManager">The instance of <see cref="IAppDataManager"/> used to manage application data directories.</param>
        ''' <param name="extractorService">The instance of <see cref="IExtractorService"/> used for extracting resources and archives.</param>
        ''' <param name="appSettingsWriter">The instance of <see cref="IAppSettingsWriter"/> used for writing settings to the app settings file.</param>
        ''' <param name="appSettingsReader">The instance of <see cref="IAppSettingsReader"/> used for reading settings from the app settings file.</param>
        Public Sub New(appDataManager As IAppDataManager, extractorService As IExtractorService, appSettingsWriter As IAppSettingsWriter, appSettingsReader As IAppSettingsReader)
            _appDataManager = appDataManager
            _extractorService = extractorService
            _appSettingsWriter = appSettingsWriter
            _appSettingsReader = appSettingsReader
        End Sub

        ''' <summary>
        ''' Reads the content of the app settings file and deserializes it into a dictionary.
        ''' </summary>
        ''' <returns>A dictionary representing the app settings.</returns>
        ''' <remarks>
        ''' This method ensures that the necessary directories and app settings file exist before attempting to read.
        ''' Although the call to <see cref="EnsureDirectoriesAndFilesExistAsync"/> occurs right after initialization,
        ''' it is done as a precaution to ensure that the file and directories remain intact for future operations.
        ''' </remarks>
        Friend Async Function ReadSettings() As Task(Of Dictionary(Of String, Object)) Implements IAppSettingsManager.ReadSettings
            Await EnsureDirectoriesAndFilesExistAsync()
            Try
                Return Await _appSettingsReader.ReadSettings()
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Writes the provided settings to the app settings file.
        ''' </summary>
        ''' <remarks>
        ''' This method ensures that the necessary directories and app settings file exist before attempting to write.
        ''' Although the call to <see cref="EnsureDirectoriesAndFilesExistAsync"/> occurs right after initialization,
        ''' it is done as a precaution to ensure that the file and directories remain intact for future operations.
        ''' </remarks>
        Friend Async Function WriteSettings() As Task(Of Boolean) Implements IAppSettingsManager.WriteSettings
            Try
                Await _appSettingsWriter.WriteSettings()
                Return True
            Catch
                Throw
            End Try
        End Function

        ''' <summary>
        ''' Ensures that the service and extracted directories exist.
        ''' This method delegates the directory creation to the <see cref="_appDataManager"/> and uses the <see cref="_extractorService"/> to extract resources and archives.
        ''' </summary>
        ''' <remarks>
        ''' This method ensures that the application's required directories and extracted files are created and maintained properly.
        ''' </remarks>
        Friend Async Function EnsureDirectoriesAndFilesExistAsync() As Task Implements IAppSettingsManager.EnsureDirectoriesAndFilesExistAsync
            _appDataManager.CreateServiceDirectory()
            Await _extractorService.ExtractAllWithMessagingAsync()
        End Function
    End Class
End Namespace
