Namespace Application
    ''' <summary>
    ''' Configures the services for dependency injection.
    ''' </summary>
    ''' <remarks>
    ''' The <see cref="ServiceConfigurator"/> class provides methods to configure and register services
    ''' for dependency injection. It creates a new instance of the <see cref="ServiceCollection"/> class,
    ''' registers various services and their corresponding implementations, and builds an <see cref="IServiceProvider"/>
    ''' which can be used to resolve services at runtime.
    ''' </remarks>
    Friend Class ServiceConfigurator
        Implements IServiceConfigurator
        ''' <summary>
        ''' Registers all services required by the application.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the services will be added.
        ''' </param>
        ''' <remarks>
        ''' This method encapsulates the registration of various service categories essential for the application. 
        ''' It calls the following methods to register the respective services:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="RegisterErrorHandlingServices"/> - Registers services for error handling.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="RegisterUserInputServices"/> - Registers services related to user input.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="RegisterProgramPathValidator"/> - Registers the service for validating program paths.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="RegisterDirectoryServices"/> - Registers services for directory operations.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="RegisterResourceExtractorServices"/> - Registers services for resource extraction.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="RegisterAppRunner"/> - Registers the application runner service, responsible for executing the core logic of the application.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' </remarks>
        Private Shared Sub RegisterServices(services As IServiceCollection)
            RegisterErrorHandlingServices(services)
            RegisterUserInputServices(services)
            RegisterProgramPathValidator(services)
            RegisterDirectoryServices(services)
            RegisterResourceExtractorServices(services)
            RegisterAppRunner(services)
        End Sub
        ''' <summary>
        ''' Registers error handling services.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the error handling services are added. This instance of <see cref="IServiceCollection"/> 
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <remarks>
        ''' This method registers the following error handling services:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="IWin32ErrorHelper"/> is implemented by <see cref="Win32ErrorHelper"/>. This service helps retrieve Win32 error codes.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IWin32ErrorUtility"/> is implemented by <see cref="Win32ErrorUtility"/>. This service provides descriptions for Win32 error codes.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IErrorHandlingService"/> is implemented by <see cref="ErrorHandlingService"/>. This service handles Win32 errors.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IExitUtility"/> is implemented by <see cref="ExitUtility"/>. This service provides methods for exiting the application 
        '''       with specific error codes.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' </remarks>
        ''' <seealso cref="AddServices"/>
        ''' <seealso cref="IWin32ErrorHelper"/>
        ''' <seealso cref="Win32ErrorHelper"/>
        ''' <seealso cref="IWin32ErrorUtility"/>
        ''' <seealso cref="Win32ErrorUtility"/>
        ''' <seealso cref="IErrorHandlingService"/>
        ''' <seealso cref="ErrorHandlingService"/>
        ''' <seealso cref="IExitUtility"/>
        ''' <seealso cref="ExitUtility"/>
        Private Shared Sub RegisterErrorHandlingServices(services As IServiceCollection)
            Dim errorHandlingServices As New Dictionary(Of Type, Type) From {
                {GetType(IWin32ErrorHelper), GetType(Win32ErrorHelper)},
                {GetType(IWin32ErrorUtility), GetType(Win32ErrorUtility)},
                {GetType(IErrorHandlingService), GetType(ErrorHandlingService)},
                {GetType(IExitUtility), GetType(ExitUtility)}
            }
            AddServices(services, errorHandlingServices)
        End Sub
        ''' <summary>
        ''' Registers user input services.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the user input services are added. This instance of <see cref="IServiceCollection"/> 
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <remarks>
        ''' This method registers the following user input services:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="IUserInputReader"/> is implemented by <see cref="UserInputReader"/>. This service handles reading user inputs 
        '''       during interaction processes.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IUserPrompter"/> is implemented by <see cref="UserPrompter"/>. This service prompts the user for inputs, 
        '''       typically used in setup tasks where user confirmation is needed.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IUserInputChecker"/> is implemented by <see cref="UserInputChecker"/>. This service handles user interactions 
        '''       to verify decisions, such as whether to proceed with setup tasks.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' </remarks>
        ''' <seealso cref="AddServices"/>
        ''' <seealso cref="IUserInputReader"/>
        ''' <seealso cref="UserInputReader"/>
        ''' <seealso cref="IUserPrompter"/>
        ''' <seealso cref="UserPrompter"/>
        ''' <seealso cref="IUserInputChecker"/>
        ''' <seealso cref="UserInputChecker"/>
        Private Shared Sub RegisterUserInputServices(services As IServiceCollection)
            Dim userInputServices As New Dictionary(Of Type, Type) From {
                        {GetType(IUserInputReader), GetType(UserInputReader)},
                        {GetType(IUserPrompter), GetType(UserPrompter)},
                        {GetType(IUserInputChecker), GetType(UserInputChecker)}
                    }
            AddServices(services, userInputServices)
        End Sub
        ''' <summary>
        ''' Registers program path validation services.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the program path validation services are added. This instance of <see cref="IServiceCollection"/>
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <remarks>
        ''' This method registers the following program path validation services:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="IProgramPathValidator"/> is implemented by <see cref="ProgramPathValidator"/>. This service validates 
        '''       the correctness of a program's file path, ensuring that the path is well-formed and points to a valid location.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' 
        ''' The service is registered with a transient lifetime, meaning a new instance of <see cref="ProgramPathValidator"/> will be created 
        ''' each time it is requested by the application. This ensures that path validation can be performed independently across different 
        ''' program components.
        ''' </remarks>
        ''' <seealso cref="IProgramPathValidator"/>
        ''' <seealso cref="ProgramPathValidator"/>
        Private Shared Sub RegisterProgramPathValidator(services As IServiceCollection)
            Dim programPathValidatorServices As New Dictionary(Of Type, Type) From {
                        {GetType(IProgramPathValidator), GetType(ProgramPathValidator)}
                    }
            AddServices(services, programPathValidatorServices)
        End Sub
        ''' <summary>
        ''' Registers directory services.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the directory services are added. This instance of <see cref="IServiceCollection"/>
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <remarks>
        ''' This method registers the following directory services:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="IDirectoryManager"/> is implemented by <see cref="DirectoryManager"/>. This service handles directory operations.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IAppDataManager"/> is implemented by <see cref="AppDataManager"/>. This service manages application data directories.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' </remarks>
        ''' <seealso cref="IDirectoryManager"/>
        ''' <seealso cref="DirectoryManager"/>
        ''' <seealso cref="IAppDataManager"/>
        ''' <seealso cref="AppDataManager"/>
        Private Shared Sub RegisterDirectoryServices(services As IServiceCollection)
            Dim directoryServices As New Dictionary(Of Type, Type) From {
                        {GetType(IDirectoryManager), GetType(DirectoryManager)},
                        {GetType(IAppDataManager), GetType(AppDataManager)}
                    }
            AddServices(services, directoryServices)
        End Sub
        ''' <summary>
        ''' Registers resource extractor services.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the resource extractor services are added. This instance of <see cref="IServiceCollection"/>
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <remarks>
        ''' This method registers the following resource extractor services:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="IResourceExtractor"/> is implemented by <see cref="ResourceExtractor"/>. This service handles resource extraction.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IArchiveExtractor"/> is implemented by <see cref="ArchiveExtractor"/>. This service handles 7z archive extraction.
        '''     </description>
        '''   </item>
        '''   <item>
        '''     <description>
        '''       <see cref="IExtractorService"/> is implemented by <see cref="ExtractorService"/>. This service handles both resource and archive extraction.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' </remarks>
        ''' <seealso cref="IResourceExtractor"/>
        ''' <seealso cref="ResourceExtractor"/>
        ''' <seealso cref="IArchiveExtractor"/>
        ''' <seealso cref="ArchiveExtractor"/>
        ''' <seealso cref="IExtractorService"/>
        ''' <seealso cref="ExtractorService"/>
        Private Shared Sub RegisterResourceExtractorServices(services As IServiceCollection)
            Dim resourceExtractorServices As New Dictionary(Of Type, Type) From {
                        {GetType(IResourceExtractor), GetType(ResourceExtractor)},
                        {GetType(IArchiveExtractor), GetType(ArchiveExtractor)},
                        {GetType(IExtractorService), GetType(ExtractorService)}
                    }
            AddServices(services, resourceExtractorServices)
        End Sub
        ''' <summary>
        ''' Registers the <see cref="AppRunner"/> service.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the <see cref="IAppRunner"/> service is added. This instance of <see cref="IServiceCollection"/> 
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <remarks>
        ''' This method registers the following service:
        ''' <list type="bullet">
        '''   <item>
        '''     <description>
        '''       <see cref="IAppRunner"/> is registered as a transient service. This service is responsible for managing application runs and operations.
        '''     </description>
        '''   </item>
        ''' </list>
        ''' </remarks>
        ''' <seealso cref="IAppRunner"/>
        ''' <seealso cref="AppRunner"/>
        Private Shared Sub RegisterAppRunner(services As IServiceCollection)
            Dim appRunnerServices As New Dictionary(Of Type, Type) From {
                        {GetType(IAppRunner), GetType(AppRunner)}
                    }
            AddServices(services, appRunnerServices)
        End Sub
        ''' <summary>
        ''' Adds the specified services to the service collection.
        ''' </summary>
        ''' <param name="services">
        ''' The service collection to which the services are added. This instance of <see cref="IServiceCollection"/> 
        ''' is used to register services and their implementations for dependency injection.
        ''' </param>
        ''' <param name="serviceRegistrations">
        ''' The dictionary containing service registrations, where each key represents a service type and each value 
        ''' represents its corresponding implementation type.
        ''' </param>
        ''' <remarks>
        ''' This method iterates over the provided dictionary of service registrations and adds each service
        ''' to the <paramref name="services"/> collection with a transient lifetime.
        ''' 
        ''' This method uses to add the services. 
        ''' Transient services are created each time they are requested, which is suitable for lightweight, stateless services.
        ''' </remarks>
        Private Shared Sub AddServices(services As IServiceCollection, serviceRegistrations As Dictionary(Of Type, Type))
            For Each kvp As KeyValuePair(Of Type, Type) In serviceRegistrations
                services.AddTransient(kvp.Key, kvp.Value)
            Next
        End Sub
        ''' <summary>
        ''' Configures the services for dependency injection.
        ''' </summary>
        ''' <returns>
        ''' An <see cref="IServiceProvider"/> that provides the configured services. This provider can be used to resolve services
        ''' at runtime.
        ''' </returns>
        ''' <remarks>
        ''' The <see cref="ConfigureServices"/> method creates a new instance of the <see cref="ServiceCollection"/>
        ''' and registers various services and their corresponding implementations by calling the <see cref="RegisterServices"/> method.
        ''' The method then builds and returns an <see cref="IServiceProvider"/> which can be used to resolve services at runtime.
        ''' 
        ''' The returned <see cref="IServiceProvider"/> is the main interface for accessing the configured services and is used 
        ''' throughout the application to resolve dependencies.
        ''' </remarks>
        Public Function ConfigureServices() As IServiceProvider Implements IServiceConfigurator.ConfigureServices
            Dim services As New ServiceCollection()
            RegisterServices(services)
            Return services.BuildServiceProvider()
        End Function
    End Class
End Namespace
