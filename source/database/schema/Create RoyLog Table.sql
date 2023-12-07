CREATE TABLE [dbo].[RoyLog](
	[Id] [varchar](32) NOT NULL,
	[Level] [varchar](11) NOT NULL,
	[Message] [text] NULL,
	[Date] [datetimeoffset](7) NOT NULL,
	[CustomListOfParametersJSON] [text] NULL,
	[LogValueJSON] [text] NULL,
	[MethodCallerFileName] [varchar](1000) NULL,
	[MethodCallerMethodName] [varchar](500) NULL,
	[MethodCallerLineNumber] [int] NULL,
	[MethodParametersJSON] [text] NULL,
	[MachineCLRVersion] [varchar](50) NULL,
	[MachineDomainName] [varchar](500) NULL,
	[MachineName] [varchar](500) NULL,
	[MachineOperativeSystemVersion] [varchar](250) NULL,
	[MachineUserAccountName] [varchar](250) NULL,
	[MachineOperativeSystem] [varchar](10) NULL,
	[ApplicationIsDebuggingEnabled] [bit] NULL,
	[ApplicationPhysicalPath] [varchar](500) NULL,
	[ApplicationAssemblyLocation] [varchar](500) NULL,
	[ApplicationFriendlyName] [varchar](100) NULL,
	[ApplicationIsFullyTrusted] [bit] NULL,
	[ApplicationUserDomainName] [varchar](500) NULL,
	[ApplicationUserName] [varchar](250) NULL,
	[WebApplicationCurrentURL] [varchar](1000) NULL,
	[WebApplicationCurrentURLParameters] [varchar](1000) NULL,
	[WebApplicationPreviousURL] [varchar](1000) NULL,
	[WebApplicationUserHostIP] [varchar](250) NULL,
	[WebApplicationIsSecureConnection] [bit] NULL,
	[WebApplicationDomainName] [varchar](500) NULL,
	[WebApplicationCookiesValues] [varchar](1000) NULL,
	[WebApplicationHeadersValues] [varchar](1000) NULL,
	[WebApplicationUserLanguagePreferences] [varchar](100) NULL,
	[CreatedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_RoyLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[RoyLog] ADD  CONSTRAINT [DF_RoyLog_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO


