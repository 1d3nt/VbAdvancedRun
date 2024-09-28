Namespace CoreServices.Configuration.Interfaces

    ''' <summary>
    ''' Defines the contract for managing application settings.
    ''' Provides methods to read and write to the app settings file.
    ''' </summary>
    Public Interface IAppSettingsManager

        ''' <summary>
        ''' Reads the content of the app settings file and deserializes it into a dictionary.
        ''' </summary>
        ''' <returns>A dictionary representing the app settings.</returns>
        Function ReadSettings() As Task(Of Dictionary(Of String, Object))

        ''' <summary>
        ''' Writes the provided settings to the app settings file.
        ''' </summary>
        ''' <param name="settings">A dictionary representing the app settings to write.</param>
        Function WriteSettings(settings As Dictionary(Of String, Object)) As Task

        ''' <summary>
        ''' Ensures that the service directory and necessary files exist.
        ''' </summary>
        Function EnsureDirectoriesAndFilesExistAsync() As Task
    End Interface
End Namespace
