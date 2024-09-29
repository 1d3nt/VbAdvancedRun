Namespace CoreServices.Configuration.Interfaces

    ''' <summary>
    ''' Provides methods to read from the app settings file.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IAppSettingsReader.ReadSettings"/> method for asynchronously reading the settings from the app settings file.
    ''' Implementations of this interface are expected to handle deserialization and retrieval of settings.
    ''' </remarks>
    Public Interface IAppSettingsReader

        ''' <summary>
        ''' Reads the settings from the app settings file.
        ''' </summary>
        ''' <returns>A task representing the asynchronous operation, with a dictionary of settings as the result.</returns>
        ''' <remarks>
        ''' This method reads the app settings file asynchronously and deserializes the content into a dictionary of key-value pairs.
        ''' </remarks>
        Function ReadSettings() As Task(Of Dictionary(Of String, Object))
    End Interface
End Namespace
