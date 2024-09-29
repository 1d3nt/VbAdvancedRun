Namespace CoreServices.Configuration.Interfaces

    ''' <summary>
    ''' Defines the contract for managing application settings.
    ''' Provides methods to read and write to the app settings file.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the following methods:
    ''' <see cref="IAppSettingsManager.ReadSettings"/> for reading the app settings file,
    ''' <see cref="IAppSettingsManager.WriteSettings"/> for writing settings to the app settings file,
    ''' and <see cref="IAppSettingsManager.EnsureDirectoriesAndFilesExistAsync"/> for ensuring that necessary directories and files exist.
    ''' </remarks>
    Public Interface IAppSettingsManager

        ''' <summary>
        ''' Reads the content of the app settings file and deserializes it into a dictionary.
        ''' </summary>
        ''' <returns>A dictionary representing the app settings.</returns>
        ''' <remarks>
        ''' This method ensures that the app settings file is properly read and deserialized into a dictionary of key-value pairs.
        ''' </remarks>
        Function ReadSettings() As Task(Of Dictionary(Of String, Object))

        ''' <summary>
        ''' Writes the provided settings to the app settings file.
        ''' </summary>
        ''' <remarks>
        ''' This method serializes the provided settings dictionary and writes it to the app settings file.
        ''' </remarks>
        Function WriteSettings() As Task(of Boolean)

        ''' <summary>
        ''' Ensures that the service directory and necessary files exist.
        ''' </summary>
        ''' <remarks>
        ''' This method guarantees that the necessary service directory and files are present, creating them if needed.
        ''' </remarks>
        Function EnsureDirectoriesAndFilesExistAsync() As Task
    End Interface
End Namespace
