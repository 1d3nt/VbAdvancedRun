Namespace CoreServices.Configuration.Interfaces

    ''' <summary>
    ''' Provides methods to write to the app settings file.
    ''' </summary>
    ''' <remarks>
    ''' This interface includes the <see cref="IAppSettingsWriter.WriteSettings"/> method for writing settings asynchronously to the app settings file.
    ''' Implementations of this interface are expected to handle serialization and file writing.
    ''' </remarks>
    Public Interface IAppSettingsWriter

        ''' <summary>
        ''' Writes the provided settings to the app settings file.
        ''' </summary>
        ''' <returns>A task representing the asynchronous operation.</returns>
        ''' <remarks>
        ''' This method handles the serialization of the settings and writes them asynchronously to the specified app settings file.
        ''' </remarks>
        Function WriteSettings() As Task(Of Boolean)
    End Interface
End Namespace
